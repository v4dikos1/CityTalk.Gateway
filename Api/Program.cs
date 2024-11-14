using Api;
using Api.Middleware;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromXml("nlog.config").GetCurrentClassLogger();

logger.Info("Инициализация API Gateway...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddUserGrpcClient(builder.Configuration);

    builder.Services.AddControllers();

    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    logger.Info($"Текущее окружение: {builder.Environment.EnvironmentName}");

    app.MapGet("/", () => "Hello World!");

    app.UseMiddleware<ExtensionHandlerMiddleware>();

    app.UseSwagger()
       .UseSwaggerUI();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "API Gateway остановлен из-за внутренней ошибки.");
    throw;
}
finally
{
    LogManager.Shutdown();
}