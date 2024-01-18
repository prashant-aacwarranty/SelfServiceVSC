namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Address object for Customer object
	/// </summary>
	public class Address
	{
		#region Properties
		public String Address1 { get; set; } = null;

		public String Address2 { get; set; } = null;

		public String City { get; set; } = null;

		public String State { get; set; } = null;

		public String ZipCode { get; set; } = null;

		public String HomePhone { get; set; } = null;

		public String HomePhoneExt { get; set; } = null;

		public String WorkPhone { get; set; } = null;

		public String WorkPhoneExt { get; set; } = null;
		#endregion
	}
}
