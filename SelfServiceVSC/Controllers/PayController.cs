using Microsoft.AspNetCore.Mvc;
using AAC.SelfServiceVSC.Models;
using AAC.SelfServiceVSC.Models.Form;
using Elavon_Converge.Models;
using Newtonsoft.Json;
using System.Text;
using AAC.SelfServiceVSC.Models.ChaseOrbitalAPI;
using Org.BouncyCastle.Utilities;
using AAC.SelfServiceVSC.Models.Line5API;
using AAC.SelfServiceVSC.Models.SCSAPI;

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
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			var odometer = sessionDetails.EstimateRequest.MileageInt;
			if (sessionDetails.OdometerImage == null)
			{
				// sessionDetails.OdometerImage = sessionDetails.EstimateRequest.VIN;

				// Specify the directory where the images are stored
				string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

				// Get all files in the directory
				string[] files = Directory.GetFiles(directoryPath);

				// Find the file matching sessionDetails.OdometerImage without the extension
				string matchingFile = files.FirstOrDefault(file =>
						Path.GetFileNameWithoutExtension(file).Equals(sessionDetails.EstimateRequest.VIN));

				// Check if a matching file is found
				if (matchingFile != null)
				{
					sessionDetails.OdometerImage = Path.GetFileName(matchingFile);
					this.HttpContext.Session.SetSessionDetails(sessionDetails);
					// You can use matchingFile as the path to the image file
				}
				else { return BadRequest("File not found"); }
			}
				return View();
		}

		[HttpGet("/payment-plans")]
		public IActionResult Plans()
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			var odometer = sessionDetails.EstimateRequest.MileageInt;
			if (sessionDetails.OdometerImage == null)
			{
				// sessionDetails.OdometerImage = sessionDetails.EstimateRequest.VIN;

				// Specify the directory where the images are stored
				string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

				// Get all files in the directory
				string[] files = Directory.GetFiles(directoryPath);

				// Find the file matching sessionDetails.OdometerImage without the extension
				string matchingFile = files.FirstOrDefault(file =>
						Path.GetFileNameWithoutExtension(file).Equals(sessionDetails.EstimateRequest.VIN));

				// Check if a matching file is found
				if (matchingFile != null)
				{
					sessionDetails.OdometerImage = Path.GetFileName(matchingFile);
					this.HttpContext.Session.SetSessionDetails(sessionDetails);
					// You can use matchingFile as the path to the image file
				}
				else { return BadRequest("File not found"); }

			}
			return View();
		}

		[HttpPost("/payment-details")]
		public JsonResult PaymentsDetails([FromBody] AAC.SelfServiceVSC.Models.Form.PaymentRequest paymentRequest)
		{
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			var estimate = sessionDetails.SelectedSessionEstimate;
			sessionDetails.PaymentRequest = paymentRequest;
			this.HttpContext.Session.SetSessionDetails(sessionDetails);
			return Json(sessionDetails.SelectedSessionEstimate);
		}
		[HttpGet("/payment-process-credit")]
		public IActionResult Processcredit()
		{
			return View();
		}
	
		[HttpPost("/ChaseOrbitalPayments")]
		//public async Task<IActionResult> ChaseOrbitalPayments()
		public async Task<IActionResult> ChaseOrbitalPayments([FromBody] ChasePaymentRequest tokenRequest)
		{
			string[] parts = tokenRequest.CcExp.Split('/');

			// Parse month and year from string parts
			int month = int.Parse(parts[0]);
			int year = int.Parse(parts[1]);

			// Determine the current century based on the current yeaSr
			int currentYear = DateTime.Now.Year;
			int century = currentYear / 100;

			// Construct the formatted date in YYYYMM format
			string formattedDate = (century * 100 + year).ToString("D4") + month.ToString("D2");
			var sessionDetails = this.HttpContext.Session.GetSessionDetails();
			sessionDetails.ChasePaymentRequest = tokenRequest;
			var sslAmount = sessionDetails.SelectedSessionEstimate.RetailRate * 0.9m;
			sessionDetails.ChasePaymentRequest.Amount = sslAmount??0;
			// var url = "https://orbitalvar1.chasepaymentech.com/gwapi/v4/gateway/payments/";  // Replace with your actual endpoint URL
			 var url = "https://orbital1.chasepaymentech.com/gwapi/v4/gateway/payments/";
			this.HttpContext.Session.SetSessionDetails(sessionDetails);
			#region "Profile Use-AuthCapture-US-Ecom-BIN2 (SOAP-JSON)"
			//var requestData = new PaymentRequest
			//{
			//	Version = "5.2",
			//	TransType = "A",
			//	Merchant = new Merchant
			//	{
			//		Bin = "000002",
			//		TerminalID = "001"
			//	},
			//	PaymentInstrument = new PaymentInstrument
			//	{
			//		UseProfile = new UseProfile
			//		{
			//			useCustomerRefNum = "testprofile01"
			//		}
			//	},
			//	Order = new Order
			//	{
			//		OrderID = "123457",// increment order id
			//		Amount = "1000", // Convert to string with 2 decimal places
			//		IndustryType = "EC"
			//	},
			//	CardholderVerification = new CardholderVerification
			//	{
			//		CcCardVerifyNum = "123"
			//	}
			//};
			#endregion

			#region "AuthCapture-AMEX-US-EC-BIN2 (SOAP-JSON)"
			var requestData = new PaymentRequest
			{
				Version = "5.2",
				TransType = "AC",
				Merchant = new Merchant
				{
					Bin = "000002",
					TerminalID = "001"
				},
				PaymentInstrument = new PaymentInstrument
				{
					Card = new Card
					{
						CcAccountNum = tokenRequest.CcAccountNum.Replace("-", ""),
						CcExp = formattedDate,
					}
				},
				Order = new Order
				{
					OrderID = "123457",// increment order id
					Amount = ((int)(sslAmount * 100)).ToString(), // Convert to string with 2 decimal places
																												//Amount = "12200",
					IndustryType = "EC"
				},
				CardholderVerification = new CardholderVerification
				{
					CcCardVerifyNum = tokenRequest.CcCardVerifyNum
				},
				AvsBilling = new AvsBilling
				{
					AvsAddress1 = sessionDetails.QuoteRequest.Address1,
					AvsAddress2 = sessionDetails.QuoteRequest.Address2,
					AvsCity = sessionDetails.QuoteRequest.City,
					AvsState = sessionDetails.QuoteRequest.State,
					AvsZip = sessionDetails.QuoteRequest.Zip
				}
			};
			//var requestData = new PaymentRequest
			//{
			//	Version = "5.2",
			//	TransType = "AC",
			//	Merchant = new Merchant
			//	{
			//		Bin = "000002",
			//		TerminalID = "001"
			//	},
			//	PaymentInstrument = new PaymentInstrument
			//	{
			//		Card = new Card
			//		{
			//			CcAccountNum = "341134113411347",
			//			CcExp = "202506"
			//		}
			//	},
			//	Order = new Order
			//	{
			//		OrderID = "123457",// increment order id
			//		Amount = "12200",
			//		IndustryType = "EC"
			//	},
			//	CardholderVerification = new CardholderVerification
			//	{
			//		CcCardVerifyNum = "1234"

			//	},

			//	AvsBilling = new AvsBilling
			//	{
			//		AvsAddress1 = "123 Main St",
			//		AvsAddress2 = "Apt 2",
			//		AvsCity = "Tampa",
			//		AvsState = "FL",
			//		AvsZip = "11111"
			//	}
			//};
			#endregion

			#region "AuthCapture-MasterCard-2Series-US-EC-BIN2(SOAP-JSON)"
			//var requestData = new PaymentRequest
			//{
			//	Version = "5.2",
			//	TransType = "A",
			//	Merchant = new Merchant
			//	{
			//		Bin = "000002",
			//		TerminalID = "001"
			//	},
			//	PaymentInstrument = new PaymentInstrument
			//	{
			//		Card = new Card
			//		{
			//			CcAccountNum = "2421000000000007",
			//			CcExp = "202506"
			//		}
			//	},
			//	Order = new Order
			//	{
			//		OrderID = "123459",// increment order id
			//		Amount = "12800", // Convert to string with 2 decimal places
			//		IndustryType = "EC"
			//	},
			//	CardholderVerification = new CardholderVerification
			//	{
			//		CcCardVerifyNum = "123"
			//	}
			//};
			#endregion

			#region "AuthCapture-MasterCard-US-EC-BIN2 (SOAP-JSON)"
			//var requestData = new PaymentRequest
			//{
			//	Version = "5.2",
			//	TransType = "A",
			//	Merchant = new Merchant
			//	{
			//		Bin = "000002",
			//		TerminalID = "001"
			//	},
			//	PaymentInstrument = new PaymentInstrument
			//	{
			//		Card = new Card
			//		{
			//			CcAccountNum = "5112345112345114",
			//			CcExp = "202506"
			//		}
			//	},
			//	Order = new Order
			//	{
			//		OrderID = "123460",// increment order id
			//		Amount = "13000", // Convert to string with 2 decimal places
			//		IndustryType = "EC"
			//	},
			//	CardholderVerification = new CardholderVerification
			//	{
			//		CcCardVerifyNum = "123"
			//	}
			//};
			#endregion

			#region "AuthCapture-Visa-US-EC-BIN2 (SOAP-JSON)"
			//var requestData = new PaymentRequest
			//{
			//    Version = "5.2",
			//    TransType = "A",
			//    Merchant = new Merchant
			//    {
			//        Bin = "000002",
			//        TerminalID = "001"
			//    },
			//    PaymentInstrument = new PaymentInstrument
			//    {
			//        Card = new Card
			//        {
			//            CcAccountNum = "4112344112344113",
			//            CcExp = "202506"
			//        }
			//    },
			//    Order = new Order
			//    {
			//        OrderID = "123461",// increment order id
			//        Amount = "13200", // Convert to string with 2 decimal places
			//        IndustryType = "EC"
			//    },
			//    CardholderVerification = new CardholderVerification
			//    {
			//        CcCardVerifyNum = "1234"
			//    }
			//};
			#endregion

			#region "Account Verification-AMEX-US (SOAP-JSON)"
			//var requestData = new PaymentRequest
			//{
			//    Version = "5.2",
			//    TransType = "A",
			//    Merchant = new Merchant
			//    {
			//        Bin = "000002",
			//        TerminalID = "001"
			//    },
			//    PaymentInstrument = new PaymentInstrument
			//    {
			//        Card = new Card
			//        {
			//            CcAccountNum = "341134113411347",
			//            CcExp = "202506"
			//        }
			//    },
			//    Order = new Order
			//    {
			//        OrderID = "123462",// increment order id
			//        Amount = "0", // Convert to string with 2 decimal places
			//        IndustryType = "EC"
			//    },
			//    CardholderVerification = new CardholderVerification
			//    {
			//        CcCardVerifyNum = "1234"
			//    },
			//    AvsBilling = new AvsBilling
			//    {
			//        AvsAddress1 = "123 Main St",
			//        AvsAddress2 = "Apt 2",
			//        AvsCity = "Tampa",
			//        AvsState = "FL",
			//        AvsZip = "11111"
			//    }
			//};
			#endregion

			#region "* Passes two test cases Account Verification-MasterCard 2Series-US (SOAP-JSON)"
			//var requestData = new PaymentRequest
			//{
			//    Version = "5.2",
			//    TransType = "A",
			//    Merchant = new Merchant
			//    {
			//        Bin = "000002",
			//        TerminalID = "001"
			//    },
			//    PaymentInstrument = new PaymentInstrument
			//    {
			//        Card = new Card
			//        {
			//            CcAccountNum = "2221000000000009",
			//            CcExp = "202506"
			//        }
			//    },
			//    Order = new Order
			//    {
			//        OrderID = "123463",// increment order id
			//        Amount = "0", // Convert to string with 2 decimal places
			//        IndustryType = "EC"
			//    },
			//    CardholderVerification = new CardholderVerification
			//    {
			//        CcCardVerifyNum = "123"
			//    },
			//    AvsBilling = new AvsBilling
			//    {
			//        AvsAddress1 = "123 Main St",
			//        AvsAddress2 = "Apt 2",
			//        AvsCity = "Tampa",
			//        AvsState = "FL",
			//        AvsZip = "11111"
			//    }
			//};
			#endregion

			#region "Account Verification-MasterCard-US (SOAP-JSON)"
			//var requestData = new PaymentRequest
			//{
			//    Version = "5.2",
			//    TransType = "A",
			//    Merchant = new Merchant
			//    {
			//        Bin = "000002",
			//        TerminalID = "001"
			//    },
			//    PaymentInstrument = new PaymentInstrument
			//    {
			//        Card = new Card
			//        {
			//            CcAccountNum = "5112345112345114",
			//            CcExp = "202506"
			//        }
			//    },
			//    Order = new Order
			//    {
			//        OrderID = "123466",// increment order id
			//        Amount = "0", // Convert to string with 2 decimal places
			//        IndustryType = "EC"
			//    },
			//    CardholderVerification = new CardholderVerification
			//    {
			//        CcCardVerifyNum = "123"
			//    },
			//    AvsBilling = new AvsBilling
			//    {
			//        AvsAddress1 = "123 Main St",
			//        AvsAddress2 = "Apt 2",
			//        AvsCity = "Tampa",
			//        AvsState = "FL",
			//        AvsZip = "11111"
			//    }
			//};
			#endregion

			#region "Account Verification-Visa-US (SOAP-JSON)"
			//var requestData = new PaymentRequest
			//{
			//	Version = "5.2",
			//	TransType = "A",
			//	Merchant = new Merchant
			//	{
			//		Bin = "000002",
			//		TerminalID = "001"
			//	},
			//	PaymentInstrument = new PaymentInstrument
			//	{
			//		Card = new Card
			//		{
			//			CcAccountNum = "4112344112344113",
			//			CcExp = "202506",
			//			CcCardVerifyPresenceInd = true
			//		}
			//	},
			//	Order = new Order
			//	{
			//		OrderID = "123466",// increment order id
			//		Amount = "0", // Convert to string with 2 decimal places
			//		IndustryType = "EC"

			//	},
			//	CardholderVerification = new CardholderVerification
			//	{
			//		CcCardVerifyNum = "123"
			//	},
			//	AvsBilling = new AvsBilling
			//	{
			//		AvsAddress1 = "123 Main St",
			//		AvsAddress2 = "Apt 2",
			//		AvsCity = "Tampa",
			//		AvsState = "FL",
			//		AvsZip = "11111"
			//	}
			//};
			#endregion

			#region "Refund-MasterCard 2-EC-BIN2 (SOAP-JSON)"
			//var requestData = new PaymentRequest
			//{
			//    Version = "5.2",
			//    TransType = "A",
			//    Merchant = new Merchant
			//    {
			//        Bin = "000002",
			//        TerminalID = "001"
			//    },
			//    PaymentInstrument = new PaymentInstrument
			//    {
			//        Card = new Card
			//        {
			//            CcAccountNum = "2621000000000005",
			//            CcExp = "202506"
			//        }
			//    },
			//    Order = new Order
			//    {
			//        OrderID = "123466",// increment order id
			//        Amount = "1100", // Convert to string with 2 decimal places
			//        IndustryType = "EC"
			//    },
			//    CardholderVerification = new CardholderVerification
			//    {
			//        CcCardVerifyNum = "123"
			//    },
			//    AvsBilling = new AvsBilling
			//    {
			//        AvsAddress1 = "123 Main St",
			//        AvsAddress2 = "Apt 2",
			//        AvsCity = "Tampa",
			//        AvsState = "FL",
			//        AvsZip = "12345"
			//    }
			//};
			#endregion
			using (var client = new HttpClient())
			{
				var json = JsonConvert.SerializeObject(requestData);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				client.DefaultRequestHeaders.Add("OrbitalConnectionUsername", "AMERICANA8064656114");
				client.DefaultRequestHeaders.Add("OrbitalConnectionPassword", "6D851DNsP4X8n1zx6cP");
				client.DefaultRequestHeaders.Add("MerchantID", "800000552702");

				//client.DefaultRequestHeaders.Add("OrbitalConnectionUsername", "TEST4510AMER");
				//client.DefaultRequestHeaders.Add("OrbitalConnectionPassword", "X7Hwd42k8ghR");
				//client.DefaultRequestHeaders.Add("MerchantID", "700000014510");
				Console.WriteLine(json);
				await LogRequestAsync(json);
				try
				{
					var response = await client.PostAsync(url, content);
					var responseString = await response.Content.ReadAsStringAsync();
					var chasePaymentResponse = JsonConvert.DeserializeObject<ChasePaymentResponse>(responseString);
					Console.WriteLine($"Response: {responseString}");
					await LogResponseAsync(responseString);
					if (chasePaymentResponse.Order.Status.ApprovalStatus != "1")
					{
						throw new HttpRequestException($"Client error: {(int)response.StatusCode} - {response.ReasonPhrase}");
					}
				}
				catch (HttpRequestException ex)
				{
					Console.WriteLine($"A client error occurred: {ex.Message}");
					return BadRequest(new { message = "Valid data received" }); // Re-throw the exception to be handled by the calling code if necessary
				}
				catch (Exception ex)
				{
					Console.WriteLine($"An error occurred: {ex.Message}");
					return BadRequest(new { message = "Valid data received" });
				}

			}
			return Ok(new { message = "Payment Complete" });
		}
		private async Task LogRequestAsync(string  paymentrequestjson)
		{
			//string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogFilePath);
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");
			using (StreamWriter sw = new StreamWriter(path, true))
			{
				await sw.WriteLineAsync($"{timestamp} -Request: {paymentrequestjson}");
			}
		}

		private async Task LogResponseAsync(string response)
		{
			//string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogFilePath);
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");
			using (StreamWriter sw = new StreamWriter(path, true))
			{
				await sw.WriteLineAsync($"{timestamp} -Response: {(response)}");
			}
		}
		[HttpGet("/payment-process-payment-plan")]
		public IActionResult Processpayment()
		{
			return View();
		}

	}
	public class PaymentRequest
	{
		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("transType")]
		public string TransType { get; set; }

		[JsonProperty("merchant")]
		public Merchant Merchant { get; set; }

		[JsonProperty("paymentInstrument")]
		public PaymentInstrument PaymentInstrument { get; set; }

		[JsonProperty("order")]
		public Order Order { get; set; }

		[JsonProperty("cardholderVerification")]
		public CardholderVerification CardholderVerification { get; set; }
		[JsonProperty("avsBilling", NullValueHandling = NullValueHandling.Ignore)]
		public AvsBilling AvsBilling { get; set; }
	}
	public class AvsBilling
	{
		[JsonProperty("avsAddress1")]
		public string AvsAddress1 { get; set; }

		[JsonProperty("avsAddress2")]
		public string AvsAddress2 { get; set; }

		[JsonProperty("avsCity")]
		public string AvsCity { get; set; }

		[JsonProperty("avsState")]
		public string AvsState { get; set; }

		[JsonProperty("avsZip")]
		public string AvsZip { get; set; }

		[JsonProperty("avsRespCode", NullValueHandling = NullValueHandling.Ignore)]
		public string AvsRespCode { get; set; }

		[JsonProperty("hostAvsRespCode", NullValueHandling = NullValueHandling.Ignore)]
		public string HostAvsRespCode { get; set; }
	}
	public class Merchant
	{
		[JsonProperty("bin")]
		public string Bin { get; set; }

		[JsonProperty("terminalID")]
		public string TerminalID { get; set; }
		[JsonProperty("merchantID ", NullValueHandling = NullValueHandling.Ignore)]
		public UseProfile MerchantID { get; set; }
	}

	public class PaymentInstrument
	{
		[JsonProperty("card")]
		public Card Card { get; set; }
		[JsonProperty("useProfile", NullValueHandling = NullValueHandling.Ignore)]
		public UseProfile UseProfile { get; set; }
	}

	//public class PaymentInstrument
	//{

	//     public UseProfile UseProfile { get; set; }
	//}

	public class Card
	{
		[JsonProperty("ccAccountNum")]
		public string CcAccountNum { get; set; }

		[JsonProperty("ccExp")]
		public string CcExp { get; set; }
		//[JsonProperty("ccCardVerifyPresenceIndExp")]
		//public bool CcCardVerifyPresenceInd = false;
	}

	public class Order
	{
		[JsonProperty("orderID")]
		public string OrderID { get; set; }

		[JsonProperty("amount")]
		public string Amount { get; set; }

		[JsonProperty("industryType")]
		public string IndustryType { get; set; }

		[JsonProperty("txRefNum", NullValueHandling = NullValueHandling.Ignore)]
		public string TxRefNum { get; set; }

		[JsonProperty("txRefIdx", NullValueHandling = NullValueHandling.Ignore)]
		public string TxRefIdx { get; set; }

		[JsonProperty("respDateTime", NullValueHandling = NullValueHandling.Ignore)]
		public string RespDateTime { get; set; }

		[JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
		public Status Status { get; set; }
	}
	public class UseProfile
	{
		public String useCustomerRefNum;
	}
	public class CardholderVerification
	{
		[JsonProperty("ccCardVerifyNum")]
		public string CcCardVerifyNum { get; set; }

		[JsonProperty("cvvRespCode", NullValueHandling = NullValueHandling.Ignore)]
		public string CvvRespCode { get; set; }

		[JsonProperty("hostCvvRespCode", NullValueHandling = NullValueHandling.Ignore)]
		public string HostCvvRespCode { get; set; }
	}

	public class CardData
	{
		public string DownpaymentPaymentCardExpiration { get; set; }
		public string DownpaymentPaymentCardNumber { get; set; }
		public string PaymentCardCVV { get; set; }
		public string DownpaymentPaymentCardName { get; set; }
		public decimal DownpaymentPaymentAmount { get; set; }
	}

}
