using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// This schema is used by SCS to return the collection of all products along with all rates for each
	/// product based on dealer’s sale authority and the vehicle information for a given Provider. It also
	/// allows the rating engine to optionally return error responses.
	/// </summary>
	public class GetRatesResponse
	{
		#region Properties
		/// <summary>
		/// Collection (array1..N) of PlanRate.
		/// This category contains the hierarchical response data which is subdivided by Plan, Term/Mileage, Deductible and Rate
		/// </summary>
		public List<PlanRate> PlanRates { get; set; } = null;

		/// <summary>
		/// Object contains ErrorResponse definition.
		/// </summary>
		public ErrorResponse ErrorResponse { get; set; } = null;

		/// <summary>
		/// This information should be ignored. The dealer information is not correct.
		/// </summary>
		public ContractKeyData ContractKeyData { get; set; } = null;

		/// <summary>
		/// Collection of ManufactureWarranty.
		/// This object provides Manufacturer Warranty information (base, powertrain and emissions warranty term/mileage)
		/// that has been configured for that vehicle by the provider (SCS Auto customer) whose rates are being requested.
		/// If the provider has not configured manufacturer warranty data for this vehicle, this information will not be returned.
		/// </summary>
		public List<ManufactureWarranty> ManufactureWarraties { get; set; } = null;

		/// <summary>
		/// Unique identifier that identifies the entire collection of rates returned within a single rate response.
		/// </summary>
		public String QuoteID { get; set; } = null;

		/// <summary>
		/// Date when the quote expires. After this date, a contract cannot be created based on the rates issued for this Quote ID.
		/// </summary>
		public DateTime? QuoteExpiration { get; set; } = null;

		public Boolean QuoteExpirationSpecified { get { return QuoteExpiration != null; } }

		/// <summary>
		/// Value specified in the eRating request.
		/// </summary>
		public Int16? AdditionalBaseWarrantyTerm { get; set; } = null;

		public Boolean AdditionalBaseWarrantyTermSpecified { get { return AdditionalBaseWarrantyTerm != null; } }

		/// <summary>
		/// Value specified in the eRating request.
		/// </summary>
		public Int32? AdditionalBaseWarrantyMiles { get; set; } = null;

		public Boolean AdditionalBaseWarrantyMilesSpecified { get { return AdditionalBaseWarrantyMiles != null; } }

		/// <summary>
		/// Value specified in the eRating request.
		/// </summary>
		public Int16? AdditionalPowertrainWarrantyTerm { get; set; } = null;

		public Boolean AdditionalPowertrainWarrantyTermSpecified { get { return AdditionalPowertrainWarrantyTerm != null; } }

		/// <summary>
		/// Value specified in the eRating request.
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

		public IEnumerable<RateClassMoney> GetMainRates(PlanRate planRate)
		{
			return planRate.RateClassMoneys.Where(rateClassMoney => rateClassMoney.Deductible.DeductAmt == 200m || rateClassMoney.Deductible.DeductAmt == 100m || rateClassMoney.Deductible.DeductAmt == 0m);
		}

		[XmlIgnore, JsonIgnore]
		public List<SessionEstimate> ClientQuotes
		{
			get
			{
				return PlanRates
					.SelectMany(planRate => GetMainRates(planRate).Select(rateClassMoney => new SessionEstimate(planRate.Plan, rateClassMoney)))
					.ToList();
			}
		}

		[XmlIgnore, JsonIgnore]
		public List<SessionDetails.Rate> Rates
		{
			get
			{
				return PlanRates
					.SelectMany(planRate => GetMainRates(planRate).Select(rateClassMoney => new SessionDetails.Rate { PlanRate = planRate, RateClassMoney = rateClassMoney, Selected = false}))
					.ToList();
			}
		}
		#endregion
	}
}
