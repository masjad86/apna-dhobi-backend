var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var swaggerSettings = builder.Configuration.GetSection("Swagger").Get<SwaggerSetting>();    
var rateLimitSettings = builder.Configuration.GetSection("RateLimiting").Get<RateLimiterSetting>();
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSetting>();   
builder.Services.AddScoped<GatewayHealthCheck>();
builder.Services.AddScoped<DownstreamHealthCheck>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = swaggerSettings.Title,
        Version = swaggerSettings.Version,
        Description = swaggerSettings.Description,
        TermsOfService = new Uri(swaggerSettings.TermsOfService),
        Contact = new OpenApiContact
        {
            Name = swaggerSettings.Contact.Name,
            Email = swaggerSettings.Contact.Email,
            Url = new Uri(swaggerSettings.Contact.Url)
        },
    });
});

builder.Services.AddHealthChecks()
    .AddCheck<GatewayHealthCheck>("gateway")
    .AddCheck<DownstreamHealthCheck>("downstream");

builder.Services.AddHttpClient();
builder.Services.AddRateLimiter(options =>
{
    // options.AddPolicy("Global", context =>
    //     RateLimitPartition.GetFixedWindowLimiter(
    //         partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
    //         factory: _ => new FixedWindowRateLimiterOptions
    //         {
    //             PermitLimit = 100,
    //             Window = TimeSpan.FromMinutes(1),
    //             QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
    //             QueueLimit = 0
    //        }));
});

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = "ApiKeyScheme";
    config.DefaultChallengeScheme = "ApiKeyScheme";
}).AddJwtBearer("ApiKeyScheme", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        ClockSkew = TimeSpan.Zero
    };
});
// builder.Services.AddAuthentication("ApiKeyScheme")
//     .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKeyScheme", null);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiKeyPolicy", policy =>
    {
        policy.AddAuthenticationSchemes("ApiKeyScheme")
              .RequireAuthenticatedUser();
    });
});

var app = builder.Build();

// app.UseOcelot().Wait();
app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // app.UseSwaggerUI(config =>
    // {
    //     config.SwaggerEndpoint("/swagger/v1/swagger.json", swaggerSettings?.Title);
    // });
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();
app.Run();
