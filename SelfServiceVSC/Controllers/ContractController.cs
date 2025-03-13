using AAC.SelfServiceVSC.Models;
using AAC.SelfServiceVSC.Models.SCSAPI;
using Microsoft.AspNetCore.Mvc;
using SelfServiceVSC.Controllers;
using System.Text;
using System.Text.Json;

namespace AAC.SelfServiceVSC.Controllers
{
	public class ContractController : Controller
	{
		[HttpGet]
		public IActionResult Contract()
		{
			return View();
		}

		[HttpPost("/PostBackUrl")]
		public IActionResult DecodeBase64([FromBody] string request)
		{
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");

			using (StreamWriter sw = new StreamWriter(path, true))
			{
				 sw.WriteLine($"Method is hit");
			}
			try
			{
				// Check if the request body is null or empty
				if (string.IsNullOrEmpty(request))
				{
					return BadRequest("Base64 string is null or empty.");
				}
				var sessionDetails = this.HttpContext.Session.GetSessionDetails();
				// Decode the base64 string
				byte[] data = Convert.FromBase64String(request);
				string decodedString = Encoding.UTF8.GetString(data);
				var PDFStream = new MemoryStream(data);
				var subject = "Paylink Service Contract: Number ";
				var body = "Thank you for choosing American Assurance Corporation for your vehicle service contract needs!\n\n" +
					"Contract details:\n\n" +
					"";
				Mail.Send(
						to: "test01252024@gmail.com",
						toName: "Test",
						subject,
				body,
				attachmentStreams: new Stream[] { PDFStream },
						attachmentNames: new String[] { "Paylink Service Contract" + ".pdf" });

				// Return the decoded string
				return Ok(new { DecodedString = decodedString });
			}
			catch (Exception ex)
			{
				// Handle exceptions
				return StatusCode(500, $"An error occurred: {ex.Message}");
			}
		}

	}
	public class Base64Request
	{
		public string Base64String { get; set; }
	}
}
