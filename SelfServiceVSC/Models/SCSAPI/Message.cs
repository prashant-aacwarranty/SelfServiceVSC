namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Message object.
	/// </summary>
	public class Message
	{
		#region Properties
		/// <summary>
		/// Message Type
		/// Valid values:
		///		1 – Error
		///		2 – Info
		///		3 – Warning
		///		4 - Success
		/// </summary>
		public Int16? Type { get; set; } = null;
		
		public Boolean TypeSpecified { get { return Type != null; } }

		/// <summary>
		/// Returns the associated message code with each message text.
		/// </summary>
		public String Code { get; set; } = null;

		/// <summary>
		/// Message Text
		/// </summary>
		public String Text { get; set; } = null;
		#endregion
	}
}
