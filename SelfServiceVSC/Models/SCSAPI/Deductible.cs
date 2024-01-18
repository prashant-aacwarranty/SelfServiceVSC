namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Deductible object for RateClassMoney object
	/// </summary>
	public class Deductible
	{
		#region Properties
		/// <summary>
		/// DeductTypeDescriptions object. Contains culture name for display. Currently, this will always have CultureName = EN-US
		/// </summary>
		public DeductTypeDescriptions DeductTypeDescriptions { get; set; } = null;

		/// <summary>
		/// The unique identification number of the deductible
		/// </summary>
		public Int64? DeductId { get; set; } = null;
		
		public Boolean DeductIdSpecified { get { return DeductId != null; } }

		/// <summary>
		/// Deductible amount
		/// Valid values:
		///		Max value is 1000
		/// </summary>
		public Decimal? DeductAmt { get; set; } = null;
		
		public Boolean DeductAmtSpecified { get { return DeductAmt != null; } }

		/// <summary>
		/// Deductible type.
		/// When deductible type = PERCENT, the value in the DeductAmt field should be displayed as a percent not dollar amount(example: when DeductType = PERCENT and DeductAmt = 50.00, display as 50% not $50.)
		/// Valid values:
		///		“ ” = Standard;
		///		“DISP” = Disappearing;
		///		“REDUCING” = Reducing; “PERCENT” = percentage
		/// </summary>
		public String DeductType { get; set; } = null;

		/// <summary>
		/// Reducing amount, applicable when DeductType is REDUCING.
		/// </summary>
		public Decimal? ReducedAmount { get; set; } = null;
		
		public Boolean ReducedAmountSpecified { get { return ReducedAmount != null; } }
		#endregion
	}
}
