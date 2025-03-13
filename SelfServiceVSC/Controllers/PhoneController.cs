using AAC.SelfServiceVSC.Models.Form;
using AAC.SelfServiceVSC.Models.SCSAPI;
using AAC.SelfServiceVSC.Models;
using Microsoft.AspNetCore.Mvc;
using Twilio.TwiML.Voice;
using AAC.Libraries;
using Microsoft.EntityFrameworkCore;
using SelfServiceVSC.DAL;
using Customer = SelfServiceVSC.DAL.Customer;

namespace AAC.SelfServiceVSC.Controllers
{
	public class PhoneController : Controller
	{
		private readonly SCSApiClient scs;
		public PhoneController(SCSApiClient scs)
		{
			this.scs = scs;
		}
		public IActionResult Index()
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			string vinToCheck = sessionDetails.EstimateRequest.VIN;
			//DAL.Customer Customer = dbContext.Customers.Find(sessionDetails.EstimateRequest.VIN);
			BestpriceprotectionContext dbContext = new BestpriceprotectionContext();
			//Customer customer = dbContext.Customers.FirstOrDefault(c => c.Vin == sessionDetails.EstimateRequest.VIN);
			DateTime twentyFourHoursAgo = DateTime.Now.AddHours(-24);

			Customer customer = dbContext.Customers
					.FirstOrDefault(c => c.Vin == vinToCheck &&
																c.IsPurchased == false &&
																c.CreatedDate >= twentyFourHoursAgo);
			sessionDetails.SelectEstimate(customer.RateId);
			sessionDetails.RateId = (int)customer.RateId;
			sessionDetails.CustomerId = customer.Id; 
			if (sessionDetails.SessionEstimates.Any(estimate => estimate.Selected))
			{
				this.HttpContext.Session.SetSessionDetails(sessionDetails);

				//return Json(sessionDetails.SelectedSessionEstimate);
			}
			else
			{
				//return Json(false);
			}
			QuoteRequest quoteRequest = new QuoteRequest()
			{FirstName = customer.FirstName,
			LastName = customer.LastName,
			Email = customer.Email,
			Phone = customer.Phone,
			Address1 = customer.Street1,
			Address2 = customer.Street2,
			City = customer.City,
			State = customer.State,
			Zip = customer.Zip
			};
			sessionDetails.QuoteRequest = quoteRequest;
			this.HttpContext.Session.SetSessionDetails(sessionDetails);
			return View("Phone");
		}
		
		[HttpPost]
		public async Task<JsonResult> Estimate([FromBody] EstimateRequest estimateRequest)
		{
			this.HttpContext.Session.Clear();
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

			// todo: do some validation and bail out if this is not filled innnbn
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

				//var options = new JsonSerializerOptions
				//{
				//	PropertyNamingPolicy = null,
				//	Converters =
				//{
				//	new JsonStringEnumConverter()
				//},
				//	//Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
				//};
				//var json1 = JsonSerializer.Serialize(getRatesRequest, typeof(GetRates), options);

				//Logger.WriteLine("Request JSON:\n"+ json1);

				var res = await scs.GetRates(getRatesRequest);

				//var json2 = JsonSerializer.Serialize(res, typeof(GetRatesResponse), options);
				//Logger.WriteLine("Response JSON:\n" + json2);

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

		

		

	}
}
