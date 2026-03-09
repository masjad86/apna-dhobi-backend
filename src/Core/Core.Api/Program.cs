using Swashbuckle.AspNetCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Caching.StackExchangeRedis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((config) =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Core.Api", Version = "v1" });
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    // var redis = sp.GetRequiredService<IOptions<RedisOptions>>().Value;

    // Build configuration string with extra params
    // options.Configuration =
    //     $"{redis.Configuration}," +
    //     $"abortConnect={(redis.AbortOnConnectFail ? "true" : "false")}," +
    //     $"connectTimeout={redis.ConnectTimeoutMs}," +
    //     $"syncTimeout={redis.SyncTimeoutMs}";

    // options.InstanceName = redis.InstanceName;
});

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
app.Run();
