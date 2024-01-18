namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Plan object for PlanRates object
	/// </summary>
	public class Plan
	{
		#region Properties
		/// <summary>
		/// Not used
		/// </summary>
		public String PlanDescriptions { get; set; } = null;

		/// <summary>
		/// Not used
		/// </summary>
		public String ProgramDescriptions { get; set; } = null;

		/// <summary>
		/// Product Type Codes
		/// Note: This is a general list of Product Type Codes.Providers are able to customize the Product Type Codes they use so other code not listed here may be returned.
		/// Valid values:
		///		VSC – Vehicle Service Contract;
		///		GAP – GAP;
		///		MNT – Maintenance;
		///		AP – Appearance Protection;
		///		PDR – Paintless Dent Repair;
		///		LTW – Limited Warranty;
		///		RHT – Tire & Wheel;
		///		THP – Theft Protection;
		///		EWT – Extended Wear & Tear;
		///		ETCH – Etch;
		///		KEY – Key Replacement;
		///		OTH – Other Aftermarket Product
		/// </summary>
		public String ProductTypeCode { get; set; } = null;

		/// <summary>
		/// Product Type Description
		/// </summary>
		public String ProductTypeDescription { get; set; } = null;

		/// <summary>
		/// The unique plan code
		/// </summary>
		public String PlanCode { get; set; } = null;

		/// <summary>
		/// Plan description
		/// </summary>
		public String PlanDescription { get; set; } = null;

		/// <summary>
		/// Contract type
		/// </summary>
		public Int64? ContractType { get; set; } = null;
		
		public Boolean ContractTypeSpecified { get { return ContractType != null; } }

		/// <summary>
		/// The unique identification number of plan
		/// </summary>
		public Int64? PlanId { get; set; } = null;
		
		public Boolean PlanIdSpecified { get { return PlanId != null; } }

		/// <summary>
		/// Rate book number
		/// </summary>
		public Int64? RateBook { get; set; } = null;
		
		public Boolean RateBookSpecified { get { return RateBook != null; } }

		/// <summary>
		/// Values will be returned as configured in SCS at plan level.
		/// Valid values:
		///		B- Balloon
		///		L- Lease
		///		F- Loan
		///		A – Any
		///		C- Cash
		/// </summary>
		public String OwnershipTypeCode { get; set; } = null;

		/// <summary>
		/// Contract Plan Name for plan mapping on contract PDF.
		/// </summary>
		public String ContractPlanName { get; set; } = null;

		/// <summary>
		/// Program Description
		/// </summary>
		public String ProgramDescription { get; set; } = null;

		/// <summary>
		/// Unique identification number of program
		/// </summary>
		public Int64? ProgramID { get; set; } = null;
		
		public Boolean ProgramIDSpecified { get { return ProgramID != null; } }

		/// <summary>
		/// If the Discountable flag is set to 1, the Retail Price (excluding taxes) for the submitted contract can be less than the Retail Price value sent in the rating response, but not greater.
		/// Valid values:
		///		1 - Regulated rate is discountable
		///		0 – Not discountable
		/// </summary>
		public Byte? Discountable { get; set; } = null;
		
		public Boolean DiscountableSpecified { get { return Discountable != null; } }
		#endregion
	}
}
