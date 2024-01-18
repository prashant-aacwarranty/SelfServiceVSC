namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// GetTrimsByVINResult object for GetTrimsByVINResponse object.
	/// </summary>
	public class GetTrimsByVINResult
	{
		#region Properties
		/// <summary>
		/// 0 = GetTrimsByVIN failed.
		/// 1 = Successful, GetTrimsByVIN passed.
		/// 2 = Successful, GetTrimsByVIN passed with warning.
		/// </summary>
		public Byte? ResponseCode { get; set; } = null;
		
		public Boolean ResponseCodeSpecified { get { return ResponseCode != null; } }

		/// <summary>
		/// Collection of Messages.
		/// </summary>
		public List<Message> Messages { get; set; } = null;

		/// <summary>
		/// Collection (array 1..N) of Trims.
		/// </summary>
		public List<Trim> Trims { get; set; } = null;
		#endregion
	}
}
