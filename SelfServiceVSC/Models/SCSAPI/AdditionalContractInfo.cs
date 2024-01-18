namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// AdditionalContractInfo object for PlanRate and GenerateContract objects
	/// </summary>
	public class AdditionalContractInfo
	{
		#region Properties
		/// <summary>
		/// If there are multiple additional information fields returned, FieldOrder will indicate the order the fields should be displayed on the screen. (Note: At this time there is only one additional field that can be configured so the value will always be ‘1’.)
		/// </summary>
		public Byte? FieldOrder { get; set; } = null;
		
		public Boolean FieldOrderSpecified { get { return FieldOrder != null; } }

		/// <summary>
		/// The field label to display for additional information field.
		/// </summary>
		public String FieldLabel { get; set; } = null;

		/// <summary>
		/// Data collected for additional contract info. Only used in GenerateContract requests.
		/// </summary>
		public String FieldValue { get; set; } = null;

		/// <summary>
		/// The type of data to be collected in the additional field. (Note: Only ‘alphanumeric’ will be returned at this time.)
		/// Valid Values:
		///		Alphanumeric
		///		Numeric(not currently used)
		///		Date(not currently used)
		///		Currency(not currently used)
		/// </summary>
		public String FieldType { get; set; } = null;

		/// <summary>
		/// Maximum length for data collected in additional info field. Length = 0 means that there is no required length. When the length is 0, the only limit is the max length of the field which is 20 characters. If the length is something other than 0, the data collected must length specified to be valid.
		/// </summary>
		public Int32? Length { get; set; } = null;
		
		public Boolean LengthSpecified { get { return Length != null; } }

		/// <summary>
		/// If Required = true, the additional information must be included in the contract when contract request is submitted. If additional information is required and not submitted in contract request, an error will be returned and contract will not be submitted. If Required = false, the additional information should be submitted in the contract request but error will not be returned if the additional information is not included in the contract request.
		/// </summary>
		public Boolean? Required { get; set; } = null;
		
		public Boolean RequiredSpecified { get { return Required != null; } }

		/// <summary>
		/// Additional information must be a unique value (true) or not required to be unique (false). If Unique = true, validation will occur when contract is submitted and an error will be returned if value is not unique.
		/// </summary>
		public Boolean? Unique { get; set; } = null;
		
		public Boolean UniqueSpecified { get { return Unique != null; } }
		#endregion
	}
}
