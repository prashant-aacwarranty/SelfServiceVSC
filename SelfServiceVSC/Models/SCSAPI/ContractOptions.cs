namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// ContractOptions object for GetLienholderResult object.
	/// </summary>
	public class ContractOptions
	{
		#region Properties
		/// <summary>
		/// Grouping tag used to allow specifying more than one contract option in a single request.
		/// </summary>
		public String ContractOption { get; set; } = null;

		/// <summary>
		/// Value from rating engine response (OptionId). Required if contract includes options or surcharges.
		/// </summary>
		public Int64? Id { get; set; } = null;
		
		public Boolean IdSpecified { get { return Id != null; } }

		/// <summary>
		/// Value from rating engine response (OptionDesc). Required if contract includes options or surcharges.
		/// </summary>
		public String Description { get; set; } = null;

		/// <summary>
		/// Value from rating engine response (IsSurcharge). Required if contract includes options or surcharges.
		/// </summary>
		public Boolean? IsSurcharge { get; set; } = null;
		
		public Boolean IsSurchargeSpecified { get { return IsSurcharge != null; } }

		/// <summary>
		/// Value from rating engine response (NetRate). Required if contract includes options or surcharges.
		/// </summary>
		public Decimal? NetCost { get; set; } = null;
		
		public Boolean NetCostSpecified { get { return NetCost != null; } }
		#endregion
	}
}
