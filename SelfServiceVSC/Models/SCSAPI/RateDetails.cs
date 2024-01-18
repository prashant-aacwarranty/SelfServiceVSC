namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// RateDetails object for Rate object
	/// </summary>
	public class RateDetails
	{
		#region Properties
		/// <summary>
		/// Amount that must be remitted. This is the dealer cost. This value will be the same as the NetRate value, which is what the dealer is expected to remit to the product provide/administrator upon sale of a contract.
		/// </summary>
		public Decimal? Remit { get; set; } = null;
		
		public Boolean RemitSpecified { get { return Remit != null; } }

		/// <summary>
		/// This is the F&I manager's cost. Cost = Remit + Admin Markup + Base Markup. When the plan is regulated retail, Cost = remit + base markup.
		/// </summary>
		public Decimal? Cost { get; set; } = null;
		
		public Boolean CostSpecified { get { return Cost != null; } }

		/// <summary>
		/// Retail is the Remit + Total Markup (Advanced + Base Markup + F&I Markup)
		/// </summary>
		public Decimal? Retail { get; set; } = null;
		
		public Boolean RetailSpecified { get { return Retail != null; } }

		/// <summary>
		/// This is the Advanced Markup amount from SCS.
		/// </summary>
		public Decimal? AdminMarkup { get; set; } = null;
		
		public Boolean AdminMarkupSpecified { get { return AdminMarkup != null; } }

		/// <summary>
		/// Base markup amount defined in the Markup Settings from the SEFI Seller Portal (DAP).
		/// </summary>
		public Decimal? BaseMarkup { get; set; } = null;
		
		public Boolean BaseMarkupSpecified { get { return BaseMarkup != null; } }

		/// <summary>
		/// F&I Markup amount defined in the Markup Settings from the SEFI Seller Portal (DAP).
		/// </summary>
		public String FIMarkup { get; set; } = null;
		#endregion
	}
}
