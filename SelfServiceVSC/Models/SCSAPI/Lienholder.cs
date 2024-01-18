namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Lienholder object for GetLienholderResult object.
	/// </summary>
	public class Lienholder
	{
		#region Properties
		/// <summary>
		/// Lienholder Number
		/// </summary>
		public String Number { get; set; } = null;

		/// <summary>
		/// Lienholder Name
		/// </summary>
		public String Name { get; set; } = null;

		/// <summary>
		/// Lienholder Address 1
		/// </summary>
		public String Address1 { get; set; } = null;

		/// <summary>
		/// Lienholder Address 2
		/// </summary>
		public String Address2 { get; set; } = null;

		/// <summary>
		/// Lienholder City
		/// </summary>
		public String City { get; set; } = null;

		/// <summary>
		/// Lienholder State
		/// </summary>
		public String State { get; set; } = null;

		/// <summary>
		/// Lienholder ZIP Code
		/// </summary>
		public String Zip { get; set; } = null;

		public Boolean? Verified { get; set; } = null;
		
		public Boolean VerifiedSpecified { get { return Verified != null; } }

		public Boolean? Preferred { get; set; } = null;
		
		public Boolean PreferredSpecified { get { return Preferred != null; } }

		/// <summary>
		/// Statement Type
		/// </summary>
		public String StatementType { get; set; } = null;

		/// <summary>
		/// Lienholder Type Code
		/// </summary>
		public String LienTypeCode { get; set; } = null;

		/// <summary>
		/// Lienholder Group Number
		/// </summary>
		public String LienHolderGroupNumber { get; set; } = null;
		#endregion
	}
}
