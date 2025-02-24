using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.ConfigurationModels;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Shared.DTOs.AuthenticationDtos;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IEmailSender emailSender,
    IOptions<JwtConfiguration> jwtConfig) : ControllerBase
{
    private readonly JwtConfiguration _jwtConfig = jwtConfig.Value;
    private ApplicationUser? _user;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var user = model.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errors });
        }

        var userDto = user.Adapt<UserDataDto>();

        return CreatedAtAction("UserProfile", new { id = user.Id }, userDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(ModelState);


        _user = await userManager.FindByEmailAsync(model.Email);

        if(_user is null)
            throw new LoginBadRequest();

        bool checkPassword = await userManager.CheckPasswordAsync(_user, model.Password);

        if (await userManager.IsLockedOutAsync(_user!))
            throw new LockedOutBadRequest();

        if (!checkPassword)
        {
            await userManager.AccessFailedAsync(_user!);
            throw new LoginBadRequest();
        }
        
        await userManager.ResetAccessFailedCountAsync(_user!);
        var response = await GenerageToken(true);

        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
    {
        ClaimsPrincipal principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

        ApplicationUser? user = await userManager.FindByNameAsync(principal.Identity.Name);

        if (user is null || user.RefreshToken != tokenDto.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new RefreshTokenBadRequest();

        _user = user;

        var response = await GenerageToken(false);

        return Ok(response);
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgotPasswordDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        _user = await userManager.FindByEmailAsync(model.Email);

        if (_user is null)
            return Ok(new { Message = "If your email is registered, you will receive a password reset link." });

        var token = await userManager.GeneratePasswordResetTokenAsync(_user);

        var callbackUrl = Url.Action("ResetPassword", "Authenticate",
            new { userId = _user.Id, token = token }, protocol: HttpContext.Request.Scheme);
        
        string email = model.Email;
        string subject = "Reset Password";
        string body = await GenerateEmailMessage(callbackUrl!,
            "Reset Your Password",
            "You requested a password reset.Click the link below to reset it:");

        await emailSender.SendEmailAsync(email, subject, body);

        return Ok(new { Message = "If your email is registered, you will receive a password reset link." });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        _user = await userManager.FindByIdAsync(model.UserId);

        if (_user is null)
            throw new NotFoundBadRequest($"user with {model.UserId} not found");


        string decodedToken = WebUtility.UrlDecode(model.Token);
        string newPassword = model.NewPassword;

        var result = await userManager.ResetPasswordAsync(_user, decodedToken, newPassword);

        if(!result.Succeeded)
            throw new InValidTokenBadRequest();

        return Ok(new { Message = "Password has been reset successfully." });
    }


    [Authorize]
    [HttpPost("reset-email")]
    public async Task<IActionResult> ResetEmail([FromBody] ResetEmailDto model)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        _user = await userManager.FindByIdAsync(userId);

        if (_user is null)
            throw new NotFoundBadRequest($"user with {userId} not found");

        var token = await userManager.GenerateChangeEmailTokenAsync(_user, model.NewEmail);

        var callbackUrl = Url.Action("ResetPassword", "Authenticate",
            new { userId = _user.Id, email = model.NewEmail, token = token }, protocol: HttpContext.Request.Scheme);


        string newEmail = model.NewEmail;
        string subject = "Change Email";
        string body = await GenerateEmailMessage(callbackUrl!,
            "Change your email",
            "You requested an email reset.Click the link below to reset it:");

        await emailSender.SendEmailAsync(newEmail, subject, body);

        return Ok(new { Message = "You will receive email message for reste your email" });
    }

    [HttpGet("email-change")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        _user = await userManager.FindByIdAsync(model.UserId);
        if (_user is null)
            throw new NotFoundBadRequest($"user with {model.UserId} not found");

        var decodedToken = WebUtility.UrlDecode(model.Token);
        var newEmail = model.NewEmail;

        var result = await userManager.ChangeEmailAsync(_user, newEmail, decodedToken);

        if (!result.Succeeded)
            throw new InValidTokenBadRequest();

        await signInManager.RefreshSignInAsync(_user);
        return Ok(new { Message = "your Email Updated Successfully" });
    }


    [HttpGet("{id:guid}/user-profile")]
    public async Task<IActionResult> UserProfile(Guid id)
    {
        string userId = id.ToString();
        _user = await userManager.Users
            .Include(U => U.Children.Where(C => C.UserId == userId))
            .FirstOrDefaultAsync(U => U.Id == userId);

        if (_user is null)
            throw new NotFoundBadRequest($"user with {userId} not found");


        var userChildren = new List<ChildDto>();
        foreach(var child in _user.Children)
        {
            var childDto = child.Adapt<ChildDto>();
            userChildren.Add(childDto);
        }

        var userDto = _user.Adapt<UserDataDto>();

        return Ok(userDto);
    }

    #region Private actions

    private async Task<TokenDto> GenerageToken(bool populateExp)
    {
        var claims = await GetClaims();

        var signingCredential = GetSigningCredentials();


        var jwtToken = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            signingCredentials: signingCredential,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfig.Expires))
        );

        var refreshToken = GenerateRefreshToken();

        _user.RefreshToken = refreshToken;

        if (populateExp)
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

        await userManager.UpdateAsync(_user);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        // Generate token
        return new TokenDto(accessToken, refreshToken);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
        var key = new SymmetricSecurityKey(secret);
        var signingCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return signingCredential;
    }

    private async Task<List<Claim>> GetClaims()
    {
        List<Claim> claims = [
            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier,_user!.Id),
            new(ClaimTypes.Name,_user.UserName!),
            new(ClaimTypes.Email,_user.Email!),
        ];

        var userRoles = await userManager.GetRolesAsync(_user);

        foreach (var role in userRoles)
            claims.Add(new(ClaimTypes.Role, role));

        return claims;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtConfig.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtConfig.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!)),
            ValidateLifetime = true,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken is null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private async Task<string> GenerateEmailMessage(string resetLink, string emailTitle, string emailBody)
    {
        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "EmailMessage.html");
        Console.WriteLine(templatePath);

        // Check if file exists
        if (!System.IO.File.Exists(templatePath))
            throw new FileNotFoundException("Something went wrong with your Request Please Contact Support");

        // Read the file content
        string emailMessage = await System.IO.File.ReadAllTextAsync(templatePath);
        emailMessage = emailMessage.Replace("{RESET_LINK}", resetLink);
        emailMessage = emailMessage.Replace("EMAILTITLE", emailTitle);
        emailMessage = emailMessage.Replace("EMAILBODY", emailBody);

        return emailMessage;
    } 

    #endregion
}