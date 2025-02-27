using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.ConfigurationModels;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Repository;
using Ebret4m4n.Repository.Configuration;
using Ebret4m4n.Repository.Repositories;
using Ebret4m4n.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ebret4m4n.API.Extenstions;

public static class ServiceExtenstions
{
    public static void ConfigureAddDbContext(this IServiceCollection service,
        IConfiguration configuration) =>
        service.AddDbContext<EbretAmanDbContext>(optins =>
            optins.UseSqlServer(configuration.GetConnectionString("dbConnection")));

    public static void ConfigureCors(this IServiceCollection service) =>
        service.AddCors(opts =>
            opts.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            )
        );

    public static void ConfigureAddIdentity(this IServiceCollection service) =>
        service.AddIdentity<ApplicationUser, IdentityRole>(opts =>
        {
            opts.User.RequireUniqueEmail = true;
            opts.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-._@ " +
            "ءآأؤإئابةتثجحخدذرزسشصضطظعغفقكلمنهوىي";

            opts.Password.RequireNonAlphanumeric = false;
            opts.Password.RequiredLength = 8;
            
            opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        
            opts.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
        })
        .AddEntityFrameworkStores<EbretAmanDbContext>()
        .AddDefaultTokenProviders();



    public static void ConfigureJWTAuthenticationToken(this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddAuthentication(opts =>
        {
            // Check JWT Header
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            // unauthorize
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opts =>
        {
            opts.SaveToken = true;
            opts.RequireHttpsMetadata = false;
            opts.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!))
            };
        });
    }

    public static void AddJwtConfiguration(this IServiceCollection service, IConfiguration config)
        => service.Configure<JwtConfiguration>(config.GetSection("JwtSettings"));

    public static void AddEmailSettingsConfiguration(this IServiceCollection service, IConfiguration config)
        => service.Configure<EmailSettings>(config.GetSection("EmailSettings"));

    public static void ConfigureTokenLifespan(this IServiceCollection service)
        => service.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(1));

    public static void EmailSenderConfiguration(this IServiceCollection service)
        => service.AddScoped<IEmailSender, EmailSender>();

    public static void UnitOfWorkConfiguration(this IServiceCollection service)
        => service.AddScoped<IUnitOfWork, UnitOfWork>();
}
