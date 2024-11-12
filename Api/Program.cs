using Api;
using Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine($"Current Environment: {builder.Environment.EnvironmentName}");
builder.Services.AddUserGrpcClient(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseMiddleware<ExtensionHandlerMiddleware>();

app.UseSwagger()
   .UseSwaggerUI();

app.MapControllers();

app.Run();
