using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Timelogger.Api.Dtos;
using Timelogger.Exceptions;

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
            try
            {
                await _next(httpContext);
            }
            catch (BadRequestException ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.NotFound);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionAsync(httpContext, ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, IEnumerable<ValidationFailure> failures)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = failures.ToDictionary(x => x.PropertyName, x => new[] { x.ErrorMessage });

            var response = JsonSerializer.Serialize(new ExceptionDto
            {
                Message = "There are validations errors",
                StatusCode = context.Response.StatusCode,
                Errors = errors,
                TraceId = context.TraceIdentifier
            });

            return context.Response.WriteAsync(response);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionDto
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                TraceId = context.TraceIdentifier
            }));
        }
    }
}
