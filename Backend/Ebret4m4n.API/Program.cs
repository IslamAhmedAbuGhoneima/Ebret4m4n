using Ebret4m4n.Repository.Configuration.DataSeed;
using Ebret4m4n.API.Extenstions;
using Ebret4m4n.API.Hubs;
using Ebret4m4n.API.Mapping;


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

// Register Mapster
MapsterConfig.RegisterMappings();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.ConfigureExceptionHandler();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("CorsPolicy");

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

app.Run();
