using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class QuoteProtectionPatchResponse : ErrorsResponse
	{
		#region Properties
		[JsonPropertyName("data")]
		public DataModel Data { get; set; } = null;

		[JsonPropertyName("included")]
		public List<IncludedModel> Included { get; set; } = null;
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
				[JsonPropertyName("link-to-quote-wizard")]
				public String LinkToQuoteWizard { get; set; } = null;

				[JsonPropertyName("integration-loan-id")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? IntegrationLoanId { get; set; } = null;

				[JsonPropertyName("status")]
				public String Status { get; set; } = null;

				[JsonPropertyName("term")]
				public Decimal? Term { get; set; } = null;

				[JsonPropertyName("monthly-payment")]
				public Decimal? MonthlyPayment { get; set; } = null;

				[JsonPropertyName("protections-total")]
				public Decimal? ProtectionsTotal { get; set; } = null;

				[JsonPropertyName("taxed-total")]
				public Decimal? TaxedTotal { get; set; } = null;

				[JsonPropertyName("tax")]
				public Decimal? Tax { get; set; } = null;

				[JsonPropertyName("subtotal")]
				public Decimal? Subtotal { get; set; } = null;

				[JsonPropertyName("down-payment")]
				public Decimal? DownPayment { get; set; } = null;

				[JsonPropertyName("total")]
				public Decimal? Total { get; set; } = null;

				[JsonPropertyName("doc-stamp-fee")]
				public Decimal? DocStampFee { get; set; } = null;

				[JsonPropertyName("financed-total")]
				public Decimal? FinancedTotal { get; set; } = null;

				[JsonPropertyName("base-fee")]
				public Decimal? BaseFee { get; set; } = null;

				[JsonPropertyName("buy-down-fee")]
				public Decimal? BuyDownFee { get; set; } = null;

				[JsonPropertyName("interest-percent")]
				public Decimal? InterestPercent { get; set; } = null;

				[JsonPropertyName("additional-term-fee")]
				public Decimal? AdditionalTermFee { get; set; } = null;

				[JsonPropertyName("fees")]
				public Decimal? Fees { get; set; } = null;
				#endregion
			}

			public class RelationshipsModel
			{
				#region Properties
				[JsonPropertyName("quote-protections")]
				public QuoteProtectionsModel QuoteProtections { get; set; } = null;

				[JsonPropertyName("customer")]
				public CustomerModel Customer { get; set; } = null;

				[JsonPropertyName("vehicle")]
				public VehicleModel Vehicle { get; set; } = null;
				#endregion

				public class QuoteProtectionsModel
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
						public String Type { get; set; } = "quote-protections";
						#endregion
					}
				}

				public class CustomerModel
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
						public String Type { get; set; } = "customers";
						#endregion
					}
				}

				public class VehicleModel
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
						public String Type { get; set; } = "vehicles";
						#endregion
					}
				}
			}
		}

		public class IncludedModel
		{
			#region Properties
			[JsonPropertyName("id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? Id { get; set; } = null;

			[JsonPropertyName("type")]
			public String Type { get; set; } = null;

			[JsonPropertyName("attributes")]
			public AttributesModel Attributes { get; set; } = null;

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

			public class AttributesModel
			{
				#region Properties
				[JsonPropertyName("first-name")]
				public String FirstName { get; set; } = null;

				[JsonPropertyName("last-name")]
				public String LastName { get; set; } = null;

				[JsonPropertyName("date-of-birth")]
				public String DateOfBirth { get; set; } = null;

				[JsonPropertyName("address-1")]
				public String Address1 { get; set; } = null;

				[JsonPropertyName("address-2")]
				public String Address2 { get; set; } = null;

				[JsonPropertyName("city")]
				public String City { get; set; } = null;

				[JsonPropertyName("state")]
				public String State { get; set; } = null;

				[JsonPropertyName("postal-code")]
				public String PostalCode { get; set; } = null;

				[JsonPropertyName("phone-number")]
				public String PhoneNumber { get; set; } = null;

				[JsonPropertyName("cell-number")]
				public String CellNumber { get; set; } = null;

				[JsonPropertyName("work-number")]
				public String WorkNumber { get; set; } = null;

				[JsonPropertyName("credit-score-tier")]
				public String CreditScoreTier { get; set; } = null;

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

				[JsonPropertyName("year")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Year { get; set; } = null;

				[JsonPropertyName("make")]
				public String Make { get; set; } = null;

				[JsonPropertyName("model")]
				public String Model { get; set; } = null;

				[JsonPropertyName("vin")]
				public String Vin { get; set; } = null;

				[JsonPropertyName("purchase-price")]
				public String PurchasePrice { get; set; } = null;

				[JsonPropertyName("financed-amount")]
				public String FinancedAmount { get; set; } = null;

				[JsonPropertyName("msrp")]
				public String Msrp { get; set; } = null;

				[JsonPropertyName("finance-term")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? FinanceTerm { get; set; } = null;

				[JsonPropertyName("in-service-date")]
				public String InServiceDate { get; set; } = null;

				[JsonPropertyName("status")]
				public String Status { get; set; } = null;
				#endregion
			}

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
