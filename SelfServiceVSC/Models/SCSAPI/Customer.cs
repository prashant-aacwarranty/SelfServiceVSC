namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Customer object for GenerateContractRequest object
	/// </summary>
	public class Customer
	{
		#region Properties
		public String Title { get; set; } = null;


		public String FirstName { get; set; } = null;


		public String LastName { get; set; } = null;


		public String MiddleInitial { get; set; } = null;


		public String Email { get; set; } = null;

		/// <summary>
		/// Address object
		/// </summary>
		public Address Address { get; set; } = null;

		/// <summary>
		/// Co-Buyer name
		/// </summary>
		public String AlternateContact { get; set; } = null;
		#endregion
	}
}
