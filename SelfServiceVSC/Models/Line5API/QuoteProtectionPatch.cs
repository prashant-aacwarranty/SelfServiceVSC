using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class QuoteProtectionPatch
	{
		#region Properties
		[JsonPropertyName("data")]
		public DataModel Data { get; set; } = null;
		#endregion

		public class DataModel
		{
			#region Properties
			[JsonPropertyName("id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? Id { get; set; } = null;

			[JsonPropertyName("type")]
			public String Type { get; set; } = "quotes";

			[JsonPropertyName("attributes")]
			public AttributesModel Attributes { get; set; } = null;

			[JsonPropertyName("relationships")]
			public RelationshipsModel Relationships { get; set; } = null;
			#endregion

			public class AttributesModel
			{
				#region Properties
				[JsonPropertyName("price")]
				public Decimal? Price { get; set; } = null;

				[JsonPropertyName("exclude-tax")]
				public Boolean? ExcludeTax { get; set; } = null;

				[JsonPropertyName("months")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Months { get; set; } = null;

				[JsonPropertyName("mileage")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Mileage { get; set; } = null;
				#endregion
			}

			public class RelationshipsModel
			{
				#region Properties
				[JsonPropertyName("quote")]
				public QuoteModel Quote { get; set; } = null;

				[JsonPropertyName("protection-label")]
				public ProtectionLabelModel ProtectionLabel { get; set; } = null;

				[JsonPropertyName("coverage")]
				public CoverageModel Coverage { get; set; } = null;

				[JsonPropertyName("deductible")]
				public DeductibleModel Deductible { get; set; } = null;

				[JsonPropertyName("surcharges")]
				public SurchargesModel Surcharges { get; set; } = null;

				[JsonPropertyName("template")]
				public TemplateModel Template { get; set; } = null;
				#endregion

				public class QuoteModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public DataModel Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties
						[JsonPropertyName("id")]
						[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
						public Int64? Id { get; set; } = null;

						[JsonPropertyName("type")]
						public String Type { get; set; } = "quote";
						#endregion
					}
				}

				public class ProtectionLabelModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public DataModel Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties
						[JsonPropertyName("id")]
						[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
						public Int64? Id { get; set; } = null;

						[JsonPropertyName("type")]
						public String Type { get; set; } = "protection-label";
						#endregion
					}
				}

				public class CoverageModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public DataModel Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties
						[JsonPropertyName("id")]
						[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
						public Int64? Id { get; set; } = null;

						[JsonPropertyName("type")]
						public String Type { get; set; } = "coverage";
						#endregion
					}
				}

				public class DeductibleModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public DataModel Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties
						[JsonPropertyName("id")]
						[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
						public Int64? Id { get; set; } = null;

						[JsonPropertyName("type")]
						public String Type { get; set; } = "deductible";
						#endregion
					}
				}

				public class SurchargesModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public List<DataModel> Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties
						[JsonPropertyName("id")]
						[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
						public Int64? Id { get; set; } = null;

						[JsonPropertyName("type")]
						public String Type { get; set; } = "surcharge";
						#endregion
					}
				}

				public class TemplateModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public DataModel Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties
						[JsonPropertyName("type")]
						public String Type { get; set; } = "template";

						[JsonPropertyName("attributes")]
						public AttributesModel Attributes { get; set; } = null;
						#endregion

						public class AttributesModel
						{
							#region Properties
							[JsonPropertyName("deal_monthlypayment")]
							public String DealMonthlyPayment { get; set; } = null;
							#endregion
						}
					}
				}
			}
		}
	}
}
