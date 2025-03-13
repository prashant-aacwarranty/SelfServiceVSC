using AAC.SelfServiceVSC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace AAC.SelfServiceVSC.Controllers
{
	public class SwitchToPhoneController : Controller
	{
		public IActionResult PhonePage(string value1, string value2)
		{
			// Use the values as needed
			ViewBag.Value1 = value1;
			ViewBag.Value2 = value2;
			return View();
		}


		[HttpPost("/SaveFileFromPhone")]
		public IActionResult SaveFile(IFormFile file, string vin)
		{
			if (file == null)
			{
				return Json(new { message = "Please upload a file first." });
			}
			if (file.Length > 0)
			{
				//Getting FileName
				var fileName = Path.GetFileName(file.FileName);

				//Assigning Unique Filename (Guid)
				var myUniqueFileName = vin;

				//Getting file Extension
				var fileExtension = Path.GetExtension(fileName);

				// concatenating  FileName + FileExtension
				var newFileName = String.Concat(myUniqueFileName, fileExtension);

				// Combines two strings into a path.
				var filepath =
				new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newFileName}";

				using (FileStream fs = System.IO.File.Create(filepath))
				{
					file.CopyTo(fs);
					fs.Flush();
				}

				return Json(new { message = "Upload successful!" });
			}

			return Json(new { message = "File is empty." });
		}
	}
}
