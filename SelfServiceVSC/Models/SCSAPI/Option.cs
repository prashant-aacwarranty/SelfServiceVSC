namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Option object for Options object
	/// </summary>
	public class Option
	{
		#region Properties
		/// <summary>
		/// Not used.
		/// </summary>
		public String OptionDescriptions { get; set; } = null;

		/// <summary>
		/// ID for this option
		/// </summary>
		public Int64? OptionId { get; set; } = null;
		
		public Boolean OptionIdSpecified { get { return OptionId != null; } }

		/// <summary>
		/// Obsolete. This element of the XML is no longer in use.
		/// Valid values:
		///		0 - disabled
		///		1 - enabled
		/// </summary>
		public Byte? Status { get; set; } = null;
		
		public Boolean StatusSpecified { get { return Status != null; } }

		/// <summary>
		/// Description of the option
		/// </summary>
		public String OptionDesc { get; set; } = null;

		/// <summary>
		/// Option Name used to map on contract PDF
		/// </summary>
		public String OptionName { get; set; } = null;

		/// <summary>
		/// Retail rate of the option
		/// </summary>
		public Decimal? RetailRate { get; set; } = null;
		
		public Boolean RetailRateSpecified { get { return RetailRate != null; } }

		/// <summary>
		/// Dealer cost of the Option
		/// </summary>
		public Decimal? NetRate { get; set; } = null;
		
		public Boolean NetRateSpecified { get { return NetRate != null; } }

		/// <summary>
		/// Not used.
		/// </summary>
		public Decimal? PremiumRate { get; set; } = null;
		
		public Boolean PremiumRateSpecified { get { return PremiumRate != null; } }

		/// <summary>
		/// Not used.
		/// </summary>
		public Decimal? CommissionRate { get; set; } = null;
		
		public Boolean CommissionRateSpecified { get { return CommissionRate != null; } }

		/// <summary>
		/// Not used.
		/// </summary>
		public Decimal? AdminRate { get; set; } = null;
		
		public Boolean AdminRateSpecified { get { return AdminRate != null; } }

		/// <summary>
		/// Obsolete. This element of the XML is no longer in use.
		/// </summary>
		public Boolean? IsUsed { get; set; } = null;
		
		public Boolean IsUsedSpecified { get { return IsUsed != null; } }

		/// <summary>
		/// Identifies whether the option a surcharge or not. If ‘true’ then the option is a surcharge.
		/// </summary>
		public Boolean? IsSurcharge { get; set; } = null;
		
		public Boolean IsSurchargeSpecified { get { return IsSurcharge != null; } }

		/// <summary>
		/// Obsolete. This element of the XML is no longer in use.
		/// </summary>
		public Boolean? IsSurchargeOption { get; set; } = null;
		
		public Boolean IsSurchargeOptionSpecified { get { return IsSurchargeOption != null; } }

		/// <summary>
		/// Form number
		/// </summary>
		public String PDFFormNo { get; set; } = null;
		#endregion
	}
}
