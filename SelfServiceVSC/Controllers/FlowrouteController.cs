using AAC.SelfServiceVSC.Models;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Twilio.Types;



namespace AAC.SelfServiceVSC.Controllers
{

	public class FlowrouteController : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> SendMessage(string url)
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			string numericPhoneNumber = Regex.Replace(sessionDetails.QuoteRequest.Phone, @"\D", "");

			// Check if the phone number starts with a valid country code
			if (!numericPhoneNumber.StartsWith("1"))
			{
				// Add "1" as the country code
				numericPhoneNumber = "1" + numericPhoneNumber;
			}
			var to = new PhoneNumber(numericPhoneNumber);
			var fullUrl = "https://www.bestpriceprotection.com" + url;
			var body = "Please upload current odometer reading of your vehicle at " + fullUrl;
			// JSON payload for the POST request
			string jsonPayload = $@"
            {{
                ""data"": {{
                    ""type"": ""message"",
                    ""attributes"": {{
                        ""to"": ""{to}"",
                        ""from"": ""17203437299"",
                        ""body"": ""{body}""
                    }}
                }}
            }}";

			// Credentials for basic authentication
			string username = AAC.SelfServiceVSC.SelfServiceVSC.AppSettings.FlowrouteUsername;
			string password = AAC.SelfServiceVSC.SelfServiceVSC.AppSettings.FlowroutePassword;

			using (var httpClient = new HttpClient())
			{
				// Set base URL of the Flowroute API
				string baseUrl = "https://api.flowroute.com/v2.1/messages";

				// Set basic authentication header
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
						"Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

				// Prepare HTTP request message
				var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
				request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

				try
				{
					// Send the HTTP request
					HttpResponseMessage response = await httpClient.SendAsync(request);

					// Check if the request was successful
					if (response.IsSuccessStatusCode)
					{
						// If successful, return the response content
						string responseData = await response.Content.ReadAsStringAsync();

						Mail.Send(
						to: "test01252024@gmail.com",
						toName: "Test",
						subject: "testing whether correct url sent ",
						body,
						attachmentStreams: new Stream[] {  },
						attachmentNames: new String[] { "Paylink Service Contract" + ".pdf" });

						return Ok(responseData);
					}
					else
					{
						string responseData = await response.Content.ReadAsStringAsync();
						Console.WriteLine(responseData);
						// If not successful, return the status code
						return StatusCode((int)response.StatusCode);
					}
				}
				catch (Exception ex)
				{
					// If an exception occurs, return Internal Server Error
					Console.WriteLine($"An error occurred: {ex.Message}");
					return StatusCode(500, "An error occurred while processing the request.");
				}
			}
		}
	}
}
