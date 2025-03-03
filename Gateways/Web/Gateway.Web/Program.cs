var apiName = "[Web] API Gateway";

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug($"Initializing {apiName}...\n-----\n");

try 
{
    var builder = WebApplication.CreateBuilder(args);
    
    // Use NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
    builder.Configuration.AddJsonFile("jwt_properties.json", optional: false, reloadOnChange: true);

    builder.Services.AddOcelot();
    
    //builder.Services.Replace(ServiceDescriptor.Singleton<IClaimsParser, RoleClaimParser>());
    
    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var iss = builder.Configuration.GetSection("JwtProperties:Issuer").Value;
            var aud = builder.Configuration.GetSection("JwtProperties:Audience").Value;
            var key = builder.Configuration.GetSection("JwtProperties:Key").Value;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = iss,
                ValidAudience = aud,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!))
            };
        });

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

    app.UseHttpsRedirection();
    
    app.UseAuthentication();
    
    app.UseAuthorization(); 

    app.UseOcelot().Wait();

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
