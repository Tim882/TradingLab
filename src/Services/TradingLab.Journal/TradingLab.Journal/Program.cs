using Microsoft.EntityFrameworkCore;
using TradingLab.Journal.Application.Interfaces;
using TradingLab.Journal.Application.Services;
using TradingLab.Journal.Domain.Interfaces.Repositories;
using TradingLab.Journal.Infrastructure.Data.Context;
using TradingLab.Journal.Infrastructure.Data.Repositories;
using FluentValidation;
using TradingLab.Journal.Application.Validators;
using TradingLab.Journal.Middleware;
using EFCore.NamingConventions;
using System;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddValidatorsFromAssemblyContaining<TagRequestValidator>();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<JournalDbContext>(options => options.UseNpgsql(connection, npgsqlOptions =>
{
    npgsqlOptions.EnableRetryOnFailure(
        maxRetryCount: 3,
        maxRetryDelay: TimeSpan.FromSeconds(10),
        errorCodesToAdd: null);
    npgsqlOptions.CommandTimeout(30);
}).UseSnakeCaseNamingConvention());
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJournalEntryDataService, JournalEntryDataService>();
builder.Services.AddScoped<IPositionDataService, PositionDataService>();
builder.Services.AddScoped<ITagDataService, TagDataService>();
builder.Services.AddScoped<ITradeDataService, TradeDataService>();
builder.Services.AddScoped<ITradeNoteDataService, TradeNoteDataService>();
builder.Services.AddScoped<ITradingAccountDataService, TradingAccountDataService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<JournalDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    for (int i = 0; i < 10; i++)
    {
        try
        {
            if (db.Database.CanConnect())
            {
                logger.LogInformation("Applying migrations...");
                db.Database.Migrate();
                logger.LogInformation("Migrations applied successfully");
                break;
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Waiting for database (attempt {Attempt}/10)...", i + 1);
            Thread.Sleep(3000);
        }
    }
}

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();