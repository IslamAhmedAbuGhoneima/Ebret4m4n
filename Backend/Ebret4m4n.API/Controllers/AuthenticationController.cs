using Ebret4m4n.Shared.DTOs.AuthenticationDtos;
using Ebret4m4n.Entities.ConfigurationModels;
using Microsoft.AspNetCore.Authorization;
using Ebret4m4n.Shared.DTOs.ChildDtos;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Ebret4m4n.Entities.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using System.Data;
using System.Text;
using System.Net;
using Mapster;


namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IEmailSender emailSender,
    IUnitOfWork unitOfWork,
    IOptions<JwtConfiguration> jwtConfig) : ControllerBase
{
    private readonly JwtConfiguration _jwtConfig = jwtConfig.Value;
    private ApplicationUser? _user;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(GeneralResponse<object>.FailureResponse(ModelState));

        var user = model.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(GeneralResponse<string>.FailureResponse("لم يتم انشاء مستخدم الرجاء التاكد من ان البيانات صحيحه"));
        

        await userManager.AddToRoleAsync(user, model.Role);

        var userDto = user.Adapt<UserDataDto>();

        var response = GeneralResponse<UserDataDto>.SuccessResponse(userDto);

        return CreatedAtAction("UserProfile", new { id = user.Id }, response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(
                 GeneralResponse<string>.FailureResponse("الرجاء التاكد من ادخال الايميل وكلمه المرور"));

        _user = await userManager.FindByEmailAsync(model.Email);

        if (_user is null)
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
        var token = await GenerateToken(true);

        var response = GeneralResponse<TokenDto>.SuccessResponse(token);

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

        var token = await GenerateToken(false);
        var response = GeneralResponse<TokenDto>.SuccessResponse(token);

        return Ok(response);
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgotPasswordDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(
                 GeneralResponse<object>.FailureResponse(ModelState)
            );

        _user = await userManager.FindByEmailAsync(model.Email);

        if (_user is null)
            return Ok(GeneralResponse<string>.SuccessResponse("سوف يتم ارسال رساله علي الجيميل اذا كنت مسجل به"));

        var token = await userManager.GeneratePasswordResetTokenAsync(_user);

        var callbackUrl = $"http://localhost:4200/auth/change-password?userId={_user.Id}&token={Uri.EscapeDataString(token)}";


        string email = model.Email;
        string subject = "اعاده تعيين كلمه المرور";
        string body = await GenerateEmailMessage(callbackUrl!,
            "اعاده تعيين كلمه المرور",
            "تم طلب اعاده تعيين كلمه مرور جديد");

        await emailSender.SendEmailAsync(email, subject, body);

        var response = GeneralResponse<string>.SuccessResponse("سيتم ارسال رساله اعاده تعيين كلمه سر لهذا البريد الالكتروني");

        return Ok(response);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(
                 GeneralResponse<object>.FailureResponse(ModelState));

        _user = await userManager.FindByIdAsync(model.UserId);

        if (_user is null)
            return NotFound(GeneralResponse<string>.FailureResponse($"لا يوجد مستخدم بهذا الرقم : {_user.Id}"));


        string decodedToken = WebUtility.UrlDecode(model.Token);
        string newPassword = model.NewPassword;

        var result = await userManager.ResetPasswordAsync(_user, decodedToken, newPassword);

        if(!result.Succeeded)
            throw new InValidTokenBadRequest();

        var response = GeneralResponse<string>.SuccessResponse("تم اعاده تعيين كلمه المرور بنجاح");

        return Ok(response);
    }

    [Authorize]
    [HttpPost("reset-email")]
    public async Task<IActionResult> ResetEmail([FromBody] ResetEmailDto model)
    {
        var userId = User.FindFirst("id")!.Value;
        _user = await userManager.FindByIdAsync(userId);

        if (_user is null)
            return NotFound(GeneralResponse<string>.FailureResponse($"لا يوجد مستخدم بهذا الرقم : {_user.Id}"));


        var token = await userManager.GenerateChangeEmailTokenAsync(_user, model.NewEmail);


        var callbackUrl = $"http://localhost:4200/auth/change-email?userId={_user.Id}&email={model.NewEmail}&token={Uri.EscapeDataString(token)}";

        string newEmail = model.NewEmail;
        string subject = "طلب تغير ايميل";
        string body = await GenerateEmailMessage(callbackUrl!,
            "تغير الايميل",
            "طلبت تغير الايميل الخاص بك الرجاء الضغط علي الرابط في الاسفل");

        await emailSender.SendEmailAsync(newEmail, subject, body);

        var response = GeneralResponse<string>.SuccessResponse("سوف يتم استلام رساله علي هذا البريد");

        return Ok(response);
    }

    [HttpGet("email-change")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto model)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(
                 GeneralResponse<object>.FailureResponse(ModelState));

        _user = await userManager.FindByIdAsync(model.UserId);
        if (_user is null)
            return NotFound(GeneralResponse<string>.FailureResponse($"لا يوجد مستخدم بهذا الرقم : {_user.Id}"));

        var decodedToken = WebUtility.UrlDecode(model.Token);
        var newEmail = model.NewEmail;

        var result = await userManager.ChangeEmailAsync(_user, newEmail, decodedToken);

        if (!result.Succeeded)
            throw new InValidTokenBadRequest();

        await signInManager.RefreshSignInAsync(_user);

        var response = GeneralResponse<string>.SuccessResponse("تم اعاد تعيين البريد الالكتروني بنجاح");

        return Ok(response);
    }

    [HttpGet("{id:guid}/user-profile")]
    public async Task<IActionResult> UserProfile(Guid id)
    {
        string userId = id.ToString();
        _user = await userManager.Users
            .Include(U => U.Children.Where(C => C.UserId == userId))
            .FirstOrDefaultAsync(U => U.Id == userId);

        if (_user is null)
            return NotFound(GeneralResponse<string>.FailureResponse($"لا يوجد مستخدم بهذا الرقم : {_user.Id}"));


        var userChildren = new List<ChildDto>();
        foreach(var child in _user.Children)
        {
            var childDto = child.Adapt<ChildDto>();
            userChildren.Add(childDto);
        }

        var userDto = _user.Adapt<UserDataDto>();

        var response = GeneralResponse<UserDataDto>.SuccessResponse(userDto);

        return Ok(response);
    }

    #region Private actions

    private async Task<TokenDto> GenerateToken(bool populateExp)
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
            new("id",_user!.Id),
            new("name",$"{_user.FirstName} {_user.LastName}"),
            new("email",_user.Email!),
        ];

        var userRoles = await userManager.GetRolesAsync(_user);

        foreach (var role in userRoles)
            claims.Add(new("role", role));

        if (await userManager.IsInRoleAsync(_user, "doctor") ||
            await userManager.IsInRoleAsync(_user, "organizer"))
        {
            var staffRecord = 
                await unitOfWork.StaffRepo.FindAsync(s => s.UserId == _user.Id, false);

            if (staffRecord is null)
                throw new NotFoundBadRequest("لم نتمكن من ايجاد الوحده الصحيه التابعه لهذا المستخدم");

            var workHealthCareId = staffRecord.HCCenterId.ToString()!;

            claims.Add(new("healthCareId", workHealthCareId));
        }
        else if(await userManager.IsInRoleAsync(_user, "governorateAdmin"))
        {
            var governorateAdmin =
                await unitOfWork.GovernorateAdminRepo.FindAsync(admin => admin.UserId == _user.Id, false);

            claims.Add(new("governorate", governorateAdmin.Governorate));
        }
        else if (await userManager.IsInRoleAsync(_user, "cityAdmin"))
        {
            var cityAdmin =
                await unitOfWork.CityAdminStaffRepo.FindAsync(admin => admin.UserId == _user.Id, false);

            claims.Add(new("governorate", cityAdmin.Governorate));
            claims.Add(new("city", cityAdmin.City));

        }

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