using System.Text.Json.Serialization;
namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public class PlanContract
	{
			
		#region Properties
		public Authentication Authentication { get; set; } = new Authentication();

		public String ServiceContractNumber { get; set; }

		public String EffectiveDate { get; set; }

		public String FirstPaymentDate { get; set; }

		public Decimal TotalPremium { get; set; }

		public Decimal? SalesTax { get; set; }

		public Int32 NumberOfInstallments { get; set; }

		public BillingMethod BillingMethod { get; set; }

		[JsonPropertyName("BillinMethodACHPayment")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public ACHPayment BillingMethodACHPayment { get; set; }

		[JsonPropertyName("BillingMethodCREDITPayment")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public CreditCardPayment BillingMethodCreditPayment { get; set; }

		[JsonPropertyName("Billing_Name")]
		public String BillingName { get; set; }

		[JsonPropertyName("Billing_Address")]
		public String BillingAddress { get; set; }

		[JsonPropertyName("Billing_City")]
		public String BillingCity { get; set; }

		[JsonPropertyName("Billing_State")]
		public String BillingState { get; set; }

		[JsonPropertyName("Billing_Zip")]
		public String BillingZip { get; set; }

		public Agent Agent { get; set; }

		public Borrower Borrower { get; set; }

		public Coverage Coverage { get; set; }

		public Vehicle Vehicle { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Home Home { get; set; }

		public Decimal DownPayment { get; set; }

		public DownpaymentMethod DownpaymentMethod { get; set; }

		[JsonPropertyName("DownpaymentACHPayment")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public ACHPayment DownPaymentACHPayment { get; set; }

		[JsonPropertyName("DownpaymentCREDITPayment")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public CreditCardPayment DownPaymentCreditPayment { get; set; }

		public Boolean? PPA { get; set; }

		[JsonPropertyName("PPA_Postback_Url")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String PPA_Postback_URL { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String AuthorizedUser { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Int32? ContractVendorId { get; set; }

		[JsonPropertyName("Annual_APR")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? AnnualAPR { get; set; }

		[JsonPropertyName("Finance_Charge")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? FinanceCharge { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String HomeCoverageAddress1 { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String HomeCoverageAddress2 { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String HomeCoverageCity { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String HomeCoverageZip { get; set; }
		#endregion

		#region Constructors
		public PlanContract()
		{

		}

		private class FeeSchedule
		{
			internal Int32? TermMin { get; set; } = null;

			internal Int32? TermMax { get; set; } = null;

			internal Decimal FeePercent { get; set; } = .085m;

			internal Decimal FeeRate
			{
				get
				{
					return 1m - FeePercent;
				}
			}

			internal Decimal FeeFlat { get; set; } = 75m;
		}

		public PlanContract(
			SessionDetails sessionDetails)
		{
			var rate = sessionDetails.SelectedRate;
			var surcharges = rate.RateClassMoney.Options.Where(option => option.IsSurcharge == true).Sum(option => option.RetailRate) ?? 0m;
			var retailRate = (rate.RateClassMoney.Rate.RetailRate ?? 0m);
			var totalRate = retailRate + surcharges;
			var downPayment = Math.Round(Decimal.Parse(sessionDetails.FinalizeContract.Downpayment) * totalRate, 2);
			var downPaymentCharge = Math.Max(5m, downPayment * 0.05m);
			var term = Int32.Parse(sessionDetails.FinalizeContract.Term);
			var feeSchedule = new List<FeeSchedule>
			{
				new FeeSchedule { TermMin = null, TermMax = 12, FeePercent = .085m, FeeFlat = 75m },
				new FeeSchedule { TermMin = 13, TermMax = 15, FeePercent = .095m, FeeFlat = 100m },
				new FeeSchedule { TermMin = 16, TermMax = 18, FeePercent = .105m, FeeFlat = 100m },
				new FeeSchedule { TermMin = 19, TermMax = null, FeePercent = .135m, FeeFlat = 150m }
			};
			var fee = feeSchedule.First(fee => term >= (fee.TermMin ?? Int32.MinValue) && term <= (fee.TermMax ?? Int32.MaxValue));

			var dealerCost = Math.Min(Math.Floor(totalRate * fee.FeeRate), totalRate - fee.FeeFlat) - downPayment - downPaymentCharge;

			ServiceContractNumber = sessionDetails.ContractNumber;
			EffectiveDate = (sessionDetails.GeneratedContract?.EffectiveDate ?? DateTime.Now).ToString("yyyy-MM-dd");
			FirstPaymentDate = (sessionDetails.GeneratedContract?.EffectiveDate ?? DateTime.Now).AddMonths(1).ToString("yyyy-MM-dd");
			TotalPremium = totalRate;
			SalesTax = 0;
			NumberOfInstallments = term;
			BillingMethod = String.IsNullOrWhiteSpace(sessionDetails.FinalizeContract.MonthlyPaymentCardNumberCalculated) ? BillingMethod.ACH : BillingMethod.CREDIT;
			if (BillingMethod == BillingMethod.CREDIT)
			{
				BillingMethodCreditPayment = new CreditCardPayment
				{
					CreditCardNumber = sessionDetails.FinalizeContract.MonthlyPaymentCardNumberCalculated,
					ExpirationMonth = sessionDetails.FinalizeContract.MonthlyPaymentCardMonth,
					ExpirationYear = sessionDetails.FinalizeContract.MonthlyPaymentCardYear,
					NameOnCard = sessionDetails.FinalizeContract.MonthlyPaymentCardNameCalculated
				};
			}
			else
			{
				BillingMethodACHPayment = new ACHPayment
				{
					BankName = sessionDetails.FinalizeContract.MonthlyBankTransferNameCalculated,
					AccountType = sessionDetails.FinalizeContract.MonthlyBankTransferAccountTypeEnum,
					RoutingNumber = sessionDetails.FinalizeContract.MonthlyBankTransferRoutingNumberCalculated,
					AccountNumber = sessionDetails.FinalizeContract.MonthlyBankTransferAccountNumberCalculated
				};
			}
			BillingName = sessionDetails.FinalizeContract.SameBilling ? sessionDetails.QuoteRequest.FullName : sessionDetails.FinalizeContract.BillingFullName;
			BillingAddress = sessionDetails.FinalizeContract.SameBilling ? sessionDetails.QuoteRequest.Address1 : sessionDetails.FinalizeContract.BillingAddress1;
			BillingCity = sessionDetails.FinalizeContract.SameBilling ? sessionDetails.QuoteRequest.City : sessionDetails.FinalizeContract.BillingCity;
			BillingState = sessionDetails.FinalizeContract.SameBilling ? sessionDetails.QuoteRequest.State : sessionDetails.FinalizeContract.BillingState;
			BillingZip = sessionDetails.FinalizeContract.SameBilling ? sessionDetails.QuoteRequest.Zip : sessionDetails.FinalizeContract.BillingZip;
			Agent = new Agent
			{
				// todo: configure all this, is this also nonprod?
				SellerCode = "A13971",
				AdminCode = "C00340",
				SalesPersonEmail = SelfServiceVSC.AppSettings.MailUsername,
				DealerCost = dealerCost
			};
			Borrower = new Borrower
			{
				FullName = sessionDetails.QuoteRequest.FullName,
				Address1 = sessionDetails.QuoteRequest.Address1,
				Address2 = sessionDetails.QuoteRequest.Address2,
				City = sessionDetails.QuoteRequest.City,
				State = sessionDetails.QuoteRequest.State,
				ZipCode = sessionDetails.QuoteRequest.Zip,
				EmailAddress = sessionDetails.QuoteRequest.Email,
				PhoneNumber = sessionDetails.QuoteRequest.PhoneNumber
			};
			Coverage = new Coverage
			{
				CoverageType = CoverageType.AUTO,
				Term = (Int16)rate.RateClassMoney.TermMile.Term,
				ProgramName = rate.PlanRate.Plan.ContractPlanName,
				Mileage = rate.RateClassMoney.TermMile.Mileage
			};
			Vehicle = new Vehicle
			{
				Make = sessionDetails.EstimateRequest.Make,
				Model = sessionDetails.EstimateRequest.Model,
				Year = sessionDetails.EstimateRequest.YearInt,
				Odometer = sessionDetails.EstimateRequest.MileageInt,
				VIN = sessionDetails.EstimateRequest.VIN
			};
			DownPayment = downPayment;
			DownpaymentMethod = String.IsNullOrWhiteSpace(sessionDetails.FinalizeContract.DownpaymentPaymentCardNumber) ? DownpaymentMethod.ACH : DownpaymentMethod.CREDIT;
			if (DownpaymentMethod == DownpaymentMethod.CREDIT)
			{
				DownPaymentCreditPayment = new CreditCardPayment
				{
					CreditCardNumber = sessionDetails.FinalizeContract.DownpaymentPaymentCardNumber,
					ExpirationMonth = sessionDetails.FinalizeContract.DownpaymentPaymentCardMonth,
					ExpirationYear = sessionDetails.FinalizeContract.DownpaymentPaymentCardYear,
					NameOnCard = sessionDetails.FinalizeContract.DownpaymentPaymentCardName
				};
			}
			else
			{
				DownPaymentACHPayment = new ACHPayment
				{
					BankName = sessionDetails.FinalizeContract.DownpaymentBankTransferName,
					AccountType = sessionDetails.FinalizeContract.DownpaymentBankTransferAccountTypeEnum,
					RoutingNumber = sessionDetails.FinalizeContract.DownpaymentBankTransferRoutingNumber,
					AccountNumber = sessionDetails.FinalizeContract.DownpaymentBankTransferAccountNumber
				};
			}
			PPA = true;
			PPA_Postback_URL = "https://www.bestpriceprotection.com/PostBackUrl";
		}
		#endregion
	}
}

