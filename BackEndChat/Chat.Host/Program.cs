using Chat.Host.Configurations;
using Chat.Host.Datas;
using Chat.Host.Helpers;
using Chat.Host.Helpers.Abstractions;
using Chat.Host.Hubs;
using Chat.Host.Redis;
using Chat.Host.Redis.Abstractions;
using Chat.Host.Repositories;
using Chat.Host.Repositories.Abstractions;
using Chat.Host.Services;
using Chat.Host.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<AnalyzeConfig>(builder.Configuration.GetSection("Analyze"));

builder.Services
    .Configure<RedisConfig>(builder.Configuration.GetSection("Redis"));

builder.Services.AddTransient<IJsonProcess, JsonProcess>();
builder.Services.AddTransient<IRedisCacheConnection, RedisCacheConnection>();
builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddTransient<IAnalyzeTextService, AnalyzeTextService>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<ISentimentRepository, SentimentRepository>();
builder.Services.AddTransient<IChatService, ChatService>();


builder.Services.AddDbContextFactory<ApplicationDbContext>(opts =>
    opts.UseSqlServer(configuration["ConnectionString"]));

builder.Services.AddScoped<IDbContextWrapper<ApplicationDbContext>, DbContextWrapper<ApplicationDbContext>>();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://reactchatappui.azurewebsites.net")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors();

app.MapHub<ChatHub>("/chatHub");

CreateDbIfNotExists(app);

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}

void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            DBInitializer.Initialize(context).Wait();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}