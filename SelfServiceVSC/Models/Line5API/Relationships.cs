using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class Relationships
	{
		#region Properties
		[JsonPropertyName("customer")]
		public DataContainer Customer { get; set; } = null;

		[JsonPropertyName("vehicle")]
		public DataContainer Vehicle { get; set; } = null;

		[JsonPropertyName("quote-protections")]
		public DataContainer QuoteProtections { get; set; } = null;
		#endregion
	}
}
