using System.Text;
using System.Threading.RateLimiting;
using Gateway.Api.HealthChecks;
using Gateway.Api.Models.Settings;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var swaggerSettings = builder.Configuration.GetSection("Swagger").Get<SwaggerSetting>();    
var rateLimitSettings = builder.Configuration.GetSection("RateLimiting").Get<RateLimiterSetting>();
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSetting>();   
builder.Services.AddScoped<GatewayHealthCheck>();
builder.Services.AddScoped<DownstreamHealthCheck>();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks()
    .AddCheck<GatewayHealthCheck>("gateway")
    .AddCheck<DownstreamHealthCheck>("downstream");

builder.Services.AddHttpClient();
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("Global", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = rateLimitSettings?.RequestsPerMinute ?? 100,
                Window = TimeSpan.FromMinutes(rateLimitSettings?.RequestsPerMinute > 0 ? 1 : 0),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = rateLimitSettings?.BurstSize ?? 0
           }));
});

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = "ApiKeyScheme";
    config.DefaultChallengeScheme = "ApiKeyScheme";
}).AddJwtBearer("ApiKeyScheme", opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings?.Issuer,
        ValidAudience = jwtSettings?.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Secret ?? string.Empty)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseOcelot().Wait();
app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();
app.Run();
