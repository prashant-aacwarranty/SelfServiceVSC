namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Filter object for GetRatesRequests
	/// </summary>
	public class Filter
	{
		#region Properties
		/// <summary>
		/// Minimum Term Months
		/// </summary>
		public Int64? TermMin { get; set; } = null;
		
		public Boolean TermMinSpecified { get { return TermMin != null; } }

		/// <summary>
		/// Maximum Term Months
		/// </summary>
		public Int64? TermMax { get; set; } = null;
		
		public Boolean TermMaxSpecified { get { return TermMax != null; } }

		/// <summary>
		/// Minimum Term Mileage
		/// </summary>
		public Int32? MileageMin { get; set; } = null;
		
		public Boolean MileageMinSpecified { get { return MileageMin != null; } }

		/// <summary>
		/// Maximum Term Mileage
		/// </summary>
		public Int32? MileageMax { get; set; } = null;
		
		public Boolean MileageMaxSpecified { get { return MileageMax != null; } }
		#endregion
	}
}
