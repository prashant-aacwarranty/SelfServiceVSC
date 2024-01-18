using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class QuoteProtectionRevisePatch
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
			public String Type { get; set; } = "quote-protection";

			[JsonPropertyName("attributes")]
			public AttributesModel Attributes { get; set; } = null;
			#endregion

			public class AttributesModel
			{
				#region Properties
				[JsonPropertyName("price")]
				public Decimal? Price { get; set; } = null;
				#endregion
			}
		}
	}
}
