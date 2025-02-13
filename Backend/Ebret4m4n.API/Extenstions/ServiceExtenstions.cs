using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Repository;
using Ebret4m4n.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-._@+";

            opts.Password.RequireNonAlphanumeric = false;
            opts.Password.RequiredLength = 8;
            
            opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        
            opts.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
        })
        .AddEntityFrameworkStores<EbretAmanDbContext>()
        .AddDefaultTokenProviders();

    public static void UnitOfWorkConfiguration(this IServiceCollection service)
        => service.AddScoped<IUnitOfWork, UnitOfWork>();
}
