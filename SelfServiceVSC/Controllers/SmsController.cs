using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using AAC.SelfServiceVSC.Models;

namespace AAC.SelfServiceVSC.Controllers
{
	public class SmsController : Controller
	{
		public ActionResult SendSms(string url)
		{
			try
			{
				var sessionDetails = this.HttpContext.Session.GetSessionDetails();
				var accountSid = "AC05b24db730cefe5abf3cc0761ccb259a";
				var authToken = "337b21bd678fb250560b8df8b257c939";
				TwilioClient.Init(accountSid, authToken);

				var fullUrl = "https://www.bestpriceprotection.com" + url;

				// Construct the message body with the full URL
				var messageBody = "Please upload current odometer reading of your vehicle at " + fullUrl;

				var to = new PhoneNumber(sessionDetails.QuoteRequest.Phone);
				var from = new PhoneNumber("+16203712378");

				var message = MessageResource.Create(
						to: to,
						from: from,
						body: messageBody);

				return new OkObjectResult(message.Sid);
			}
			catch (Exception ex)
			{
				// Log the error
				Console.WriteLine("Error sending SMS: " + ex.Message);

				// Optionally, return a meaningful error response
				return new StatusCodeResult(500); // Internal Server Error
			}
		}
	}
}
