using Microsoft.AspNetCore.Mvc;
using AAC.SelfServiceVSC.Models;
using AAC.SelfServiceVSC.Models.SCSAPI;
using AAC.SelfServiceVSC.Models.PaylinkAPI;
using AAC.SelfServiceVSC.Models.Form;
using AAC.Libraries;
using Elavon_Converge.Models;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.IO;
using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using SelfServiceVSC.DAL;
using static AAC.SelfServiceVSC.Models.SessionDetails;
using Org.BouncyCastle.Utilities.Zlib;
using AAC.SelfServiceVSC.Models.GoogleReview;
using Twilio.Base;
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
				//Filter = new Filter
				//{
				//	TermMinSpecified = true,
				//	TermMax = 1,
				//	TermMaxSpecified = true,
				//	TermMin = 0
				//}
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
			var VinExists = false;
			BestpriceprotectionContext dbContext = new BestpriceprotectionContext();
			
					Logger.WriteLine($"Executing try block");

					// Retrieve a customer with the same VIN as the one entered by the user
					string vinToCheck = sessionDetails.EstimateRequest.VIN;
					//DAL.Customer Customer = dbContext.Customers.Find(sessionDetails.EstimateRequest.VIN);
					//DAL.Customer customer = dbContext.Customers.FirstOrDefault(c => c.Vin == sessionDetails.EstimateRequest.VIN);

					//DAL.Customer matchingCustomer = dbContext.Customers.FirstOrDefault(c => c.Vin == vinToCheck);


			DateTime twentyFourHoursAgo = DateTime.Now.AddHours(-24);

			DAL.Customer matchingCustomer = dbContext.Customers
					.FirstOrDefault(c => c.Vin == vinToCheck &&
																c.IsPurchased == false &&
																c.CreatedDate >= twentyFourHoursAgo);


			if (matchingCustomer != null)
					{
						// Matching customer found
						Logger.WriteLine($"Matching customer found with ID: {matchingCustomer.Id}");
						 VinExists = true;
					}
					else
					{
						// No matching customer found
						Logger.WriteLine("No matching customer found for the entered VIN.");
						 VinExists = false;
					}
				
			

				var SCSResponse = true;
				//var VinExists = "someValue2";

			// Construct an anonymous object with the values
			var responseData = new
			{
				SCSResponse = SCSResponse,
				VinExists = VinExists
			};
			return Json(responseData);
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

			sessionDetails.RateId = (int)selectEstimate.rateId;
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
			try
			{
				if (sessionDetails != null && sessionDetails.EstimateRequest != null && sessionDetails.SelectedSessionEstimate != null &&
			quoteRequest != null)
				{
					using (BestpriceprotectionContext dbContext = new BestpriceprotectionContext())
					{
						Logger.WriteLine($"Executing try block");
						DAL.Customer newCustomer = new DAL.Customer
						{
							Vin = sessionDetails.EstimateRequest.VIN,
							Coverage = sessionDetails.SelectedSessionEstimate.PlanDescription,
							Deductible = (Int32)sessionDetails.SelectedSessionEstimate.DeductAmt,
							CoverageExpirationDate = (DateTime)sessionDetails.SelectedSessionEstimate.ExpirationDate,
							CoverageExpirationMileage = (Int32)sessionDetails.SelectedSessionEstimate.ExpirationMileage,
							FirstName = quoteRequest.FirstName,
							LastName = quoteRequest.LastName,
							Email = quoteRequest.Email,
							Phone = quoteRequest.Phone,
							Street1 = quoteRequest.Address1,
							Street2 = quoteRequest.Address2,
							City = quoteRequest.City,
							State = quoteRequest.State,
							Zip = quoteRequest.Zip,
							Amount = (Decimal)sessionDetails.SelectedSessionEstimate.RetailRate,
							RateId = sessionDetails.RateId

						};

						


						Logger.WriteLine($"Database object created");
						// Add the new customer to the Customers DbSet
						dbContext.Customers.Add(newCustomer);
						//dbContext.CustomersSwitched.Add(newCustomerSwitched);
						// Save changes to the database
						dbContext.SaveChanges();
						Logger.WriteLine($"Database object added succesfully");
						sessionDetails.CustomerId = newCustomer.Id;
						//sessionDetails.CustomersSwitched = (newCustomerSwitched.Id);

					}

				}
			}
			catch (Exception ex)
			{
				Logger.WriteLine($"An error occurred: {ex.Message}");
				return Json(new { message = "An error occurred while processing the file." });
			}
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
			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = null,
				Converters =
				{
					new JsonStringEnumConverter()
				},
				//Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};

			var json0 = JsonSerializer.Serialize(finalizeContract, typeof(FinalizeContractRequest), options);
			var json1 = JsonSerializer.Serialize(sessionDetails, typeof(SessionDetails), options);
			var estimates = sessionDetails.SessionEstimates.Select(estimate => estimate.GetClientEstimate());
			decimal? discountedCost = 0;
			foreach (var estimate in estimates)
			{
				if (estimate.Selected == true)
				{
					var originalCost = estimate.RetailRate;
					discountedCost = originalCost * 0.9m;
				}
			}

			sessionDetails.FinalizeContract = finalizeContract;
			this.HttpContext.Session.SetSessionDetails(sessionDetails);

			var generateContract = new GenerateContract(sessionDetails)
			{
				FinalCopy = true,
				RetailCost = discountedCost
			};
			var json3 = JsonSerializer.Serialize(generateContract, typeof(GenerateContract), options);
			try
			{
				var scsContract = await scs.GenerateContract(generateContract);

				if ((scsContract?.ResponseCode ?? 0) == 0)
				{
					var errorSubject = "Contract Generation failed" + scsContract.ContractNumber;
					var errorBody = "Error occurred during contrat generation!\n\n" +
						"Customer Payment details:\n\n" +
						"\tName: " + sessionDetails.QuoteRequest.FullName + "\n\n" +
						"\tPhone: " + sessionDetails.QuoteRequest.PhoneNumber + "\n\n" +
						"\tEmail: " + sessionDetails.QuoteRequest.Email + "\n\n" +
						"\tOdometer: " + sessionDetails.EstimateRequest.MileageInt + "\n\n" +
						"\tAmount Charged: " + sessionDetails.ChasePaymentRequest.Amount + "\n\n" +
						//"\tCard Number: " + sessionDetails.ChasePaymentRequest.CcAccountNum + "\n\n" +
						"\tCard Number: " + MaskFirst12Digits(sessionDetails.ChasePaymentRequest.CcAccountNum) + "\n\n" +
						"\tVIN: " + sessionDetails.EstimateRequest.VIN + "\n\n" +
						"\tVehicle Make: " + sessionDetails.EstimateRequest.Make + "\n\n" +
						"\tVehicle Model: " + sessionDetails.EstimateRequest.Model + "\n\n" +
						"";


					Mail.Send(
						//"test01252024@gmail.com",
						"newbpp@aacwarranty.com",
					"AAC",
					errorSubject,
					errorBody,
						new Stream[] { },
						new String[] { });
					throw new Exception("Creating contract failed.");
				}



				var contract = new Contract(sessionDetails);

				var json2 = JsonSerializer.Serialize(contract, typeof(Contract), options);


				/*var submitContractResult = await AAC.SelfServiceVSC.Models.PaylinkAPI.API.SubmitContract(contract);

				if (submitContractResult == null || (submitContractResult.ValidationErrors?.Any() ?? false))
				{
					var voidContract = new VoidContract(sessionDetails);
					var scsCancel = await scs.VoidContract(voidContract);

					throw new Exception("Creating loan failed. Canceled contract creation.");
				}*/
				// Assuming scsContract.ContractDocument contains the Base64 string representation of the PDF document





				//Convert the Base64 string to a byte array
				byte[] pdfBytes = Convert.FromBase64String(scsContract.ContractDocument);

				using (var outputStream = new MemoryStream(pdfBytes))
				{
					//RemovePages(new MemoryStream(pdfBytes), outputStream, 1, 2);

					
					scsContract.ContractDocument = Convert.ToBase64String(outputStream.ToArray());
					sessionDetails.GeneratedContract = scsContract;
					this.HttpContext.Session.SetSessionDetails(sessionDetails);






					//var PDFBytes = Convert.FromBase64String(scsContract.ContractDocument);
					//var PDFStream = new MemoryStream(PDFBytes);
					var subject = "AAC Service Contract: Number " + scsContract.ContractNumber;
					var body = "Thank you for choosing American Assurance Corporation for your vehicle service contract needs!\n\n" +
						"Contract details:\n\n" +
						"\tName: " + sessionDetails.QuoteRequest.FullName + "\n\n" +
						"\tPhone Number: " + sessionDetails.QuoteRequest.PhoneNumber + "\n\n" +
						"\tEmail: " + sessionDetails.QuoteRequest.Email + "\n\n" +
						"\tContract Number: " + scsContract.ContractNumber + "\n\n" +
						"\tVehicle Make: " + sessionDetails.EstimateRequest.Make + "\n\n" +
						"\tVehicle Model: " + sessionDetails.EstimateRequest.Model + "\n\n" +
						"\tVIN: " + sessionDetails.EstimateRequest.VIN + "\n\n" +
						"\tOdometer: " + sessionDetails.EstimateRequest.MileageInt + "\n\n" +
						"";




					if (sessionDetails.OdometerImage != null)
					{
						var filePath =
						new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{sessionDetails.OdometerImage}";

						var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

						Mail.Send(
						"newbpp@aacwarranty.com",
						"AAC",
						subject,
						body,
						new Stream[] { outputStream, imageStream },
						new String[] { scsContract.ContractNumber + ".pdf", sessionDetails.OdometerImage });

						Mail.Send(
						to: sessionDetails.QuoteRequest.Email,
						toName: sessionDetails.QuoteRequest.FullName,
						subject,
						body,
						new Stream[] { outputStream, imageStream },
						new String[] { scsContract.ContractNumber + ".pdf", sessionDetails.OdometerImage });
						imageStream.Close();
						var fileInfo = new FileInfo(filePath);
						if (fileInfo.Exists)
						{
							fileInfo.Delete();
						}

					}
					else
					{

						Mail.Send(
						to: sessionDetails.QuoteRequest.Email,
						toName: sessionDetails.QuoteRequest.FullName,
						subject,
						body,
						attachmentStreams: new Stream[] { outputStream },
						attachmentNames: new String[] { scsContract.ContractNumber + ".pdf" });
						Mail.Send(
						"newbpp@aacwarranty.com",	
						"AAC",
						subject,
						body,
						new Stream[] { outputStream },
						new String[] { scsContract.ContractNumber + ".pdf" });
					}
				}
				using (BestpriceprotectionContext dbContext = new BestpriceprotectionContext())
				{

					DAL.Customer Customer = dbContext.Customers.Find(sessionDetails.CustomerId);
					if (Customer != null)
					{
						Customer.IsPurchased = true;
						dbContext.SaveChanges();
					}
					else
					{
						// Record with the specified ID not found, handle accordingly
						Console.WriteLine("Record not found with ID: " + sessionDetails.CustomerId);
					}

				}
				sessionDetails.Reset();

				return Json(true);
			}
			catch (Exception ex)
			{
				Logger.WriteError(ex);
				return Json(false);
			}
		}
		private string MaskFirst12Digits(string cardNumber)
		{
			if (string.IsNullOrEmpty(cardNumber))
				return cardNumber;

			int digitCount = 0; // To track the number of digits replaced
			char[] result = cardNumber.ToCharArray();

			for (int i = 0; i < result.Length; i++)
			{
				if (char.IsDigit(result[i])) // Only replace digits
				{
					result[i] = 'X';
					digitCount++;

					if (digitCount == 12) // Stop after 12 digits
						break;
				}
			}

			return new string(result);
		}

		static void RemovePages(Stream inputPdfStream, Stream outputPdfStream, params int[] pageNumbersToRemove)
		{
			// Open the input PDF document
			using (PdfReader reader = new PdfReader(inputPdfStream))
			{
				// Create a new document
				using (Document document = new Document())
				{
					// Create a PdfCopy object to write to the output PDF
					using (PdfCopy copy = new PdfCopy(document, outputPdfStream))
					{
						// Open the document
						document.Open();

						// Get the total number of pages in the input PDF
						int totalPages = reader.NumberOfPages;

						// Loop through each page of the input PDF
						for (int i = 1; i <= totalPages; i++)
						{
							// Check if the current page should be removed
							if (!ArrayContains(pageNumbersToRemove, i))
							{
								// If the page should not be removed, add it to the output PDF
								PdfImportedPage page = copy.GetImportedPage(reader, i);
								copy.AddPage(page);
							}
						}

						// Check if the input PDF has form fields
						if (reader.AcroForm != null)
						{
							// Copy the form fields to the output PDF
							copy.CopyAcroForm(reader);
						}
					}
				}
			}
		}


		static bool ArrayContains(int[] array, int value)
		{
			foreach (int item in array)
			{
				if (item == value)
					return true;
			}
			return false;
		}

		[HttpPost("/voidPlan")]
		public async Task<JsonResult> VoidPlan()
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			var voidContract = new VoidContract(sessionDetails);
			var scsCancel = await scs.VoidContract(voidContract);
			return Json(true);
		}

			[HttpPost("/finalizePaymentPlan")]
		public async Task<JsonResult> FinalizeContractForPaymentPlan(FinalizeContractRequest finalizeContract)
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = null,
				Converters =
				{
					new JsonStringEnumConverter()
				},
				//Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};

			var json0 = JsonSerializer.Serialize(finalizeContract, typeof(FinalizeContractRequest), options);
			var json1 = JsonSerializer.Serialize(sessionDetails, typeof(SessionDetails), options);



			sessionDetails.FinalizeContract = finalizeContract;

			this.HttpContext.Session.SetSessionDetails(sessionDetails);

			var generateContract = new GenerateContract(sessionDetails)
			{
				FinalCopy = true
			};
			var json3 = JsonSerializer.Serialize(generateContract, typeof(GenerateContract), options);
			try
			{
				var scsContract = await scs.GenerateContract(generateContract);

				if ((scsContract?.ResponseCode ?? 0) == 0)
				{
					throw new Exception("Creating contract failed.");
				}

				var contract = new PlanContract(sessionDetails);

				var json2 = JsonSerializer.Serialize(contract, typeof(PlanContract), options);

				var submitContractResult = await AAC.SelfServiceVSC.Models.PaylinkAPI.API.SubmitContract(contract);

				if (submitContractResult == null || (submitContractResult.ValidationErrors?.Any() ?? false))
				{
					var voidContract = new VoidContract(sessionDetails);
					var scsCancel = await scs.VoidContract(voidContract);

					throw new Exception("Creating loan failed. Canceled contract creation.");
				}


				byte[] pdfBytes = Convert.FromBase64String(scsContract.ContractDocument);

				using (MemoryStream outputStream = new MemoryStream(pdfBytes))
				{
					//RemovePages(new MemoryStream(pdfBytes), outputStream, 1, 2);
					scsContract.ContractDocument = Convert.ToBase64String(outputStream.ToArray());
					sessionDetails.GeneratedContract = scsContract;
					this.HttpContext.Session.SetSessionDetails(sessionDetails);
					//	var PDFBytes = Convert.FromBase64String(scsContract.ContractDocument);
					//var PDFStream = new MemoryStream(PDFBytes);
					var subject = "AAC Service Contract: Number " + scsContract.ContractNumber;
					var body = "Thank you for choosing American Assurance Corporation for your vehicle service contract needs!\n\n" +
						"Contract details:\n\n" +
						"\tName: " + sessionDetails.QuoteRequest.FullName + "\n\n" +
						"\tPhone Number: " + sessionDetails.QuoteRequest.PhoneNumber + "\n\n" +
						"\tEmail: " + sessionDetails.QuoteRequest.Email + "\n\n" +
						"\tContract Number: " + scsContract.ContractNumber + "\n\n" +
						"\tVehicle Make: " + sessionDetails.EstimateRequest.Make + "\n\n" +
						"\tVehicle Model: " + sessionDetails.EstimateRequest.Model + "\n\n" +
						"\tVIN: " + sessionDetails.EstimateRequest.VIN + "\n\n" +
						"\tOdometer: " + sessionDetails.EstimateRequest.MileageInt + "\n\n" +
						"";
					// uncomment when solution found to not get error when no odometer image uploaded
					//var filePath =
					//	new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{sessionDetails.OdometerImage}";

					//var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);



					if (sessionDetails.OdometerImage != null)
					{
						var filePath =
						new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{sessionDetails.OdometerImage}";

						var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

						Mail.Send(
						"newbpp@aacwarranty.com",
						"AAC",
						subject,
						body,
						new Stream[] { outputStream, imageStream },
						new String[] { scsContract.ContractNumber + ".pdf", sessionDetails.OdometerImage });

						Mail.Send(
						to: sessionDetails.QuoteRequest.Email,
						toName: sessionDetails.QuoteRequest.FullName,
						subject,
						body,
						new Stream[] { outputStream, imageStream },
						new String[] { scsContract.ContractNumber + ".pdf", sessionDetails.OdometerImage });
						imageStream.Close();
						var fileInfo = new FileInfo(filePath);
						if (fileInfo.Exists)
						{
							fileInfo.Delete();
						}
					}
					else
					{
						Mail.Send(
						to: sessionDetails.QuoteRequest.Email,
						toName: sessionDetails.QuoteRequest.FullName,
						subject,
						body,
						attachmentStreams: new Stream[] { outputStream },
						attachmentNames: new String[] { scsContract.ContractNumber + ".pdf" });
						Mail.Send(
						"newbpp@aacwarranty.com",
						"AAC",
						subject,
						body,
						new Stream[] { outputStream },
						new String[] { scsContract.ContractNumber + ".pdf" });
					}
				}

				using (BestpriceprotectionContext dbContext = new BestpriceprotectionContext())
				{

					DAL.Customer Customer = dbContext.Customers.Find(sessionDetails.CustomerId);
					if (Customer != null)
					{
						Customer.IsPurchased = true;
						dbContext.SaveChanges();
					}
					else
					{
						// Record with the specified ID not found, handle accordingly
						Console.WriteLine("Record not found with ID: " + sessionDetails.CustomerId);
					}

				}

				//Mail.Send(
				//	"programming@aacwarranty.com",
				//	"AAC",
				//	subject,
				//	body,
				//	new Stream[] { PDFStream, imageStream },
				//	new String[] { scsContract.ContractNumber + ".pdf", sessionDetails.OdometerImage });

				sessionDetails.Reset();

				return Json(true);
			}
			catch (Exception ex)
			{
				Logger.WriteError(ex);
				return Json(false);
			}
		}
		[HttpPost("/SwitchToPhone")]
		public async Task<IActionResult> SwitchToPhone()
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			using (BestpriceprotectionContext dbContext = new BestpriceprotectionContext())
			{

				DAL.Customer Customer = dbContext.Customers.Find(sessionDetails.CustomerId);
				if (Customer != null)
				{
					Customer.IsSwitched = true;
					dbContext.SaveChanges();
					return Json(true);
				}
				else
				{
					// Record with the specified ID not found, handle accordingly
					Console.WriteLine("Record not found with ID: " + sessionDetails.CustomerId);
					return Json(false);
				}

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
			await LogRequestAsync(tokenRequest, sslAmount);
			ConvergeApiService service = new ConvergeApiService();
			var Token = await service.GetConvergeCreditCardSessionTokenAsync(tokenRequest.FirstName, tokenRequest.LastName, sslAmount);
			//breaking here because null token recieved
			await LogResponseAsync(Token);
			var Data = new
			{
				QuoteRequest = sessionDetails.QuoteRequest,
				token = Token
			};

			return new JsonResult(Data);
		}
		[HttpPost("/GetAchToken")]
		public async Task<IActionResult> GetAchToken([FromBody] TokenRequestModel tokenRequest)
		{

			// Implement your server-side logic to obtain the session token
			// You can use the ConvergeApiService or another method to make the API call

			// For demonstration purposes, returning a dummy session token

			var sessionDetails = this.HttpContext.Session.GetSessionDetails();

			var sslAmount = sessionDetails.SelectedSessionEstimate.RetailRate * 0.9m;
			await LogRequestAsync(tokenRequest, sslAmount);
			ConvergeApiService service = new ConvergeApiService();
			var Token = await service.GetConvergeAchSessionTokenAsync(tokenRequest.FirstName, tokenRequest.LastName, sslAmount);
			//breaking here because null token recieved
			await LogResponseAsync(Token);
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
			try
			{

				if (file == null)
				{
					return Json(new { message = "Please upload a file first." });
				}

				if (file.Length > 0)
				{
					// Getting FileName
					var fileName = Path.GetFileName(file.FileName);

					// Assigning Unique Filename (Guid)
					var myUniqueFileName = Convert.ToString(Guid.NewGuid());

					// Getting file Extension
					var fileExtension = Path.GetExtension(fileName);

					// Concatenating FileName + FileExtension
					var newFileName = String.Concat(myUniqueFileName, fileExtension);

					// Combines two strings into a path.
					var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newFileName}";

					using (FileStream fs = System.IO.File.Create(filepath))
					{
						file.CopyTo(fs);
						fs.Flush();
					}

					var sessionDetails = this.HttpContext.Session.GetSessionDetails();
					sessionDetails.OdometerImage = newFileName;
					this.HttpContext.Session.SetSessionDetails(sessionDetails);

					return Json(new { message = "Upload successful!" });
				}
				else
				{
					return Json(new { message = "File is empty." });
				}
			}
			catch (Exception ex)
			{
				Logger.WriteLine($"An error occurred: {ex.Message}");
				return Json(new { message = "An error occurred while processing the file." });
			}
		}



		[HttpGet("/DownloadPDF")]
		public IActionResult DownloadPdf()
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			if (sessionDetails.GeneratedContract == null)
			{
				return NotFound("Contract not found");
			}

			// Get the PDF content from the session
			var PDFBytes = Convert.FromBase64String(sessionDetails.GeneratedContract.ContractDocument);
			var PDFStream = new MemoryStream(PDFBytes);

			// Set the content type for the response
			Response.Headers.Add("Content-Disposition", "inline; filename=Contract.pdf");
			return File(PDFStream, "application/pdf");
		}

		[HttpGet("/VINdecoder")]
		public async Task<IActionResult> Vindecoder(string vin)
		{
			try
			{

				// Example VIN for testing
				string vinToDecode = vin;

				// VPIC API endpoint for VIN decoding
				string apiUrl = $"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/{vinToDecode}?format=json"; // Replace with the actual API URL

				using (HttpClient client = new HttpClient())
				{


					HttpResponseMessage response = await client.GetAsync(apiUrl);

					if (response.IsSuccessStatusCode)
					{
						string jsonResponse = await response.Content.ReadAsStringAsync(); 
						VehicleInfo vehicleInfo = JsonSerializer.Deserialize<VehicleInfo>(jsonResponse); 

						// Specify the desired VariableId
						/*int desiredVariableId = 29;*/ // Change this to your desired VariableId
						string VariableYear = "Model Year";
						string VariableModel = "Model";
						string VariableMake = "Make";
						// Find the result with the specified VariableId
						VehicleResult resultYear = vehicleInfo.Results.Find(r => r.Variable == VariableYear);
						VehicleResult resultModel = vehicleInfo.Results.Find(r => r.Variable == VariableModel);
						VehicleResult resultMake = vehicleInfo.Results.Find(r => r.Variable == VariableMake);


						var Data = new
						{
							Year = resultYear.Value,
							Make = resultMake.Value,
							Model = resultModel.Value

						};

						return Ok(Data); // Return the parsed data as the response
					}
					else
					{
						return StatusCode((int)response.StatusCode, response.ReasonPhrase); // Return the appropriate error status code
					}
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal Server Error"); // Handle unexpected errors
			}
		}

		[HttpGet("/GetReview")]
		public async Task<IActionResult> GetPlaceDetails()
		{

					string PlaceId = "ChIJq6EmWr-Ga4cREbeCz8bRnz4"; // Replace with actual Place ID
		      string ApiKey = "AIzaSyDFXo3PRBeK8caV_mHsLyoYhvZml2MbNbU";

		// Hardcoded URL with API key and place ID
		var url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={PlaceId}&fields=rating,reviews,user_ratings_total&key={ApiKey}";

			try
			{
				// Using HttpClient in a `using` block
				using (HttpClient client = new HttpClient())
				{
					HttpResponseMessage response = await client.GetAsync(url);

					if (response.IsSuccessStatusCode)
					{
						string responseData = await response.Content.ReadAsStringAsync();
						var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
						var placeDetails = JsonSerializer.Deserialize<PlaceDetailsResponse>(responseData, options);

						if (placeDetails.Status == "OK")
						{
							return Json(new
							{
								rating = placeDetails.Result.Rating,
								reviews = placeDetails.Result.Reviews,
								totalReviews = placeDetails.Result.User_Ratings_Total
							});
						}

						return BadRequest($"Google API Error: {placeDetails.Status}");
					}

					return StatusCode((int)response.StatusCode, $"Error fetching place details: {response.ReasonPhrase}");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		private async Task LogRequestAsync(TokenRequestModel generateContract, decimal? sslAmount)
		{
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");

			using (StreamWriter sw = new StreamWriter(path, true))
			{
				await sw.WriteLineAsync($"{timestamp} - Request: {JsonSerializer.Serialize(generateContract)} - SSL Amount: {sslAmount}");
			}
		}


		private async Task LogResponseAsync(String response)
		{
			//string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogFilePath);
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");
			using (StreamWriter sw = new StreamWriter(path, true))
			{
				await sw.WriteLineAsync($"{timestamp} - Response: {JsonSerializer.Serialize(response)}");
			}
		}
	}

}
