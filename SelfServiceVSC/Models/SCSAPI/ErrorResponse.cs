namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Error response object.
	/// </summary>
	public class ErrorResponse
	{
		#region Properties
		/// <summary>
		/// Error number.
		/// </summary>
		public Int32? ErrorNumber { get; set; } = null;
		
		public Boolean ErrorNumberSpecified { get { return ErrorNumber != null; } }

		/// <summary>
		/// Error code.
		/// </summary>
		public String ErrorCode { get; set; } = null;

		/// <summary>
		/// Error Message.
		/// </summary>
		public String ErrorDescription { get; set; } = null;
		#endregion
	}
}
