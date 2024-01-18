namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// ManufactureWarranty object for GetRatesResponse object.
	/// </summary>
	public class ManufactureWarranty
	{
		#region Properties
		/// <summary>
		/// Type of warranty
		/// Valie values:
		///		BASE
		///		POWERTRAIN
		///		EMISSION
		/// </summary>
		public String Type { get; set; } = null;

		/// <summary>
		/// Manufacturer Warranty Term
		/// </summary>
		public Int16? Term { get; set; } = null;
		
		public Boolean TermSpecified { get { return Term != null; } }

		/// <summary>
		/// Manufacturer Warranty Mileage
		/// </summary>
		public Int32? Mile { get; set; } = null;
		
		public Boolean MileSpecified { get { return Mile != null; } }
		#endregion
	}
}
