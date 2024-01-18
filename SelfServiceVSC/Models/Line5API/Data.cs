using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class Data
	{
		#region Properties
		[JsonPropertyName("id")]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public Int64? Id { get; set; } = null;

		[JsonPropertyName("type")]
		public String Type { get; set; } = String.Empty;

		[JsonPropertyName("attributes")]
		public Attributes Attributes { get; set; } = null;

		[JsonPropertyName("relationships")]
		public Relationships Relationships { get; set; } = null;

		[JsonPropertyName("links")]
		public Links Links { get; set; } = null;
		#endregion
	}
}
