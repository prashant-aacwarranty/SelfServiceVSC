namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Get trims by VIN request object.
	/// </summary>
	public class GetTrimsByVIN
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
		/// VIN used for trim lookup.
		/// </summary>
		public String VIN { get; set; } = null;
		#endregion
	}
}
