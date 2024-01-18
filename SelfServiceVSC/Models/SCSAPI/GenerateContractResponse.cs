using System.Xml.Serialization;

namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// GenerateContract response object.
	/// </summary>
	public class GenerateContractResponse
	{
		#region Properties
		/// <summary>
		/// 0 = Failed.Error returned.
		/// 1 = Successful.
		/// 2 = Successful, passed with a warning.
		/// </summary>
		public Byte? ResponseCode { get; set; } = null;
		
		public Boolean ResponseCodeSpecified { get { return ResponseCode != null; } }

		/// <summary>
		/// Contract Document
		/// </summary>
		public String ContractDocument { get; set; } = null;

		public String ContractNumber { get; set; } = null;
		
		public DateTime? EffectiveDate { get; set; } = null;
		
		public Boolean EffectiveDateSpecified { get { return EffectiveDate != null; } }

		public Int32? EffectiveOdometer { get; set; } = null;
		
		public Boolean EffectiveOdometerSpecified { get { return EffectiveOdometer != null; } }

		public DateTime? ExpirationDate { get; set; } = null;
		
		public Boolean ExpirationDateSpecified { get { return ExpirationDate != null; } }

		public Int32? ExpirationOdometer { get; set; } = null;
		
		public Boolean ExpirationOdometerSpecified { get { return ExpirationOdometer != null; } }

		/// <summary>
		/// Value specified in the eContracting request.
		/// </summary>
		public Int16? AdditionalBaseWarrantyTerm { get; set; } = null;
		
		public Boolean AdditionalBaseWarrantyTermSpecified { get { return AdditionalBaseWarrantyTerm != null; } }

		/// <summary>
		/// Value specified in the eContracting request.
		/// </summary>
		public Int32? AdditionalBaseWarrantyMiles { get; set; } = null;
		
		public Boolean AdditionalBaseWarrantyMilesSpecified { get { return AdditionalBaseWarrantyMiles != null; } }

		/// <summary>
		/// Value specified in the eContracting request.
		/// </summary>
		public Int16? AdditionalPowertrainWarrantyTerm { get; set; } = null;
		
		public Boolean AdditionalPowertrainWarrantyTermSpecified { get { return AdditionalPowertrainWarrantyTerm != null; } }

		/// <summary>
		/// Value specified in the eContracting request.
		/// </summary>
		public Int32? AdditionalPowertrainWarrantyMiles { get; set; } = null;
		
		public Boolean AdditionalPowertrainWarrantyMilesSpecified { get { return AdditionalPowertrainWarrantyMiles != null; } }

		/// <summary>
		/// The sum of the configured Base Warranty (term) plus the Additional Base Warranty (term)
		/// </summary>
		public Int16? TotalBaseWarrantyTerm { get; set; } = null;
		
		public Boolean TotalBaseWarrantyTermSpecified { get { return TotalBaseWarrantyTerm != null; } }

		/// <summary>
		/// The sum of the configured Base Warranty (miles) plus the Additional Base Warranty (miles)
		/// </summary>
		public Int32? TotalBaseWarrantyMiles { get; set; } = null;
		
		public Boolean TotalBaseWarrantyMilesSpecified { get { return TotalBaseWarrantyMiles != null; } }

		/// <summary>
		/// The sum of the configured Powertrain Warranty (term) plus the Additional Powertrain Warranty (term)
		/// </summary>
		public Int16? TotalPowertrainWarrantyTerm { get; set; } = null;
		
		public Boolean TotalPowertrainWarrantyTermSpecified { get { return TotalPowertrainWarrantyTerm != null; } }

		/// <summary>
		/// The sum of the configured Powertrain Warranty (miles) plus the Additional Powertrain Warranty (miles)
		/// </summary>
		public Int32? TotalPowertrainWarrantyMiles { get; set; } = null;
		
		public Boolean TotalPowertrainWarrantyMilesSpecified { get { return TotalPowertrainWarrantyMiles != null; } }

		public Tax Tax { get; set; } = null;

		/// <summary>
		/// Collection of Messages.
		/// </summary>
		public List<Message> Messages { get; set; } = null;
		#endregion
	}
}
