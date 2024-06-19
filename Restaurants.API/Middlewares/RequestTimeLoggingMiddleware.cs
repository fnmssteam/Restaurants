
using System.Diagnostics;

namespace Restaurants.API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger logger) : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		var s = Stopwatch.StartNew();

		await next.Invoke(context);

		s.Stop();

		// If elapse time is 4 seconds
		if (s.ElapsedMilliseconds > 4000)
		{
			logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms",
				context.Request.Method,
				context.Request.Path,
				s.ElapsedMilliseconds);
		}
	}
}
