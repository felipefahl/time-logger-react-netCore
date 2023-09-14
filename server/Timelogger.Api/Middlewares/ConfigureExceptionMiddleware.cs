using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timelogger.Api.Middlewares
{
    public class ConfigureExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ConfigureExceptionMiddleware> _logger;

        public ConfigureExceptionMiddleware(RequestDelegate next, ILogger<ConfigureExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //try
            //{
            //    await _next(httpContext);
            //}
            //catch (DomainException ex)
            //{
            //    _logger.LogError(ex);
            //    await HandleExceptionAsync(httpContext, ex);
            //}
            //catch (AggregateException ex)
            //{
            //    IList<DomainException> exceptions = new List<DomainException>();

            //    ex.Handle(x =>
            //    {
            //        if (x is DomainException)
            //            exceptions.Add((DomainException)x);

            //        return x is DomainException;
            //    });

            //    _logger.LogError(ex);
            //    await HandleExceptionAsync(httpContext, exceptions.ToArray());
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex);
            //    await HandleExceptionAsync(httpContext, ex);
            //}
        }

        //private Task HandleExceptionAsync(HttpContext context, params DomainException[] exceptions)
        //{
        //    //context.Response.ContentType = "application/json";
        //    //context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        //    //var errors = exceptions.SelectMany(x => x.DomainNotifications).ToDictionary(x => x.Key, x => new[] { x.Value });

        //    //var response = JsonSerializer.Serialize(new DomainExceptionViewModel(
        //    //    context.Response.StatusCode,
        //    //    errors,
        //    //    context.TraceIdentifier));

        //    //Elastic.Apm.Agent.Tracer.CurrentTransaction?.SetLabel("Response.Body", JsonConvert.SerializeObject(response));

        //    //return context.Response.WriteAsync(response);
        //}

        //private Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    //context.Response.ContentType = "application/json";
        //    //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        //    //return context.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionViewModel()
        //    //{
        //    //    StatusCode = context.Response.StatusCode,
        //    //    Message = LanguageResource.ServerWasUnableToProcessTheRequest + " - " + exception.Message,
        //    //    TraceId = context.TraceIdentifier
        //    //}));
        //}
    }
}
