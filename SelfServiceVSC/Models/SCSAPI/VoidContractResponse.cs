using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Schema returned when voiding a contract in SCS.
	/// </summary>
	[XmlRoot(ElementName = "ContractVoid")]
	public class VoidContractResponse
	{
		#region Properties
		/// <summary>
		/// 1 = Successful, the contract was voided
		/// 0 = Contract was not voided, see Message Object for Type
		/// </summary>
		public Byte? ResponseCode { get; set; } = null;

		/// <summary>
		/// Collection of Messages.
		/// </summary>
		public List<Message> Messages { get; set; } = null;
		#endregion
	}
}
