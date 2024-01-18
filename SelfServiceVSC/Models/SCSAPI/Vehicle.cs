namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Vehicle object for GenerateContractRequest object
	/// </summary>
	public class Vehicle
	{
		#region Properties
		/// <summary>
		/// VIN
		/// </summary>
		public String VinNumber { get; set; } = null;

		/// <summary>
		/// Vehicle Type New or Used. This must be the same as the value for NewUsed for the selected plan from the rating response.
		/// Valid values:
		///		N
		///		U
		/// </summary>
		public String VehicleType { get; set; } = null;

		public String AssetType { get; set; } = null;

		public Int16? Year { get; set; } = null;
		
		public Boolean YearSpecified { get { return Year != null; } }

		public String Model { get; set; } = null;

		public String Trim { get; set; } = null;

		public Int32? EngineSize { get; set; } = null;
		
		public Boolean EngineSizeSpecified { get { return EngineSize != null; } }

		/// <summary>
		/// Valid values:
		///		CC
		///		CID
		/// </summary>
		public String EngineSizeUnit { get; set; } = null;

		public String Cyl { get; set; } = null;

		public String Drv { get; set; } = null;

		public String Fuel { get; set; } = null;

		public String AspirationCode { get; set; } = null;

		public String SegmentationCode { get; set; } = null;

		public String TonRating { get; set; } = null;

		public String GVWR { get; set; } = null;

		public String BodyStyle { get; set; } = null;

		/// <summary>
		/// Ownership of the vehicle
		/// Valid values:
		///		<blank>
		///		‘N’ – New
		///		‘P’ – Pre-Owned
		/// </summary>
		public String Ownership { get; set; } = null;
		#endregion
	}
}
