var apiName = "Identity API";

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug($"Initializing {apiName}...\n-----\n");

try 
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    
    builder.Configuration.AddJsonFile("jwt_properties.json", optional: false, reloadOnChange: true);
    
    builder.Services.AddControllers();
    builder.Services.AddIdentityServices();
    builder.Services.AddAspVersioningService();
    builder.Services.AddInfrastructure(builder.Configuration);
    
    builder.Services.Configure<JwtProperties>(builder.Configuration.GetSection("JwtProperties"));
    
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }    
    
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.UseHttpsRedirection();
    app.Run();
}
catch (Exception ex)
{
    logger.Error($"Error(s) occured when starting {apiName}:\n-----\n{ex}");
}
finally
{
    LogManager.Shutdown();
}
