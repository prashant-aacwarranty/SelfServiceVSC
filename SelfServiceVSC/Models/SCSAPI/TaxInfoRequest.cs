namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// TaxInfoRequest object for GenerateContract object
	/// </summary>
	public class TaxInfoRequest
	{
		#region Properties
		/// <summary>
		/// Net Tax Amount 1
		/// Allows for tax amount to be passed in with the contract request.
		/// If Tax is passed with the eContracting request, do not calculate sales tax per SCS configurations, if they exist.
		/// </summary>
		public Decimal? NetTaxAmt1 { get; set; } = null;

		/// <summary>
		/// Net Tax Amount 2
		/// Allows for tax amount to be passed in with the contract request.
		/// If Tax is passed with the eContracting request, do not calculate sales tax per SCS configurations, if they exist.
		/// </summary>
		public Decimal? NetTaxAmt2 { get; set; } = null;

		/// <summary>
		/// Retail Tax Amount 1
		/// Allows for tax amount to be passed in with the contract request.
		/// If Tax is passed with the eContracting request, do not calculate sales tax per SCS configurations, if they exist.
		/// </summary>
		public Decimal? RetailTaxAmt1 { get; set; } = null;

		/// <summary>
		/// Retail Tax Amount 2
		/// Allows for tax amount to be passed in with the contract request.
		/// If Tax is passed with the eContracting request, do not calculate sales tax per SCS configurations, if they exist.
		/// </summary>
		public Decimal? RetailTaxAmt2 { get; set; } = null;
		#endregion
	}
}
