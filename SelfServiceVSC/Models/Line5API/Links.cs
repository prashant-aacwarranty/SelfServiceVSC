using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class Links
	{
		#region Properties
		[JsonPropertyName("download")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Download { get; set; } = null;
		#endregion
	}
}
