using Microsoft.AspNetCore.Mvc;
using AAC.SelfServiceVSC.Models;
using AAC.SelfServiceVSC.Models.SCSAPI;
using AAC.SelfServiceVSC.Models.PaylinkAPI;
using AAC.SelfServiceVSC.Models.Form;
using AAC.Libraries;
using Elavon_Converge.Models;
using Microsoft.Extensions.FileProviders;

namespace SelfServiceVSC.Controllers
{

	[Route("[controller]")]
	public class EstimateController : Controller
	{
		private readonly SCSApiClient scs;

		public EstimateController(SCSApiClient scs)
		{
			this.scs = scs;
		}

		[HttpGet]
		public IActionResult Estimate()
		{
			return View();
		}

		// todo(rec): just use the built-in logging...
		// todo(rec): CSRF blocking? checking the status of the session to ensure it's valid, or redirecting
		[HttpPost]
		public async Task<JsonResult> Estimate([FromBody] EstimateRequest estimateRequest)
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();

			 sessionDetails.EstimateRequest = estimateRequest;

			var getRatesRequest = new GetRates
			{
				TpaCode = "AAC",
				UserId = AAC.SelfServiceVSC.SelfServiceVSC.AppSettings.SCSAPIUserId,
				Password = AAC.SelfServiceVSC.SelfServiceVSC.AppSettings.SCSAPIPassword,
				DealerNo = "DTC001",
				SaleDate = DateTime.Now,
				NewUsed = estimateRequest.NewUsed,
				VehicleOdometer = estimateRequest.MileageInt,
				FinancedAmount = 0m,
				FinanceTerm = 0,
				FinanceType = "B",
				FinanceApr = 0m,
				MSRP = 0m,
				NADA = 0m
			};

			// todo: do some validation and bail out if this is not filled in
			// to avoid hitting the API for an invalid request
			if (!String.IsNullOrWhiteSpace(estimateRequest.VIN))
			{
				getRatesRequest.VIN = estimateRequest.VIN;
			}
			else
			{
				// todo(rec): necessary? Vin is required on the form...
				// do we only actually WANT the vin? Might save the user some time.
				getRatesRequest.VehicleYear = estimateRequest.YearInt;
				getRatesRequest.VehicleMake = estimateRequest.Make;
				getRatesRequest.VehicleModel = estimateRequest.Model;
				getRatesRequest.Trim = "";
				getRatesRequest.TonRating = "";
				getRatesRequest.AspirationCode = "";
				getRatesRequest.DrivingWheels = "";
				getRatesRequest.SegmentationCode = "";
				getRatesRequest.Cylinders = "";
				getRatesRequest.FuelType = "";
				getRatesRequest.GVWR = "";
				getRatesRequest.AssetType = "";
				getRatesRequest.BodyStyle = "";
				getRatesRequest.EngineSize = 0;
				getRatesRequest.EngineSizeUnit = "";
				getRatesRequest.CompleteVehicle = "Y";
			}

			try
			{
				Logger.WriteLine("checking rates from SCSAPI...");

				var res = await scs.GetRates(getRatesRequest);
				if (res.ErrorResponse != null)
				{
					var err = res.ErrorResponse;
					Logger.WriteLine(string.Format("ERROR: {0} {1}", err.ErrorCode, err.ErrorDescription));

					return Json(false);
				}

				// todo(rec): maybe save some of these details down to a database in case it fails half-way through,
				// at least you could do some analysis, or learn what vehicles are being requested and so on
				Logger.WriteLine(string.Format("got quote id:{0} exp: {1}", res.QuoteID, res.QuoteExpiration));

				sessionDetails.EstimateId = res.QuoteID;

				if (res.ClientQuotes.Count == 0)
				{
					// what is the actual error condition here?
					// if we don't produce a selectable rate?
					return Json(false);
				}

				// this is a fancy getter that maps over some plan rates...
				// however this only works if the API response contains rates
				sessionDetails.SessionEstimates = res.ClientQuotes;
				sessionDetails.Rates = res.Rates;

				this.HttpContext.Session.SetSessionDetails(sessionDetails);
			}
			catch (Exception ex)
			{
				Logger.WriteError(ex);
				return Json(false);
			}

			return Json(true);
		}

		[HttpGet("/RetrieveEstimate")]
		public JsonResult RetrieveEstimate()
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();

			return Json(sessionDetails.SessionEstimates.Select(estimate => estimate.GetClientEstimate()));
		}

		[HttpPost("/SelectEstimate")]
		public JsonResult SelectEstimate([FromBody] SelectEstimate selectEstimate)
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();

			sessionDetails.SelectEstimate(selectEstimate.rateId);

			if (sessionDetails.SessionEstimates.Any(estimate => estimate.Selected))
			{
				this.HttpContext.Session.SetSessionDetails(sessionDetails);

				return Json(sessionDetails.SelectedSessionEstimate);
			}
			else
			{
				return Json(false);
			}
		}

		[HttpPost("/ChoosePaymentOption")]
		public JsonResult ChoosePaymentOption([FromBody] SelectEstimate selectEstimate)
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();

			sessionDetails.SelectEstimate(selectEstimate.rateId);

			if (sessionDetails.SessionEstimates.Any(estimate => estimate.Selected))
			{
				this.HttpContext.Session.SetSessionDetails(sessionDetails);

				return Json(sessionDetails.SelectedSessionEstimate);
			}
			else
			{
				return Json(false);
			}
		}

		[HttpPost("/CustomerDetails")]
		public JsonResult CustomerDetails([FromBody] QuoteRequest quoteRequest)
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			var estimate = sessionDetails.SelectedSessionEstimate;

			sessionDetails.QuoteRequest = quoteRequest;

			this.HttpContext.Session.SetSessionDetails(sessionDetails);
			return Json(sessionDetails.SelectedSessionEstimate);
		}

		[HttpGet("/contract-preview")]
		public async Task<FileResult> GetContractPreview()
		{
			var PDFBytes = new Byte[0];
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			var generateContract = new GenerateContract(sessionDetails);

			try
			{
				var responseObject = await scs.GenerateContract(generateContract);

				PDFBytes = Convert.FromBase64String(responseObject.ContractDocument);
			}
			catch (Exception ex)
			{
				Logger.WriteError(ex);
			}

			return File(PDFBytes, System.Net.Mime.MediaTypeNames.Application.Pdf, String.Format("DTC{0}.pdf", sessionDetails.EstimateRequest.VIN.Substring(sessionDetails.EstimateRequest.VIN.Length - 8)));
		}

		[HttpPost("/finalize")]
		public async Task<JsonResult> FinalizeContract(FinalizeContractRequest finalizeContract)
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();

			sessionDetails.FinalizeContract = finalizeContract;
		
			this.HttpContext.Session.SetSessionDetails(sessionDetails);

			var generateContract = new GenerateContract(sessionDetails)
			{
				FinalCopy = true
			};

			try
			{
				var scsContract = await scs.GenerateContract(generateContract);

				if ((scsContract?.ResponseCode ?? 0) == 0)
				{
					throw new Exception("Creating contract failed.");
				}

				sessionDetails.GeneratedContract = scsContract;

				this.HttpContext.Session.SetSessionDetails(sessionDetails);

				var contract = new Contract(sessionDetails);


				//var submitContractResult = await AAC.SelfServiceVSC.Models.PaylinkAPI.API.SubmitContract(contract);

				//if (submitContractResult == null || (submitContractResult.ValidationErrors?.Any() ?? false))
				//{
				//	var voidContract = new VoidContract(sessionDetails);
				//	var scsCancel = await scs.VoidContract(voidContract);

				//	throw new Exception("Creating loan failed. Canceled contract creation.");
				//}

				var PDFBytes = Convert.FromBase64String(scsContract.ContractDocument);
				var PDFStream = new MemoryStream(PDFBytes);
				var subject = "AAC Service Contract: Number " + scsContract.ContractNumber;
				var body = "Thank you for choosing American Assurance Corporation for your vehicle service contract needs!\n\n" +
					"Contract details:\n\n" +
					"\tName: " + sessionDetails.QuoteRequest.FullName + "\n\n" +
					"\tPhone: " + sessionDetails.QuoteRequest.PhoneNumber + "\n\n" +
					"\tContract Number: " + scsContract.ContractNumber + "\n\n" +
					"\tVehicle Make: " + sessionDetails.EstimateRequest.Make + "\n\n" +
					"\tVehicle Model: " + sessionDetails.EstimateRequest.Model + "\n\n" +
					"\tVIN: " + sessionDetails.EstimateRequest.VIN + "\n\n" +
					"\tOdometer: " + sessionDetails.EstimateRequest.MileageInt + "\n\n" +
					"";
				var filePath =
					new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{sessionDetails.OdometerImage}";

				var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

				//Mail.Send(
				//	to: sessionDetails.QuoteRequest.Email,
				//	toName: sessionDetails.QuoteRequest.FullName,
				//	subject,
				//	body,
				//	attachmentStreams: new Stream[] { PDFStream },
				//	attachmentNames: new String[] { scsContract.ContractNumber + ".pdf" }
				//);

				//Mail.Send(
				//	"programming@aacwarranty.com",
				//	"AAC",
				//	subject,
				//	body,
				//	new Stream[] { PDFStream, imageStream },
				//	new String[] { scsContract.ContractNumber + ".pdf", sessionDetails.OdometerImage});

				sessionDetails.Reset();

				return Json(true);
			}
			catch (Exception ex)
			{
				Logger.WriteError(ex);
				return Json(false);
			}
		}

		[HttpPost("/GetToken")]
		public async Task<IActionResult> GetToken([FromBody] TokenRequestModel tokenRequest)
		{

			// Implement your server-side logic to obtain the session token
			// You can use the ConvergeApiService or another method to make the API call

			// For demonstration purposes, returning a dummy session token

			var sessionDetails = this.HttpContext.Session.GetSessionDetails();

			var sslAmount = sessionDetails.SelectedSessionEstimate.RetailRate * 0.9m;
			ConvergeApiService service = new ConvergeApiService();
			var Token = await service.GetConvergeSessionTokenAsync(tokenRequest.FirstName, tokenRequest.LastName, sslAmount);
			
			var Data = new
			{
				QuoteRequest = sessionDetails.QuoteRequest,
				token = Token
			};
		
			return new JsonResult(Data);
		}

		[HttpPost("/SaveFile")]
		public IActionResult Index(IFormFile file)
		{
			if (file != null)
			{
				if (file.Length > 0)
				{
					//Getting FileName
					var fileName = Path.GetFileName(file.FileName);

					//Assigning Unique Filename (Guid)
					var myUniqueFileName = Convert.ToString(Guid.NewGuid());

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
					var sessionDetails = this.HttpContext.Session.GetSessionDetails();
					sessionDetails.OdometerImage = newFileName;
					this.HttpContext.Session.SetSessionDetails(sessionDetails);
				}
			}
			return Json(new { message = "Upload successful!" });
		}
	}
}
