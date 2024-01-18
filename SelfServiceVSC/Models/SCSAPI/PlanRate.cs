namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Plan rate object for GetRatesResponse object
	/// </summary>
	public class PlanRate
	{
		#region Properties
		/// <summary>
		/// Object contains plan definition.
		/// </summary>
		public Plan Plan { get; set; } = null;

		/// <summary>
		/// Collection (array 1..N) of RateClassMoney.
		/// </summary>
		public List<RateClassMoney> RateClassMoneys { get; set; } = null;

		/// <summary>
		/// Collection (array 1..N) of AdditionalContractInfo
		/// </summary>
		public List<AdditionalContractInfo> AdditionalContractInfos { get; set; } = null;

		/// <summary>
		/// Obsolete. No value will be returned.
		/// </summary>
		public String ContractNo { get; set; } = null;

		/// <summary>
		/// Vehicle Type either N = New or U = used
		/// </summary>
		public String NewUsed { get; set; } = null;

		/// <summary>
		/// Obsolete. No value will be returned.
		/// </summary>
		public String ProgramTypeCode { get; set; } = null;

		/// <summary>
		/// Obsolete. No value will be returned.
		/// </summary>
		public String SalesAgentNo { get; set; } = null;

		/// <summary>
		/// Form Number configured in SCS
		/// </summary>
		public String PDFFormNo { get; set; } = null;
		#endregion
	}
}
