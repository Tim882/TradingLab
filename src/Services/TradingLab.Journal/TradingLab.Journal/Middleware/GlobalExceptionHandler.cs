using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TradingLab.Journal.Domain.Exceptions;
using FluentValidation;

namespace TradingLab.Journal.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var (statusCode, title, detail, errorCode, extensions) = MapException(exception);

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Type = $"https://api.yourdomain.com/errors/{errorCode.ToLower()}",
                Instance = httpContext.Request.Path
            };

            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
            problemDetails.Extensions["errorCode"] = errorCode;
            problemDetails.Extensions["traceId"] = traceId;
            problemDetails.Extensions["timestamp"] = DateTime.UtcNow;

            foreach (var (key, value) in extensions)
                problemDetails.Extensions[key] = value;

            LogException(exception, statusCode, errorCode, traceId);

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private (int StatusCode, string Title, string Detail, string ErrorCode, Dictionary<string, object> Extensions)
            MapException(Exception exception)
        {
            var isDevelopment = _env.IsDevelopment();

            return exception switch
            {
                NotFoundException ex => (
                    404, "Resource Not Found", ex.Message, "NOT_FOUND", new()),

                ValidationException fluentEx => (
                    400, "Validation Failed", "One or more validation errors occurred",
                    "VALIDATION_ERROR",
                    new()
                    {
                        ["errors"] = fluentEx.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray())
                    }),

                BusinessRuleViolationException ex => (
                    422, "Business Rule Violation", ex.Message, "BUSINESS_RULE_VIOLATION", new()),

                DbUpdateException ex => (
                    500, "Database Error",
                    isDevelopment ? ex.Message : "A database error occurred",
                    "DATABASE_ERROR", new()),

                _ => (
                    500, "Internal Server Error",
                    isDevelopment ? exception.Message : "An unexpected error occurred",
                    "INTERNAL_ERROR", new())
            };
        }

        private void LogException(Exception exception, int statusCode, string errorCode, string traceId)
        {
            var logLevel = statusCode >= 500 ? LogLevel.Error : LogLevel.Warning;

            _logger.Log(logLevel, exception,
                "HTTP {StatusCode} | {ErrorCode} | {Message} | TraceId: {TraceId}",
                statusCode, errorCode, exception.Message, traceId);
        }
    }
}

