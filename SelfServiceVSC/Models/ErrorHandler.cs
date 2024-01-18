using System.Net;
using AAC.Libraries;

namespace AAC.SelfServiceVSC.Models
{
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception error)
			{
				Logger.WriteError(error);

				var response = context.Response;
				response.ContentType = "application/json";

				switch (error)
				{
					case AppException e:
						// custom application error
						response.StatusCode = (Int32)HttpStatusCode.BadRequest;
						break;
					case KeyNotFoundException e:
						// not found error
						response.StatusCode = (Int32)HttpStatusCode.NotFound;
						break;
					default:
						// unhandled error
						response.StatusCode = (Int32)HttpStatusCode.InternalServerError;
						break;
				}

				// avoid leaking error details to the client
				var message = SelfServiceVSC.AppSettings.IsDevelopment
					? error?.Message
					: "an error occurred processing the request";

				await response.WriteAsJsonAsync(new { ok = false, message });
			}
		}
	}
}
