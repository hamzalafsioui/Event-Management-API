using EventManagement.Core.Bases;
using EventManagement.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace EventManagement.Core.Middleware
{
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlerMiddleware(RequestDelegate next)
		{
			this._next = next;
		}
		private Response<string> CreateErrorResponse(Exception error, HttpStatusCode statusCode, string message = null!)
		{
			return new Response<string>
			{
				Succeded = false,
				Message = message ?? error.Message,
				StatusCode = statusCode
			};
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception error)
			{
				var response = context.Response;
				response.ContentType = "application/json";

				// Get the client's IP Address
				var clientIP = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
				// Generate a Correlation ID for tracing and include it in the response headers
				var correlationId = Guid.NewGuid().ToString();
				response?.Headers?.Add("X-Correlation-ID", correlationId);

				// Log the error unless it's a FluentValidation.ValidationException				
				if (error is not FluentValidation.ValidationException)
					Log.Error(error, $"Unhandled Exception: Correlation ID: {correlationId},Client IP: {clientIP}, Message: {error.Message}");


				var responseModel = new Response<string>()
				{
					Succeded = false,
					Message = "An unexpected error occurred. Please contact support.", // Initial value
					StatusCode = HttpStatusCode.InternalServerError // Initial value
				};


				// Handle specific exception types
				switch (error)
				{
					// For unauthorized access attempts
					case UnauthorizedAccessException exception:
						responseModel = CreateErrorResponse(error, HttpStatusCode.Unauthorized, error.Message);
						response!.StatusCode = (int)HttpStatusCode.Unauthorized;
						break;

					// For validation issues, like data format or rules violations
					case ValidationException exception:
						responseModel = CreateErrorResponse(error, HttpStatusCode.UnprocessableEntity, error.Message);
						response!.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
						break;

					// For resource not found scenarios
					case KeyNotFoundException exception:
						responseModel = CreateErrorResponse(error, HttpStatusCode.NotFound, error.Message);
						response!.StatusCode = (int)HttpStatusCode.NotFound;
						break;

					// For database update failures
					case DbUpdateException exception:
						responseModel = CreateErrorResponse(error, HttpStatusCode.BadRequest, exception.Message);
						responseModel.StatusCode = HttpStatusCode.BadRequest;
						break;

					// For specific API-related exceptions
					case Exception exception when exception.GetType().ToString() == "ApiException":
						var apiMessage = exception.Message + (exception.InnerException != null ? $"\n{exception.InnerException.Message}" : ""); ;
						responseModel = CreateErrorResponse(error, HttpStatusCode.BadRequest, apiMessage);
						response!.StatusCode = (int)HttpStatusCode.BadRequest;
						break;


					// For FluentValidation errors (detailed validation feedback)
					case FluentValidation.ValidationException validationException:
						responseModel = CreateErrorResponse(validationException, HttpStatusCode.BadRequest, validationException.Message);
						responseModel.Errors = validationException.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();
						response!.StatusCode = (int)HttpStatusCode.BadRequest;
						break;

					// For asynchronous task-related errors
					case AggregateException aggregateException:
						var aggregateMessage = string.Join(" | ", aggregateException.InnerExceptions.Select(e => e.Message));
						responseModel = CreateErrorResponse(aggregateException, HttpStatusCode.InternalServerError, aggregateMessage);
						response!.StatusCode = (int)HttpStatusCode.InternalServerError;
						break;

					// For catching issues with method arguments or invalid data passed to services
					case ArgumentException argumentException:
						responseModel = CreateErrorResponse(argumentException, HttpStatusCode.BadRequest, argumentException.Message);
						response!.StatusCode = (int)HttpStatusCode.BadRequest;
						break;

					// For operation cancellations, such as a canceled task (when an operation is canceled)
					case OperationCanceledException canceledException:
						responseModel = CreateErrorResponse(canceledException, HttpStatusCode.BadRequest, "The operation was canceled.");
						response!.StatusCode = (int)HttpStatusCode.BadRequest;
						break;

					// For timeout errors during database or network operations
					case TimeoutException timeoutException:
						responseModel = CreateErrorResponse(timeoutException, HttpStatusCode.RequestTimeout, "The request timed out. Please try again.");
						response!.StatusCode = (int)HttpStatusCode.RequestTimeout;
						break;

					// For Rate limiting
					case TooManyRequestsException tooManyRequestsException:
						responseModel = CreateErrorResponse(tooManyRequestsException, HttpStatusCode.TooManyRequests, tooManyRequestsException.Message);
						response!.StatusCode = (int)HttpStatusCode.TooManyRequests;
						break;

					// For security token expiration (authentication issues)
					case SecurityTokenExpiredException expiredTokenException:
						responseModel = CreateErrorResponse(expiredTokenException, HttpStatusCode.Unauthorized, "Your session has expired. Please log in again.");
						response!.StatusCode = (int)HttpStatusCode.Unauthorized;
						break;

					// For JSON parsing errors
					case JsonException jsonException:
						responseModel = CreateErrorResponse(jsonException, HttpStatusCode.BadRequest, "Invalid JSON format.");
						response!.StatusCode = (int)HttpStatusCode.BadRequest;
						break;

					case InvalidOperationException invalidOpEx when invalidOpEx.Message.Contains("policy", StringComparison.OrdinalIgnoreCase):
						responseModel = CreateErrorResponse(invalidOpEx, HttpStatusCode.Forbidden, $"Access denied. Required policy: {invalidOpEx.Message}");
						response!.StatusCode = (int)HttpStatusCode.Forbidden;
						break;


					default:
						// unhandled error
						responseModel = CreateErrorResponse(error, HttpStatusCode.InternalServerError, "Please contact the application administrator.");
						response!.StatusCode = (int)HttpStatusCode.InternalServerError;
						break;
				}
				// Serialize and write the response model
				var options = new JsonSerializerOptions()
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					WriteIndented = true
				};
				var result = JsonSerializer.Serialize(responseModel, options);
				await response!.WriteAsync(result);
			}
		}
	}
}
