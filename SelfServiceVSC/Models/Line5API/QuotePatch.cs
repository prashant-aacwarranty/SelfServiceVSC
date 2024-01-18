using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class QuotePatch
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
			#endregion

			public class AttributesModel
			{
				#region Properties
				[JsonPropertyName("term")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public Int32? Term { get; set; } = null;

				[JsonPropertyName("down_payment")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public Decimal? DownPayment { get; set; } = null;

				[JsonPropertyName("interest_percent")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public Decimal? InterestPercent { get; set; } = null;
				#endregion
			}
		}
	}
}
