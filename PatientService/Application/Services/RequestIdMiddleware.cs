using Serilog.Context;

namespace PatientService.Application.Services
{
	public class RequestIdMiddleware
	{
		private readonly RequestDelegate _next;

		public RequestIdMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var requestId = Guid.NewGuid().ToString();
			context.Items["RequestId"] = requestId;

			using (LogContext.PushProperty("RequestId", requestId))
			{
				await _next(context);
			}
		}
	}
}