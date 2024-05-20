using CongestionTaxCalculator.Api.Common.Data;
using CongestionTaxCalculator.Api.Common.Extensions;
using CongestionTaxCalculator.Api.Common.Middlewares;
using CongestionTaxCalculator.Api.Features;

var builder = WebApplication.CreateBuilder(args);

#region DI

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// App Services
builder.Services.ConfigureDbContexts(builder.Configuration);
builder.Services.ConfigureValidator();
// Features
builder.Services.ConfigureCongestionTaxFeature();

#endregion

#region Pipeline

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();
app.UseSeeder();

// Endpoints
app.MapCongestionFeatures();

#endregion

app.Run();