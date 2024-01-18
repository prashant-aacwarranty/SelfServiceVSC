namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Get lienholders request object.
	/// </summary>
	public class GetLienholders
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
		/// Lienholder number used for trim lookup. If lienholder number is not specified all lienholders will be returned.
		/// </summary>
		public String LienNumber { get; set; } = null;

		/// <summary>
		/// If ‘true’ then only lienholders marked as ‘Verified’ will be returned. If ’false’ then only lienholders that are not verified will be returned. (Note: to get all lienholders, do not include value for Verified).
		/// </summary>
		public Boolean? Verified { get; set; } = null;
		
		public Boolean VerifiedSpecified { get { return Verified != null; } }

		/// <summary>
		/// If passed, the service will return only Lienholders that are associated to the dealer based upon the configuration settings in the system.
		/// </summary>
		public String DealerNumber { get; set; } = null;

		/// <summary>
		/// If Lienholder Group number is specified, the service will return the Lienholders in the specified Lienholder Group.
		/// </summary>
		public String LienHolderGroupNumber { get; set; } = null;
		#endregion
	}
}
