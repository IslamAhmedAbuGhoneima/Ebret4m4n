using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.ConfigurationModels;
using Ebret4m4n.Entities.Exceptions;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
public class AuthenticateController(UserManager<ApplicationUser> userManager,
    IEmailSender emailSender,
    IOptions<JwtConfiguration> jwtConfig) : ControllerBase
{
    private readonly JwtConfiguration _jwtConfig = jwtConfig.Value;
    private ApplicationUser? _user;

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var user = new ApplicationUser()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = $"{model.FirstName} {model.LastName}",
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Governorate = model.Governorate,
            City = model.City,
            Village = model.Village,
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
            return Ok(new { Message = "User created successfully." });

        var errors = result.Errors.Select(e => e.Description);
        return BadRequest(new { Errors = errors });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        bool userFound = await ValidateUser(model.Email, model.Password);

        if (!userFound)
        {
            if (await userManager.IsLockedOutAsync(_user!))
                return BadRequest(new { Message = "You are Locked try again later" });

            await userManager.AccessFailedAsync(_user!);
            return NotFound(new { Message = "Wrong email or password" });
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
        string body = await GenerateEmailMessage(callbackUrl);

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
            throw new InValidTokenBadRequest();


        string decodedToken = WebUtility.UrlDecode(model.Token);
        string newPassword = model.NewPassword;

        var result = await userManager.ResetPasswordAsync(_user, decodedToken, newPassword);

        if(!result.Succeeded)
            throw new InValidTokenBadRequest();

        return Ok(new { Message = "Password has been reset successfully." });
    }


    #region Private actions

    private async Task<bool> ValidateUser(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        _user = user;

        if (_user is null ||
            !await userManager.CheckPasswordAsync(_user, password))
            return false;

        return true;
    }

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

    private async Task<string> GenerateEmailMessage(string resetLink)
    {
        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "ResetPassword.html");
        Console.WriteLine(templatePath);

        // Check if file exists
        if (!System.IO.File.Exists(templatePath))
        {
            return "Email template not found!";
        }

        // Read the file content
        string emailBody = await System.IO.File.ReadAllTextAsync(templatePath);
        emailBody = emailBody.Replace("{RESET_LINK}", resetLink);

        return emailBody;
    } 

    #endregion
}