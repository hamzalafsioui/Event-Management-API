using EventManagement.Core.Exceptions;
using EventManagement.Data.Helper;
using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;

namespace EventManagement.Core.Middleware
{
	public class RateLimitingMiddleware
	{
		#region Fields
		private readonly RequestDelegate _next;
		private readonly ConcurrentDictionary<string, (DateTime LastRequestTime, int RequestCount)> _clientRequests = new();
		private readonly RateLimiting _rateLimiting;
		#endregion
		#region Constructors
		public RateLimitingMiddleware(RequestDelegate next, RateLimiting rateLimiting)
		{
			_next = next;
			_rateLimiting = rateLimiting;
		}
		#endregion

		#region Actions
		public async Task Invoke(HttpContext context)
		{
			var clientIdentifier = GetClientIdentifier(context);
			var currentTime = DateTime.UtcNow;


			var clientData = _clientRequests.GetOrAdd(clientIdentifier, _ => (LastRequestTime: currentTime, RequestCount: 0));

			if ((currentTime - clientData.LastRequestTime) > TimeSpan.FromSeconds(_rateLimiting.TimeWindowInSeconds))
			{
				// Reset the counter if the time window has passed
				_clientRequests[clientIdentifier] = (LastRequestTime: currentTime, RequestCount: 1);
				await _next(context);
			}
			else
			{
				// Increment the request count & Update the last Request Time
				clientData.RequestCount++;
				if (clientData.RequestCount > _rateLimiting.Limit)
				{
					throw new TooManyRequestsException();
				}
				else
				{
					_clientRequests[clientIdentifier] = (LastRequestTime: currentTime, RequestCount: clientData.RequestCount);
					await _next(context);
				}
			}
		}

		private string GetClientIdentifier(HttpContext context)
		{
			// Identify clients based on IP address (or another identifier)
			return context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
		}
		#endregion
	}

}

