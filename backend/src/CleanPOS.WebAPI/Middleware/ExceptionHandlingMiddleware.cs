// src/CleanPOS.WebAPI/Middleware/ExceptionHandlingMiddleware.cs
namespace CleanPOS.WebAPI.Middleware;

using CleanPOS.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var (statusCode, message, errors) = exception switch
        {
            NotFoundException ex =>
                (HttpStatusCode.NotFound, ex.Message, (object?)null),

            ValidationException ex =>
                (HttpStatusCode.UnprocessableEntity, ex.Message, ex.Errors),

            DuplicateNameException ex =>
                (HttpStatusCode.Conflict, ex.Message, (object?)null),

            _ => (HttpStatusCode.InternalServerError,
                  "An unexpected error occurred.", (object?)null)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            status = (int)statusCode,
            message,
            errors
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}