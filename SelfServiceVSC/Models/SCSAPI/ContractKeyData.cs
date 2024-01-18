namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// ContractKeyData object for GetRatesResult object.
	/// </summary>
	public class ContractKeyData
	{
		#region Properties
		public Gap Gap { get; set; } = null;
		public Vsc Vsc { get; set; } = null;
		public String ProgramTypeCode { get; set; } = null;
		public String SalesAgentNo { get; set; } = null;
		public String PDFFormNo { get; set; } = null;
		#endregion
	}
}
