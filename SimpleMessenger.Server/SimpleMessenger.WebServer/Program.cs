using SimpleMessenger.Logic;
using SimpleMessenger.WebServer.Middlewares;
using SimpleMessenger.WebServer.Services;
using SimpleMessenger.DataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseWebSockets();
app.MapControllers();
app.UseMiddleware<CheckWebSocketMiddleware>();
app.Run();