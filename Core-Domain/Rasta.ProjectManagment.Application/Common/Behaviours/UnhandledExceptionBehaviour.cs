using MediatR;
using Microsoft.Extensions.Logging;
using org.jsoup.helper;
using System.Text;

namespace Rasta.ProjectManagment.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            if (ex is Exceptions.ValidationException)
            {
                var errors = ((Exceptions.ValidationException)ex).Errors;
                if (errors.Any())
                {
                    StringBuilder stringBuilder = new();
                    stringBuilder.Append("پارامترهای ورودی صحیح نمی باشند" + Environment.NewLine);
                    foreach (var error in errors)
                    {
                        stringBuilder.Append($"Property: {error.Key} Error Code: {string.Join(",", error.Value)}" + Environment.NewLine);
                    }
                    _logger.LogError(stringBuilder.ToString());
                    throw new Exception(stringBuilder.ToString());
                }
            }
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "Rasta.ProjectManagment Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}
