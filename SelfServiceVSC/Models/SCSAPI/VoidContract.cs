using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Schema used to void a contract in SCS.
	/// </summary>
	[XmlRoot(ElementName = "ContractVoid")]
	public class VoidContract
	{
		#region Properties
		/// <summary>
		/// Third Party Administrator Code
		/// </summary>
		public String TpaCode { get; set; } = null;

		/// <summary>
		/// User ID issued to the requesting system
		/// </summary>
		public String UserId { get; set; } = null;

		/// <summary>
		/// Password issued to the requesting system
		/// </summary>
		public String Password { get; set; } = null;

		/// <summary>
		/// The name of the F&I manager, office manager or other user of the system that is requesting the void.
		/// </summary>
		public String RequestingUser { get; set; } = null;

		/// <summary>
		/// Contract Number, If not provided SCS will generate one
		/// </summary>
		public String ContractNumber { get; set; } = null;

		/// <summary>
		/// Vehicle Identification Number.
		/// </summary>
		public String VIN { get; set; } = null;

		/// <summary>
		/// Dealer Number
		/// </summary>
		public String DealerNumber { get; set; } = null;
		#endregion

		#region Constructors
		public VoidContract()
		{

		}

		public VoidContract(
			SessionDetails sessionDetails)
		{
			var rate = sessionDetails.SelectedRate;
			var surcharges = rate.RateClassMoney.Options.Where(option => option.IsSurcharge == true).Sum(option => option.RetailRate);

			TpaCode = "AAC";
			UserId = SelfServiceVSC.AppSettings.SCSAPIUserId;
			Password = SelfServiceVSC.AppSettings.SCSAPIPassword;
			RequestingUser = "DTC";
			ContractNumber = sessionDetails.ContractNumber;
			VIN = sessionDetails.EstimateRequest.VIN;
			DealerNumber = "DTC001";
		}
		#endregion
	}
}
