namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// TermMile object for RateClassMoney object
	/// </summary>
	public class TermMile
	{
		#region Properties
		/// <summary>
		/// The unique number of term
		/// </summary>
		public Int64? TermId { get; set; } = null;
		
		public Boolean TermIdSpecified { get { return TermId != null; } }

		/// <summary>
		/// Term
		/// </summary>
		public Int16? Term { get; set; } = null;
		
		public Boolean TermSpecified { get { return Term != null; } }

		/// <summary>
		/// Mileage
		/// </summary>
		public Int32? Mileage { get; set; } = null;
		
		public Boolean MileageSpecified { get { return Mileage != null; } }
		#endregion
	}
}
