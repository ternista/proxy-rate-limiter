using Api;
using Api.Mapping;
using Application.Mapping;
using Infrastructure.DI;
using Application.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterStarwarsApi(builder.Configuration);
builder.Services.RegisterApplicationServices();
builder.Services.RegisterRateLimiting(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddHttpLogging(o => {});

builder.Services.AddAutoMapper(typeof(ApiProfile), typeof(ApplicationProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<UnhandledExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

app.UseAuthorization();
app.UseExceptionHandler();

app.MapControllers();

app.Run();

public partial class Program { }