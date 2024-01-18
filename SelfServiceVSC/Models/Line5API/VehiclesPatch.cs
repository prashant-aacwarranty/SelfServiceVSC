using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class VehiclesPatch
	{
		#region Properties
		[JsonPropertyName("data")]
		public DataModel Data { get; set; } = null;
		#endregion

		public class DataModel
		{
			#region Properties
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
				[JsonPropertyName("vin")]
				public String VIN { get; set; } = null;

				[JsonPropertyName("mileage")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Mileage { get; set; } = null;

				[JsonPropertyName("purchase-price")]
				public Decimal? PurchasePrice { get; set; } = null;

				[JsonPropertyName("msrp")]
				public Decimal? MSRP { get; set; } = null;

				[JsonPropertyName("financed-amount")]
				public Decimal? FinancedAmount { get; set; } = null;

				[JsonPropertyName("finance-term")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? FinanceTerm { get; set; } = null;
				#endregion
			}

			public class RelationshipsModel
			{
				#region Properties
				[JsonPropertyName("quote")]
				public QuoteModel Quote { get; set; } = null;
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
						public String Type { get; set; } = "quotes";
						#endregion
					}
				}
			}
		}
	}
}
