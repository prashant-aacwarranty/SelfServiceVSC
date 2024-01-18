using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class QuoteProtectionsGetResponse : ErrorsResponse
	{
		#region Properties
		[JsonPropertyName("quote_protections")]
		public List<QuoteProtectionModel> QuoteProtections { get; set; } = null;
		#endregion

		public class QuoteProtectionModel
		{
			#region Properties
			[JsonPropertyName("id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? Id { get; set; } = null;

			[JsonPropertyName("term")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int32? Term { get; set; } = null;

			[JsonPropertyName("name")]
			public String Name { get; set; } = null;

			[JsonPropertyName("months")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int32? Months { get; set; } = null;

			[JsonPropertyName("mileage")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int32? Mileage { get; set; } = null;

			[JsonPropertyName("price")]
			public String Price { get; set; } = null;

			[JsonPropertyName("financing_limit")]
			public String FinancingLimit { get; set; } = null;

			[JsonPropertyName("exclude_tax")]
			public Boolean? ExcludeTax { get; set; } = null;

			[JsonPropertyName("protection_label_id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? ProtectionLabelId { get; set; } = null;

			[JsonPropertyName("coverage_id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? CoverageId { get; set; } = null;

			[JsonPropertyName("deductible_id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? DeductibleId { get; set; } = null;

			[JsonPropertyName("surcharge_ids")]
			public List<SurchargeIdModel> SurchargeIds { get; set; } = null;

			[JsonPropertyName("template_fields")]
			public List<TemplateFieldModel> TemplateFields { get; set; } = null;
			#endregion

			public class SurchargeIdModel
			{
				#region Properties

				#endregion
			}

			public class TemplateFieldModel
			{
				#region Properties
				[JsonPropertyName("name")]
				public String Name { get; set; } = null;

				[JsonPropertyName("prompt")]
				public String Prompt { get; set; } = null;

				[JsonPropertyName("data_type")]
				public String DataType { get; set; } = null;

				[JsonPropertyName("validator_prompt")]
				public String ValidatorPrompt { get; set; } = null;

				[JsonPropertyName("validator_regex")]
				public String ValidatorRegex { get; set; } = null;

				[JsonPropertyName("value")]
				public String Value { get; set; } = null;
				#endregion
			}
		}
	}
}
