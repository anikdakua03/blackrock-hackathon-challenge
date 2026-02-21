using Blackrock_Hackathon.Interfaces;
using BlackRock_Hackathon.Enums;
using BlackRock_Hackathon.Middlewares;
using BlackRock_Hackathon.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ITransactionBuilder, TransactionBuilder>();
builder.Services.AddScoped<ITransactionValidator, TransactionValidator>();
builder.Services.AddScoped<ITemporalConstraintsValidator, TemporalConstraintsValidator>();

builder.Services.AddKeyedScoped<IInvestmentReturnsCalculator, NPSInvestmentReturnsCalculator>(InvestmentInstrument.NPS);
builder.Services.AddKeyedScoped<IInvestmentReturnsCalculator, IndexInvestmentReturnsCalculator>(InvestmentInstrument.Index);


builder.Services.AddSingleton<PerformanceTimer>();

// Register the custom middleware explicitely as Transient for every request
builder.Services.AddTransient<PerformanceMiddleware>();

builder.Services.AddOpenApi();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Add custom middleware to the pipieline
app.UseMiddleware<PerformanceMiddleware>();

app.MapControllers();

app.Run();