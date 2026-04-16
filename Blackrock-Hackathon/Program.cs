using Blackrock_Hackathon.Interfaces;
using BlackRock_Hackathon.Enums;
using BlackRock_Hackathon.Middlewares;
using BlackRock_Hackathon.Services;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ITransactionBuilder, TransactionBuilder>();
builder.Services.AddScoped<ITransactionValidator, TransactionValidator>();
builder.Services.AddScoped<ITemporalConstraintsValidator, TemporalConstraintsValidator>();

builder.Services.AddKeyedScoped<IInvestmentReturnsCalculator, NPSInvestmentReturnsCalculator>(InvestmentInstrument.NPS);
builder.Services.AddKeyedScoped<IInvestmentReturnsCalculator, IndexInvestmentReturnsCalculator>(InvestmentInstrument.Index);


builder.Services.AddSingleton<PerformanceTimer>();

// Register the custom middleware explicitly as Transient for every request
builder.Services.AddTransient<PerformanceMiddleware>();

builder.Services.AddOpenApi();

WebApplication app = builder.Build();

// NOTE : Part of hackathon challenge, so kept public
app.MapOpenApi();
app.MapScalarApiReference();

// Automatically redirect to Scalar documentation
app.MapGet("/", () => Results.Redirect("/scalar"));

// Add custom middleware to the pipeline
app.UseMiddleware<PerformanceMiddleware>();

app.MapControllers();

app.Run();