using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AAC.SelfServiceVSC.Controllers
{
	public class TwilioErrorWebhookController : ControllerBase
	{
		[HttpPost]
		public IActionResult Post([FromBody] JObject data)
		{
			// Extracting data from the request
			string accountSid = data["AccountSid"]?.ToString();
			string sid = data["Sid"]?.ToString();
			string parentAccountSid = data["ParentAccountSid"]?.ToString();
			string timestamp = data["Timestamp"]?.ToString();
			string level = data["Level"]?.ToString();
			string payloadType = data["PayloadType"]?.ToString();
			string payload = data["Payload"]?.ToString();

			// Perform any necessary processing with the received data

			// Return a response
			var response = new
			{
				message = "Debugger event received successfully",
				accountSid,
				sid,
				parentAccountSid,
				timestamp,
				level,
				payloadType,
				payload
			};

			return Ok(response);
		}
	}
}



