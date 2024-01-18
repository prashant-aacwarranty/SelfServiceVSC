using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class Field
	{
		#region Properties
		[JsonPropertyName("name")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Name { get; set; } = null;

		[JsonPropertyName("type")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Type { get; set; } = null;

		[JsonPropertyName("required")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Required { get; set; } = "true";
		#endregion
	}
}
