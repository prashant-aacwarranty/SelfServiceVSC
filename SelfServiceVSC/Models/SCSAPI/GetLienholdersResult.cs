namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Get lienholders result object for GetLienholdersRequest object.
	/// </summary>
	public class GetLienholdersResult
	{
		#region Properties
		/// <summary>
		/// 0 = GetLienholders failed.
		/// 1 = Successful, GetLienholders passed.
		/// 2 = Successful, GetLienholders passed with warning.
		/// </summary>
		public Int16? ResponseCode { get; set; } = null;
		
		public Boolean ResponseCodeSpecified { get { return ResponseCode != null; } }

		/// <summary>
		/// Collection of Messages.
		/// </summary>
		public List<Message> Messages { get; set; } = null;

		/// <summary>
		/// Collection (array 1..N) of Lienholders.
		/// </summary>
		public List<Lienholder> Lienholders { get; set; } = null;
		#endregion
	}
}
