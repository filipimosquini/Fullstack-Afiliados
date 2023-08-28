using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net;

namespace Backend.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionInDevelopmentEnvironmentMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionInDevelopmentEnvironmentMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var resultObj = new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", new[] { "We apologise by inconvenience, something is wrong but we are investigate and soon as possible, all will be working" } }
        });

        var contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        switch (exception)
        {
            case Exception e:
            default:
                resultObj.Status = (int)HttpStatusCode.InternalServerError;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var result = JsonConvert.SerializeObject(resultObj, new JsonSerializerSettings()
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        });

        _logger.LogError(result);
        return response.WriteAsync(result);
    }
}