namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// LienHolder object for GenerateContract object.
	/// </summary>
	public class ContractLienHolder
	{
		#region Properties
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
		/// Lienholder State
		/// </summary>
		public String State { get; set; } = null;

		/// <summary>
		/// Lienholder phone
		/// </summary>
		public String Phone { get; set; } = null;

		/// <summary>
		/// Lienholder City
		/// </summary>
		public String City { get; set; } = null;

		/// <summary>
		/// Lienholder ZIP Code
		/// </summary>
		public String ZipCode { get; set; } = null;

		/// <summary>
		/// Lienholder Number
		/// </summary>
		public String Number { get; set; } = null;
		#endregion
	}
}
