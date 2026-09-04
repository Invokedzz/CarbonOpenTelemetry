using Carbon.Core.Middlewares;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataLayer(builder.Configuration);
builder.Services.AddClimatic(builder.Configuration);
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