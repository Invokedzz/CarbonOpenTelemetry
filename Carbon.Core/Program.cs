using Carbon.Core.Extensions;
using Carbon.Core.Middlewares;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarbonServices();
builder.Services.AddCarbonUseCases();
builder.Services.AddDataLayer(builder.Configuration);
builder.Services.AddTImpact(builder.Configuration);
builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();
app.UseExceptionHandler("/Error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();