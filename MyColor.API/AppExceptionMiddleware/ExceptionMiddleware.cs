using Microsoft.AspNetCore.Http;
using MyColor.Application.ApplicationException;
using MyColor.Domain.Interfaces;
using MyColor.Domain.Validation;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MyColor.API.AppExceptionMiddleware
{
    public sealed class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILoggerService _logger;

		public ExceptionMiddleware(RequestDelegate next, ILoggerService logger)
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
			catch (Exception e)
			{
				_logger.LogError($"Something went wrong: {e}");
				await HandleExceptionAsync(httpContext, e);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = exception switch
			{
				DomainExceptionValidation => (int)HttpStatusCode.BadRequest,
				AppServiceException => (int)HttpStatusCode.BadRequest,
				ApplicationException => (int)HttpStatusCode.BadRequest,
                ArgumentException => (int)HttpStatusCode.NotFound,
				_ => (int)HttpStatusCode.InternalServerError
			};

			await context.Response.WriteAsync(new ErrorDetails()
			{
				Status = context.Response.StatusCode,
				Message = exception.Message
			}.ToString());
		}
	}
}
