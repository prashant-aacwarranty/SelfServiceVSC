using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	/// <summary>
	/// This may not be usable with the returns we end up getting.
	/// </summary>
	public class RatesGetResponse : ErrorsResponse
	{
		#region Properties
		[JsonPropertyName("coverages")]
		public List<CoverageModel> Coverages { get; set; } = null;

		[JsonPropertyName("coverage_terms")]
		public CoverageTermsModel CoverageTerms { get; set; } = null;

		[JsonPropertyName("coverage_names")]
		public List<String> CoverageNames { get; set; } = null;
		#endregion

		public class CoverageModel
		{
			#region Properties
			[JsonPropertyName("id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? Id { get; set; } = null;

			[JsonPropertyName("pen_rate_id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int32? PenRateId { get; set; } = null;

			[JsonPropertyName("coverage_name")]
			public String CoverageName { get; set; } = null;

			[JsonPropertyName("form_id")]
			public String FormId { get; set; } = null;

			[JsonPropertyName("form_name")]
			public String FormName { get; set; } = null;

			[JsonPropertyName("new")]
			public Boolean? New { get; set; } = null;

			[JsonPropertyName("product_type")]
			public String ProductType { get; set; } = null;

			[JsonPropertyName("term_miles")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int32? TermMiles { get; set; } = null;

			[JsonPropertyName("term_months")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int32? TermMonths { get; set; } = null;

			[JsonPropertyName("label")]
			public String Label { get; set; } = null;

			[JsonPropertyName("deductibles")]
			public List<DeductibleModel> Deductibles { get; set; } = null;

			[JsonPropertyName("surcharges")]
			public List<SurchargeModel> Surcharges { get; set; } = null;
			#endregion

			public class DeductibleModel
			{
				#region Properties
				[JsonPropertyName("id")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int64? Id { get; set; } = null;

				[JsonPropertyName("pen_coverage_id")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? PenCoverageId { get; set; } = null;

				[JsonPropertyName("amount")]
				public String Amount { get; set; } = null;

				[JsonPropertyName("dealer_cost")]
				public String DealerCost { get; set; } = null;

				[JsonPropertyName("deductible_type")]
				public String DeductibleType { get; set; } = null;

				[JsonPropertyName("description")]
				public String Description { get; set; } = null;

				[JsonPropertyName("fi_markup")]
				public String FIMarkup { get; set; } = null;

				[JsonPropertyName("max_retail_price")]
				public String MaxRetailPrice { get; set; } = null;

				[JsonPropertyName("min_retail_price")]
				public String MinRetailPrice { get; set; } = null;

				[JsonPropertyName("rate_id")]
				public String RateId { get; set; } = null;

				[JsonPropertyName("reduced_amount")]
				public String ReducedAmount { get; set; } = null;

				[JsonPropertyName("retail_price")]
				public String RetailPrice { get; set; } = null;
				#endregion
			}

			public class SurchargeModel
			{
				#region Properties
				[JsonPropertyName("id")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int64? Id { get; set; } = null;

				[JsonPropertyName("pen_coverage_id")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? PenCoverageId { get; set; } = null;

				[JsonPropertyName("code")]
				public String Code { get; set; } = null;

				[JsonPropertyName("dealer_cost")]
				public String DealerCost { get; set; } = null;

				[JsonPropertyName("description")]
				public String Description { get; set; } = null;

				[JsonPropertyName("optional")]
				public Boolean? Optional { get; set; } = null;

				[JsonPropertyName("retail_price")]
				public String RetailPrice { get; set; } = null;
				#endregion
			}
		}

		public class CoverageTermsModel
		{
			#region Properties
			[JsonPropertyName("Powertrain Extended Warranty - Used")]
			public List<PowertrainExtendedWarrantyModel> PowertrainExtendedWarrantyUsed { get; set; } = null;

			[JsonPropertyName("Powertrain Extended Warranty - New")]
			public List<PowertrainExtendedWarrantyModel> PowertrainExtendedWarrantyNew { get; set; } = null;
			#endregion

			public class PowertrainExtendedWarrantyModel
			{
				#region Properties
				[JsonPropertyName("id")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int64? Id { set; get; } = null;

				[JsonPropertyName("term_label")]
				public String TermLabel { get; set; } = null;
				#endregion
			}
		}
	}
}
