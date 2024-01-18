using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class FilledField
	{
		#region Properties
		[JsonPropertyName("name")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Name { get; set; } = null;

		[JsonPropertyName("value")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? Value { get; set; } = null;
		#endregion
	}
}
