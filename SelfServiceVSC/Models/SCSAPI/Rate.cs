namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Rate object for RateClassMoney object
	/// </summary>
	public class Rate
	{
		#region Properties
		/// <summary>
		/// Not used.
		/// </summary>
		public String BandCode { get; set; } = null;

		/// <summary>
		/// Override Flag
		/// </summary>
		public Boolean? OverrideFlag { get; set; } = null;
		
		public Boolean OverrideFlagSpecified { get { return OverrideFlag != null; } }

		/// <summary>
		/// Unique number that identifies option group
		/// </summary>
		public Int64? OptionGroupId { get; set; } = null;
		
		public Boolean OptionGroupIdSpecified { get { return OptionGroupId != null; } }

		/// <summary>
		/// Unique number that identifies rate
		/// </summary>
		public Int64? RateId { get; set; } = null;
		
		public Boolean RateIdSpecified { get { return RateId != null; } }

		/// <summary>
		/// Retail Rate for the product
		/// </summary>
		public Decimal? RetailRate { get; set; } = null;
		
		public Boolean RetailRateSpecified { get { return RetailRate != null; } }

		/// <summary>
		/// Dealer Cost for the product.
		/// </summary>
		public Decimal? NetRate { get; set; } = null;
		
		public Boolean NetRateSpecified { get { return NetRate != null; } }

		/// <summary>
		/// Maximum Retail Rate for the product.
		/// </summary>
		public Decimal? MaxRetailRate { get; set; } = null;
		
		public Boolean MaxRetailRateSpecified { get { return MaxRetailRate != null; } }

		/// <summary>
		/// Minimum Retail Rate for the product
		/// </summary>
		public Decimal? MinRetailRate { get; set; } = null;
		
		public Boolean MinRetailRateSpecified { get { return MinRetailRate != null; } }

		/// <summary>
		/// Regulated Rule Code defined for the product. See Rating Rules for details.
		/// Valid values:
		///		0 – Non-Regulated Product
		///		3 – Where Retail Rate has to be equal to RetailRate.Markups cannot be applied to this rate.
		///		5 – Where Markup must be between MarkupMin and MarkupMax.
		///		6 – Where the Retail Rate (MaxRetailRate) is not greater than the Amount Financed (FinancedAmount) minus the configured MSRP or NADA %.
		/// </summary>
		public Byte? RegulatedRuleId { get; set; } = null;
		
		public Boolean RegulatedRuleIdSpecified { get { return RegulatedRuleId != null; } }

		/// <summary>
		/// Form Number that uniquely identifies the contract form to be used if this quote is used for a contract.
		/// </summary>
		public String PDFFormNo { get; set; } = null;

		/// <summary>
		/// Vehicle Class
		/// </summary>
		public String VehicleClass { get; set; } = null;

		/// <summary>
		/// Expiration Date of the contract if it were to be sold using this quote
		/// </summary>
		public DateTime? ExpirationDate { get; set; } = null;
		
		public Boolean ExpirationDateSpecified { get { return ExpirationDate != null; } }

		/// <summary>
		/// Expiration Mileage of the contract it if were to be sold using this quote
		/// Will be 10000 when product type is GAP
		/// </summary>
		public Int32? ExpirationMileage { get; set; } = null;
		
		public Boolean ExpirationMileageSpecified { get { return ExpirationMileage != null; } }

		/// <summary>
		/// The minimum amount of markup that must be applied to the net cost for this quote.
		/// </summary>
		public Decimal? MarkupMin { get; set; } = null;
		
		public Boolean MarkupMinSpecified { get { return MarkupMin != null; } }

		/// <summary>
		/// The maximum amount of markup that can be applied to the net cost for this quote.
		/// </summary>
		public Decimal? MarkupMax { get; set; } = null;
		
		public Boolean MarkupMaxSpecified { get { return MarkupMax != null; } }

		public Tax Tax { get; set; } = null;

		public RateDetails RateDetails { get; set; } = null;
		#endregion
	}
}
