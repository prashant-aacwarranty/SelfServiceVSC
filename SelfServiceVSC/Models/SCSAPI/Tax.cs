namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Tax object for Rate object
	/// </summary>
	public class Tax
	{
		#region Properties
		/// <summary>
		/// Returned if taxes are configured.
		/// </summary>
		public Decimal? NetTaxAmt1 { get; set; } = null;

		/// <summary>
		/// Returned if taxes are configured.
		/// </summary>
		public Decimal? NetTaxAmt2 { get; set; } = null;

		/// <summary>
		/// Returned if taxes are configured.
		/// </summary>
		public Decimal? RetailTaxAmt1 { get; set; } = null;

		/// <summary>
		/// Returned if taxes are configured.
		/// </summary>
		public Decimal? RetailTaxAmt2 { get; set; } = null;

		/// <summary>
		/// Returned if taxes are configured.
		/// </summary>
		public Decimal? TotalRetailTaxAmt { get; set; } = null;
		
		public Boolean TotalRetailTaxAmtSpecified { get { return TotalRetailTaxAmt != null; } }

		/// <summary>
		/// Returned if taxes are configured.
		/// </summary>
		public String Tax1TaxLabel { get; set; } = null;

		/// <summary>
		/// Returned if taxes are configured.
		/// </summary>
		public String Tax2TaxLabel { get; set; } = null;
		#endregion
	}
}
