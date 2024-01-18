using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Schema used to request quotes from SCS.
	/// </summary>
	[XmlRoot(ElementName = "contract")]
	public class GenerateContract
	{
		#region Properties
		/// <summary>
		/// Third Party Administrator Code
		/// </summary>
		public String TpaCode { get; set; } = null;

		/// <summary>
		/// User ID issued to the requesting system
		/// </summary>
		public String UserId { get; set; } = null;

		/// <summary>
		/// Password issued to the requesting system
		/// </summary>
		public String Password { get; set; } = null;

		/// <summary>
		/// Contract Number, If not provided SCS will generate one
		/// </summary>
		public String ContractNumber { get; set; } = null;

		/// <summary>
		/// Dealer Number
		/// </summary>
		public String DealerNumber { get; set; } = null;

		/// <summary>
		/// This is the PDFFormNo from the rating response.
		/// </summary>
		public String ContractFormNumber { get; set; } = null;

		/// <summary>
		/// Register Number
		/// </summary>
		public String RegisterNumber { get; set; } = null;

		/// <summary>
		/// In Service Date (manufacturer warranty start date. Required if the vehicle type is NEW and product type is not GAP
		/// </summary>
		[XmlElement(DataType = "date")]
		public DateTime? InserviceDate { get; set; } = null;
		
		public Boolean InserviceDateSpecified { get { return InserviceDate != null; } }

		/// <summary>
		/// Sale Date
		/// </summary>
		[XmlElement(DataType = "date")]
		public DateTime? SaleDate { get; set; } = null;
		
		public Boolean SaleDateSpecified { get { return SaleDate != null; } }

		/// <summary>
		/// Vehicle Purchase Date. Required for some products.
		/// </summary>
		[XmlElement(DataType = "date")]
		public DateTime? VehiclePurchaseDate { get; set; } = null;
		
		public Boolean VehiclePurchaseDateSpecified { get { return VehiclePurchaseDate != null; } }

		public Int32? SaleOdometer { get; set; } = null;
		
		public Boolean SaleOdometerSpecified { get { return SaleOdometer != null; } }

		public Int16? ManufWarrTerm { get; set; } = null;
		
		public Boolean ManufWarrTermSpecified { get { return ManufWarrTerm != null; } }

		public Int32? ManufWarrMiles { get; set; } = null;
		
		public Boolean ManufWarrMilesSpecified { get { return ManufWarrMiles != null; } }

		/// <summary>
		/// Required for some products.
		/// </summary>
		public Decimal? VehiclePurchasePrice { get; set; } = null;
		
		public Boolean VehiclePurchasePriceSpecified { get { return VehiclePurchasePrice != null; } }

		/// <summary>
		/// Value from eRating response that identifies the Quote that was used to produce the contract.
		/// </summary>
		public Int64? QuoteId { get; set; } = null;
		
		public Boolean QuoteIdSpecified { get { return QuoteId != null; } }

		/// <summary>
		/// Value from eRating response
		/// </summary>
		public String PlanCode { get; set; } = null;

		/// <summary>
		/// Value from eRating response
		/// </summary>
		public Int64? RateBook { get; set; } = null;
		
		public Boolean RateBookSpecified { get { return RateBook != null; } }

		/// <summary>
		/// True = PDF will be generated and returned in Contract response
		/// </summary>
		public Boolean? GenerateContractDocument { get; set; } = null;
		
		public Boolean GenerateContractDocumentSpecified { get { return GenerateContractDocument != null; } }

		/// <summary>
		/// False = Preview contract will be generated. Validation on all fields will be performed but no contract created.
		/// True = Contract created.
		/// Default Value is true
		/// </summary>
		public Boolean? FinalCopy { get; set; } = null;
		
		public Boolean FinalCopySpecified { get { return FinalCopy != null; } }

		/// <summary>
		/// The amount due to the provider for the contract (aka Remit Amount). This value should be the sum of net rate for the selected plan plus the net cost for each of the selected options and surcharges.
		/// </summary>
		public Decimal? NetCost { get; set; } = null;
		
		public Boolean NetCostSpecified { get { return NetCost != null; } }

		/// <summary>
		/// The price charged to the customer for the contract. This value should be the sum of net rate for the selected plan plus the net cost for each of the selected options and surcharges plus the markup added by the dealer.
		/// </summary>
		public Decimal? RetailCost { get; set; } = null;
		
		public Boolean RetailCostSpecified { get { return RetailCost != null; } }

		/// <summary>
		/// Base markup. If base markup is specified, then RetailCost must equal NetCost + BaseMarkup + FIMarkup
		/// </summary>
		public Decimal? BaseMarkup { get; set; } = null;
		
		public Boolean BaseMarkupSpecified { get { return BaseMarkup != null; } }

		/// <summary>
		/// F&I Markup. If F&I markup is specified, then RetailCost must equal NetCost + BaseMarkup + FIMarkup
		/// </summary>
		public Decimal? FIMarkup { get; set; } = null;
		
		public Boolean FIMarkupSpecified { get { return FIMarkup != null; } }

		/// <summary>
		/// Required for some products such as GAP. Values are BALL (balloon), LEAS (lease), LOAN, and CASH.
		/// </summary>
		public String FinanceType { get; set; } = null;

		/// <summary>
		/// Required for some products such as GAP.
		/// </summary>
		public Decimal? FinancedAmount { get; set; } = null;
		
		public Boolean FinancedAmountSpecified { get { return FinancedAmount != null; } }

		/// <summary>
		/// Required for some products such as GAP.
		/// </summary>
		public Decimal? FinanceAPR { get; set; } = null;
		
		public Boolean FinanceAPRSpecified { get { return FinanceAPR != null; } }

		/// <summary>
		/// Required for some products such as GAP.
		/// </summary>
		public Int16? FinanceTerm { get; set; } = null;
		
		public Boolean FinanceTermSpecified { get { return FinanceTerm != null; } }

		/// <summary>
		/// Required for some products such as GAP.
		/// </summary>
		public Decimal? Msrp { get; set; } = null;
		
		public Boolean MsrpSpecified { get { return Msrp != null; } }

		/// <summary>
		/// GAP contract supplemental Loan/Lease information
		/// </summary>
		public Decimal? MonthlyPayment { get; set; } = null;
		
		public Boolean MonthlyPaymentSpecified { get { return MonthlyPayment != null; } }

		/// <summary>
		/// GAP contract supplemental Loan/Lease information
		/// </summary>
		public Decimal? FirstPaymentDate { get; set; } = null;
		
		public Boolean FirstPaymentDateSpecified { get { return FirstPaymentDate != null; } }

		/// <summary>
		/// GAP contract supplemental Loan/Lease information
		/// </summary>
		public Decimal? BalloonAmount { get; set; } = null;
		
		public Boolean BalloonAmountSpecified { get { return BalloonAmount != null; } }

		/// <summary>
		/// GAP contract supplemental Loan/Lease information
		/// </summary>
		public Decimal? ResidualValue { get; set; } = null;
		
		public Boolean ResidualValueSpecified { get { return ResidualValue != null; } }

		/// <summary>
		/// Base ACV is the retail price of the car minus all the options (e.g. roof rack, floor mats, etc.)
		/// </summary>
		public Decimal? BaseACV { get; set; } = null;
		
		public Boolean BaseACVSpecified { get { return BaseACV != null; } }

		/// <summary>
		/// GAP contract supplemental Loan/Lease information
		/// </summary>
		public Decimal? NADAValue { get; set; } = null;
		
		public Boolean NADAValueSpecified { get { return NADAValue != null; } }

		/// <summary>
		/// GAP contract supplemental Loan/Lease information
		/// </summary>
		public Decimal? InsuranceDeductible { get; set; } = null;
		
		public Boolean InsuranceDeductibleSpecified { get { return InsuranceDeductible != null; } }

		/// <summary>
		/// The First Name of the F&I Mgr/Seller employee who sold the contract.
		/// </summary>
		public String SalesPersonFname { get; set; } = null;

		/// <summary>
		/// The Last Name of the F&I Mgr/Seller employee who sold the contract.
		/// </summary>
		public String SalesPersonLname { get; set; } = null;

		/// <summary>
		/// Program name
		/// </summary>
		public String ProgramName { get; set; } = null;

		/// <summary>
		/// Stock number
		/// </summary>
		public String StockNumber { get; set; } = null;

		/// <summary>
		/// DMS deal number
		/// </summary>
		public String DealNumber { get; set; } = null;

		/// <summary>
		/// Term Mile object
		/// </summary>
		public TermMile TermMile { get; set; } = null;

		/// <summary>
		/// Deductible object
		/// </summary>
		public Deductible Deductible { get; set; } = null;

		/// <summary>
		/// Vehicle Information object
		/// </summary>
		public Vehicle Vehicle { get; set; } = null;

		/// <summary>
		/// Customer object
		/// </summary>
		public Customer Customer { get; set; } = null;

		/// <summary>
		/// Lienholder object
		/// </summary>
		public ContractLienHolder LienHolder { get; set; } = null;

		/// <summary>
		/// Contract Options object
		/// </summary>
		public ContractOptions ContractOptions { get; set; } = null;

		/// <summary>
		/// Additional Contract Info object. Required for some products.
		/// </summary>
		public AdditionalContractInfo AdditionalContractInfo { get; set; } = null;

		public TaxInfoRequest TaxInfoRequest { get; set; } = null;

		/// <summary>
		/// Base-64 encoded string that is a digital representation of the filled-out contract in PDF format.
		/// </summary>
		public String ContractPDF { get; set; } = null;

		/// <summary>
		/// Whether the contract is digitally signed or not
		/// </summary>
		public Boolean? DigitallySigned { get; set; } = null;
		
		public Boolean DigitallySignedSpecified { get { return DigitallySigned != null; } }

		/// <summary>
		/// Indicates if the customer is tax exempt.
		/// </summary>
		public Boolean? IsTaxExempt { get; set; } = null;
		
		public Boolean IsTaxExemptSpecified { get { return IsTaxExempt != null; } }

		/// <summary>
		/// Additional Base Warranty Term (Months)
		/// </summary>
		public Int16? AdditionalBaseWarrantyTerm { get; set; } = null;
		
		public Boolean AdditionalBaseWarrantyTermSpecified { get { return AdditionalBaseWarrantyTerm != null; } }

		/// <summary>
		/// Additional Base Warranty Miles
		/// </summary>
		public Int32? AdditionalBaseWarrantyMiles { get; set; } = null;
		
		public Boolean AdditionalBaseWarrantyMilesSpecified { get { return AdditionalBaseWarrantyMiles != null; } }

		/// <summary>
		/// Additional Powertrain Warranty Term (Months)
		/// </summary>
		public Int16? AdditionalPowertrainWarrantyTerm { get; set; } = null;
		
		public Boolean AdditionalPowertrainWarrantyTermSpecified { get { return AdditionalPowertrainWarrantyTerm != null; } }

		/// <summary>
		/// Additional Powertrain Warranty Miles
		/// </summary>
		public Int32? AdditionalPowertrainWarrantyMiles { get; set; } = null;
		
		public Boolean AdditionalPowertrainWarrantyMilesSpecified { get { return AdditionalPowertrainWarrantyMiles != null; } }

		public String RecipientId { get; set; } = null;
		#endregion

		#region Constructors
		public GenerateContract()
		{

		}

		public GenerateContract(
			SessionDetails sessionDetails)
		{
			var rate = sessionDetails.SelectedRate;
			var surcharges = rate.RateClassMoney.Options.Where(option => option.IsSurcharge == true).Sum(option => option.RetailRate);

			TpaCode = "AAC";
			UserId = SelfServiceVSC.AppSettings.SCSAPIUserId;
			Password = SelfServiceVSC.AppSettings.SCSAPIPassword;
			ContractNumber = sessionDetails.ContractNumber;
			DealerNumber = "DTC001";
			ContractFormNumber = rate.RateClassMoney.Rate.PDFFormNo;
			SaleDate = DateTime.Now;
			SaleOdometer = sessionDetails.EstimateRequest.MileageInt;
			QuoteId = Int64.Parse(sessionDetails.EstimateId);
			PlanCode = rate.PlanRate.Plan.PlanCode;
			RateBook = rate.PlanRate.Plan.RateBook;
			GenerateContractDocument = true;
			FinalCopy = false;
			NetCost = rate.RateClassMoney.Rate.NetRate + surcharges;
			RetailCost = rate.RateClassMoney.Rate.RetailRate + surcharges;
			TermMile = rate.RateClassMoney.TermMile;
			Deductible = rate.RateClassMoney.Deductible;
			Vehicle = new Vehicle
			{
				VinNumber = sessionDetails.EstimateRequest.VIN,
				VehicleType = sessionDetails.EstimateRequest.NewUsed
			};
			Customer = new Customer
			{
				Title = "",
				FirstName = sessionDetails.QuoteRequest.FirstName,
				LastName = sessionDetails.QuoteRequest.LastName,
				MiddleInitial = "",
				Email = sessionDetails.QuoteRequest.Email,
				Address = new Address
				{
					Address1 = sessionDetails.QuoteRequest.Address1,
					Address2 = sessionDetails.QuoteRequest.Address2,
					City = sessionDetails.QuoteRequest.City,
					State = sessionDetails.QuoteRequest.State,
					ZipCode = sessionDetails.QuoteRequest.Zip,
					HomePhone = Regex.Replace(sessionDetails.QuoteRequest.Phone, "\\D", "")
				}
			};
			DigitallySigned = true;
			IsTaxExempt = false;
		}
		#endregion
	}
}
