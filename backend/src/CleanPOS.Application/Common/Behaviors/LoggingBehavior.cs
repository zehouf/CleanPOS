// src/CleanPOS.Application/Common/Behaviors/LoggingBehavior.cs
namespace CleanPOS.Application.Common.Behaviors;

using MediatR;
using Microsoft.Extensions.Logging;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation(
            "CleanPOS Request: {RequestName} {@Request}",
            requestName, request);

        var response = await next();

        _logger.LogInformation(
            "CleanPOS Response: {RequestName} completed",
            requestName);

        return response;
    }
}