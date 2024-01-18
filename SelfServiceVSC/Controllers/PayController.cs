using Microsoft.AspNetCore.Mvc;
using AAC.SelfServiceVSC.Models;
using AAC.SelfServiceVSC.Models.Form;

namespace SelfServiceVSC.Controllers
{
	[Route("[controller]")]
	public class PayController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("/single/{id}")]
		public IActionResult SinglePay(string id)
		{
			var session = this.HttpContext.Session.GetSessionDetails();
			var info = new SinglePayViewModel();
			if (AAC.SelfServiceVSC.SelfServiceVSC.AppSettings.IsDevelopment)
			{
				if (id == "test")
				{

				}
			}

			return View("SinglePay");
		}

		[HttpGet("/recurring")]
		public IActionResult Payment()
		{
			return View();
		}

		[HttpGet("/payment-details")]
		public IActionResult Details()
		{
			return View();
		}

		[HttpGet("/payment-plans")]
		public IActionResult Plans()
		{
			return View();
		}

		[HttpPost("/payment-details")]
		public JsonResult PaymentsDetails([FromBody] PaymentRequest paymentRequest)
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			var estimate = sessionDetails.SelectedSessionEstimate;
			sessionDetails.PaymentRequest = paymentRequest;
			this.HttpContext.Session.SetSessionDetails(sessionDetails);
			return Json(sessionDetails.SelectedSessionEstimate);
		}
	}
}
