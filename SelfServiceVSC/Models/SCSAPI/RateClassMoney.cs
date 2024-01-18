namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// RateClassMoney object for PlanRate object
	/// </summary>
	public class RateClassMoney
	{
		#region Properties
		/// <summary>
		/// Object contains Term Mile definition.
		/// </summary>
		public TermMile TermMile { get; set; } = null;

		/// <summary>
		/// Object contains Deductible definition.
		/// </summary>
		public Deductible Deductible { get; set; } = null;

		/// <summary>
		/// Object Containing Rate
		/// </summary>
		public Rate Rate { get; set; } = null;

		/// <summary>
		/// Collection of Options
		/// </summary>
		public List<Option> Options { get; set; } = null;
		#endregion
	}
}
