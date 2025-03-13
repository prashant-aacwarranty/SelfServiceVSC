using Microsoft.AspNetCore.Mvc;
using AAC.SelfServiceVSC.Models;
using AAC.SelfServiceVSC.Models.Line5API;
using AAC.SelfServiceVSC.Models.Form;
using AAC.SelfServiceVSC.Models.SCSAPI;

namespace SelfServiceVSC.Controllers
{
	public class QuoteController : Controller
	{
		private readonly SCSApiClient scs;

		public QuoteController(SCSApiClient scs)
		{
			this.scs = scs;
		}


		// this one does need to work... it's hit from the /Estimate page, which is a big mess
		[HttpPost, Route("/Quote")]
		public async Task<JsonResult> Quote(
			[FromBody] QuoteRequest quoteRequest)
		{
			return Json(null);

			var sessionDetails = this.HttpContext.Session.GetSessionDetails();

			var estimate = sessionDetails.SelectedSessionEstimate;

			sessionDetails.QuoteRequest = quoteRequest;

			var quotePost = new QuotePost
			{
				Data = new QuotePost.DataModel
				{
					Attributes = new QuotePost.DataModel.AttributesModel
					{
						IntegrationLoanId = estimate.RateId,
						SkipCreditValidation = false
					},
					Relationships = new QuotePost.DataModel.RelationshipsModel
					{
						Customer = new QuotePost.DataModel.RelationshipsModel.CustomerModel
						{
							Data = new QuotePost.DataModel.RelationshipsModel.CustomerModel.DataModel
							{
								Type = "customers",
								Attributes = new QuotePost.DataModel.RelationshipsModel.CustomerModel.DataModel.AttributesModel
								{
									FirstName = quoteRequest.FirstName,
									LastName = quoteRequest.LastName,
									SSN = quoteRequest.SSN,
									DateOfBirth = quoteRequest.DOB,
									Address1 = quoteRequest.Address1,
									Address2 = quoteRequest.Address2,
									City = quoteRequest.City,
									State = quoteRequest.State,
									PostalCode = quoteRequest.Zip,
									Email = quoteRequest.Email,
									CellNumber = quoteRequest.Phone,
									WorkNumber = "",
									PhoneNumber = ""
								}
							}
						},
						Vehicle = new QuotePost.DataModel.RelationshipsModel.VehicleModel
						{
							Data = new QuotePost.DataModel.RelationshipsModel.VehicleModel.DataModel
							{
								Type = "vehicles",
								Attributes = new QuotePost.DataModel.RelationshipsModel.VehicleModel.DataModel.AttributesModel
								{
									VIN = sessionDetails.EstimateRequest.VIN,
									Mileage = sessionDetails.EstimateRequest.MileageInt
								}
							}
						},
						Employee = new QuotePost.DataModel.RelationshipsModel.EmployeeModel
						{
							Data = new QuotePost.DataModel.RelationshipsModel.EmployeeModel.DataModel
							{
								Type = "employee",
								Id = 1416
							}
						},
						QuoteProtections = new QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel
						{
							Data = new List<QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel.DataModel>
							{
								new QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel.DataModel
								{
									Type = "quote-protection",
									Attributes = new QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel.DataModel.AttributesModel
									{
										ExcludeTax = true,
										FilledFields = new List<QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel.DataModel.AttributesModel.FilledFieldModel>
										{
											new QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel.DataModel.AttributesModel.FilledFieldModel
											{
												Name = "miles",
												Value = estimate.Mileage
											},
											new QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel.DataModel.AttributesModel.FilledFieldModel
											{
												Name = "months",
												Value = estimate.Term
											},
											new QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel.DataModel.AttributesModel.FilledFieldModel
											{
												Name = "cost",
												Value = estimate.NetRate - 150m
											},
											new QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel.DataModel.AttributesModel.FilledFieldModel
											{
												Name = "price",
												Value = estimate.RetailRate
											}
										}
									},
									Relationships = new QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel.DataModel.RelationshipsModel
									{
										Protection = new QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel.DataModel.RelationshipsModel.ProtectionModel
										{
											Data = new QuotePost.DataModel.RelationshipsModel.QuoteProtectionsModel.DataModel.RelationshipsModel.ProtectionModel.DataModel
											{
												Type = "protection",
												Id = 403
											}
										}
									}
								}
							}
						}
					}
				}
			};

			var quote = await AAC.SelfServiceVSC.Models.Line5API.API.QuotePost(quotePost);

			if (quote.Data.Attributes.Term > sessionDetails.SelectedSessionEstimate.Term - 12)
			{
				var quotePatch = new QuotePatch
				{
					Data = new QuotePatch.DataModel
					{
						Id = quote.Data.Id,
						Type = "quotes",
						Attributes = new QuotePatch.DataModel.AttributesModel
						{
							Term = sessionDetails.SelectedSessionEstimate.Term - 12
						}
					}
				};
			}

			sessionDetails.Quote = new SessionQuote(quote);
			sessionDetails.InitialTerm = quote.Data.Attributes.Term;

			this.HttpContext.Session.SetSessionDetails(sessionDetails);

			return Json(true);
		}

		[HttpPost, Route("/RetrieveQuote")]
		public JsonResult RetrieveQuote()
		{
			return Json(null);

			var sessionDetails = this.HttpContext.Session.GetSessionDetails();

			return Json(new { Estimate = sessionDetails.SessionEstimates.Where(estimate => estimate.Selected).FirstOrDefault().GetClientEstimate(), Quote = sessionDetails.Quote.GetClientQuote(), MaxTerm = sessionDetails.InitialTerm });
		}

		[HttpPost, Route("/ModifyQuote")]
		public async Task<JsonResult> ModifyQuote(
			[FromBody] ModifyQuoteRequest modifyQuoteRequest)
		{
			return Json(null);

			var sessionDetails = this.HttpContext.Session.GetSessionDetails();

			if (modifyQuoteRequest.TermInt <= sessionDetails.InitialTerm && modifyQuoteRequest.TermInt >= (sessionDetails.InitialTerm < 12 ? sessionDetails.InitialTerm : 12))
			{
				sessionDetails.Quote.Term = modifyQuoteRequest.TermInt;
			}
			sessionDetails.Quote.DownPayment = modifyQuoteRequest.DownPaymentDecimal;

			var quotePatch = new QuotePatch
			{
				Data = new QuotePatch.DataModel
				{
					Id = sessionDetails.Quote.QuoteId,
					Type = "quotes",
					Attributes = new QuotePatch.DataModel.AttributesModel
					{
						Term = sessionDetails.Quote.Term,
						DownPayment = sessionDetails.Quote.DownPayment
					}
				}
			};

			var quote = new SessionQuote(await AAC.SelfServiceVSC.Models.Line5API.API.QuotePatch(quotePatch));

			if (quote.Term == quotePatch.Data.Attributes.Term
				&& quote.DownPayment == quotePatch.Data.Attributes.DownPayment)
			{
				sessionDetails.Quote = quote;
				this.HttpContext.Session.SetSessionDetails(sessionDetails);

				return Json(new { Estimate = sessionDetails.SessionEstimates.Where(estimate => estimate.Selected).FirstOrDefault().GetClientEstimate(), Quote = sessionDetails.Quote.GetClientQuote(), MaxTerm = sessionDetails.InitialTerm });
			}
			else
			{
				return Json(false);
			}
		}

		[HttpPost, Route("/FinalizeLoan")]
		public async Task<JsonResult> FinalizeLoan(
			[FromBody] AAC.SelfServiceVSC.Models.Form.PaymentRequest paymentRequest)
		{
			return Json(null);

			var sessionDetails = this.HttpContext.Session.GetSessionDetails();

			var quoteFinalize = new QuoteFinalizePatch
			{
				Data = new QuoteFinalizePatch.DataModel
				{
					Id = sessionDetails.Quote.QuoteId,
					Type = "quotes",
					Attributes = new QuoteFinalizePatch.DataModel.AttributesModel
					{
						FirstPaymentDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd")
					},
					Relationships = new QuoteFinalizePatch.DataModel.RelationshipsModel
					{
						PaymentMethod = new QuoteFinalizePatch.DataModel.RelationshipsModel.PaymentMethodModel
						{
							Data = new QuoteFinalizePatch.DataModel.RelationshipsModel.PaymentMethodModel.DataModel
							{
								Type = "payment_methods",
								Attributes = new QuoteFinalizePatch.DataModel.RelationshipsModel.PaymentMethodModel.DataModel.AttributesModel
								{
									FormOfPaymentType = String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? "bank_transfer" : "payment_card",
									PaymentCardName = !String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? paymentRequest.PaymentCardName : null,
									PaymentCardNumber = !String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? paymentRequest.PaymentCardNumber : null,
									PaymentCardCVV = !String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? paymentRequest.PaymentCardCVV.Replace("//", "/") : null,
									PaymentCardExpiration = !String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? paymentRequest.PaymentCardExpiration : null,
									PaymentCardZipCode = !String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? paymentRequest.PaymentCardZipCode : null,
									BankTransferRoutingNumber = String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? paymentRequest.BankTransferRoutingNumber : null,
									BankTransferAccountNumber = String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? paymentRequest.BankTransferAccountNumber : null,
									BankTransferRoutingNumberConfirmation = String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? paymentRequest.BankTransferRoutingNumberConfirmation : null,
									BankTransferAccountNumberConfirmation = String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? paymentRequest.BankTransferAccountNumberConfirmation : null,
									BankTransferName = String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? paymentRequest.BankTransferName : null,
									BankTransferAccountType = String.IsNullOrWhiteSpace(paymentRequest.PaymentCardNumber) ? "checking" : null
								}
							}
						}
					}
				}
			};

			var quoteFinalizeResult = await AAC.SelfServiceVSC.Models.Line5API.API.QuotePatchFinalize(quoteFinalize);

			//var quoteFinalizeResult = JsonSerializer.Deserialize<QuoteFinalizePatchResponse>("{\"data\":{\"id\":\"105956\",\"type\":\"loans\",\"attributes\":{\"status\":\"awaiting_paperwork\",\"funded-at\":null,\"start-date\":\"2023-02-14\",\"payment-day\":16,\"first-payment-date\":\"2023-03-16\",\"balance\":\"0.0\",\"term\":41,\"down-payment\":\"0.0\",\"short-fund-fee\":\"0.0\",\"monthly-payment\":\"38.08\",\"tax\":\"0.0\",\"base-fee\":\"109.1\",\"buy-down-fee\":\"136.53\",\"additional-term-fee\":\"0.0\",\"doc-stamp-fee\":\"0.0\",\"financed-total\":\"1091.0\",\"esign-url\":null},\"relationships\":{\"unread-loan-notes\":{\"data\":[]},\"actual-cover-sheet\":{\"data\":{\"id\":\"3497\",\"type\":\"loan-packages\"}},\"customer\":{\"data\":{\"id\":\"11563\",\"type\":\"customers\"}},\"vehicle\":{\"data\":{\"id\":\"129215\",\"type\":\"vehicles\"}},\"quote-protections\":{\"data\":[{\"id\":\"13856\",\"type\":\"quote-protections\"}]}}},\"included\":[{\"id\":\"3497\",\"type\":\"loan-packages\",\"attributes\":{\"documents-file-name\":\"30218e3c-8d4e-4bb7-8e70-4f0f459fc5ad.pdf\",\"documents-content-type\":\"application/pdf\",\"documents-file-size\":861738},\"links\":{\"download\":\"http://staging.line5.com/api/v1/loans/105956/loan_packages/3497/download\"}},{\"id\":\"11563\",\"type\":\"customers\",\"attributes\":{\"first-name\":\"Alice\",\"last-name\":\"Alice\",\"date-of-birth\":\"2000-01-01\",\"address-1\":\"1881 CURTIS ST\",\"address-2\":null,\"city\":\"DENVER\",\"state\":\"CO\",\"postal-code\":\"80202\",\"phone-number\":\"\",\"cell-number\":\"(720) 544-6509\",\"work-number\":\"\",\"credit-score-tier\":\"h\"}},{\"id\":\"129215\",\"type\":\"vehicles\",\"attributes\":{\"year\":2022,\"make\":\"Mazda\",\"model\":\"MAZDA3\",\"vin\":\"JM1BPBNY6N1507324\",\"mileage\":6000,\"in-service-date\":\"2022-01-01\",\"status\":\"used_vehicle\",\"purchase-price\":null,\"financed-amount\":null,\"msrp\":\"35415.0\",\"finance-term\":null}},{\"id\":\"13856\",\"type\":\"quote-protections\",\"attributes\":{\"term\":84,\"name\":\"Demo VSC\",\"months\":84,\"mileage\":null,\"price\":\"1091.0\"}}]}");

			if (quoteFinalizeResult != null)
			{
				sessionDetails.LoanId = quoteFinalizeResult.Data.Id;

				var generateContract = new AAC.SelfServiceVSC.Models.SCSAPI.GenerateContract(sessionDetails);

				var generateContractResult = await scs.GenerateContract(generateContract);

				sessionDetails.GeneratedContract = generateContractResult;
				this.HttpContext.Session.SetSessionDetails(sessionDetails);

				var downloadLocation = quoteFinalizeResult.Included.FirstOrDefault(included => included.Type == "loan-packages" && !String.IsNullOrWhiteSpace(included.Links?.Download) && included.Attributes.DocumentsContentType == "application/pdf")?.Links?.Download;
				var temp = await AAC.SelfServiceVSC.Models.Line5API.API.DownloadPackage(url: downloadLocation);
				using var writerLine5 = new BinaryWriter(System.IO.File.OpenWrite("C:\\Users\\alice\\Downloads\\Line5001.pdf"));
				writerLine5.Write(temp);
				writerLine5.Close();

				try
				{
					var PDFBytes = Convert.FromBase64String(generateContractResult.ContractDocument);
					using var writerSCS = new BinaryWriter(System.IO.File.OpenWrite("C:\\Users\\alice\\Downloads\\SCS001.pdf"));
					writerSCS.Write(PDFBytes);
					writerSCS.Close();
				}
				catch
				{
					throw;
				}

				return Json(sessionDetails.LoanId);
			}
			else
			{
				return Json(false);
			}
		}
	}
}
