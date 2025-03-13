using Microsoft.AspNetCore.Mvc;
using AAC.SelfServiceVSC.Models;
using System.Diagnostics;

namespace SelfServiceVSC.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult TermsAndConditions()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel
			{
				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
			});
		}
	}
}
