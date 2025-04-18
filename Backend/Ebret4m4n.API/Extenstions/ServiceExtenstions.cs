using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ebret4m4n.Entities.ConfigurationModels;
using Ebret4m4n.Repository.Configuration;
using Ebret4m4n.Repository.Repositories;
using Ebret4m4n.API.BackgroundService;
using Ebret4m4n.Repository.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
using Ebret4m4n.Entities.Models;
using Ebret4m4n.Repository;
using Ebret4m4n.Contracts;
using System.Text;
using Hangfire;
using Microsoft.OpenApi.Models;


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
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            ));

    public static void ConfigureAddIdentity(this IServiceCollection service) =>
        service.AddIdentity<ApplicationUser, IdentityRole>(opts =>
        {
            opts.User.RequireUniqueEmail = true;
            opts.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-._@ " +
            "ءآأؤإئابةتثجحخدذرزسشصضطظعغفقكلمنهوىي";

            opts.Password.RequireNonAlphanumeric = false;
            opts.Password.RequiredLength = 8;
            opts.Password.RequiredUniqueChars = 0;
            opts.Password.RequireUppercase = false;

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

    public static void AddSignalRConfiguration(this IServiceCollection service)
        => service.AddSignalR(config => config.EnableDetailedErrors = true);

    public static void AddHangfireConfiguration(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddHangfire(config =>
            config.UseSqlServerStorage(configuration.GetConnectionString("hangfireConnection")));

        service.AddHangfireServer();
    }

    public static void AddReservationReminderService(this IServiceCollection service)
        => service.AddScoped<ReservationReminderService>();

    public static void AddRateLimiter(this IServiceCollection service)
        => service.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter("global", _ => new FixedWindowRateLimiterOptions
                {
                    Window = TimeSpan.FromMinutes(1),
                    PermitLimit = 100,
                    QueueLimit = 0,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                })
            );

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.ContentType = "text/plain";
                await context.HttpContext.Response.WriteAsync("Rate limit exceeded. Try again later.", token);
            };
        });

    public static void AddStripeConfiguration(this IServiceCollection service, IConfiguration configuration)
        => service.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

    public static void AddSwaggerConfiguration(this IServiceCollection service)
    {
        service.AddSwaggerGen(c =>
        {
            // 1. Define the “Bearer” scheme
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter ‘Bearer’ [space] and then your valid JWT token.\n\nExample: \"Bearer eyJhbGciOiJI…\""
            });

            // 2. Require the “Bearer” scheme for all endpoints
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id   = "Bearer"
                    }
                },
                Array.Empty<string>()
            }});
        });
    }
}