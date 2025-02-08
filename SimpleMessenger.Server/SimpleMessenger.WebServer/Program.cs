using SimpleMessenger.Logic;
using SimpleMessenger.WebServer.Middlewares;
using SimpleMessenger.WebServer.Services;
using SimpleMessenger.DataAccess;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Simple messenger API",
        Version = "v1"
    });

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddLogic();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Невозможно создать подключение к БД, т.к. не задана строка подключения"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple messenger API");
});
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseWebSockets();
app.MapControllers();
app.UseMiddleware<CheckWebSocketMiddleware>();
app.Run();