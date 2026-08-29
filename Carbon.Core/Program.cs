using Carbon.Core.Middlewares;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddDataLayer(builder.Configuration);
builder.Services.AddCarbonInterface();
builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();