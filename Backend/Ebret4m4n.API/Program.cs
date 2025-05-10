using Ebret4m4n.Repository.Configuration.DataSeed;
using Microsoft.Extensions.FileProviders;
using Ebret4m4n.API.BackgroundService;
using Ebret4m4n.API.Extenstions;
using Ebret4m4n.API.Mapping;
using Ebret4m4n.API.Hubs;
using Hangfire;
using Stripe;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(opts => opts.SuppressModelStateInvalidFilter = true);

builder.Services.ConfigureCors();
builder.Services.UnitOfWorkConfiguration();
builder.Services.EmailSenderConfiguration();


builder.Services.ConfigureAddDbContext(builder.Configuration);
builder.Services.ConfigureAddIdentity();
builder.Services.ConfigureJWTAuthenticationToken(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddEmailSettingsConfiguration(builder.Configuration);
builder.Services.ConfigureTokenLifespan();

builder.Services.AddSignalRConfiguration();

builder.Services.AddHangfireConfiguration(builder.Configuration);

builder.Services.AddReservationReminderService();

// Register Mapster
MapsterConfig.RegisterMappings();

// Rate Limiter
builder.Services.AddRateLimiter();

// Stripe Settings
builder.Services.AddStripeConfiguration(builder.Configuration);
StripeConfiguration.ApiKey = builder.Configuration["StripeSettings:secretKey"];


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.ConfigureExceptionHandler();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Files")),
    RequestPath = "/Files"
});

app.UseCors("CorsPolicy");

app.UseHangfireDashboard();

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");
app.MapHub<NotificationHub>("/notification");

// Ensure the database is created and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.CreateAdminUser(services, builder.Configuration);
}

app.NotificationMessagJob();

app.Run();