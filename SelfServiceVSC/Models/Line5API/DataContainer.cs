using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class DataContainer
	{
		#region Properties
		[JsonPropertyName("data")]
		public Data Data { get; set; } = null;
		#endregion
	}
}
