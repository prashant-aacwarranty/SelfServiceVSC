using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class ErrorsResponse
	{
		#region Properties
		[JsonPropertyName("errors")]
		public List<ErrorModel> Errors { get; set; } = null;
		#endregion

		public class ErrorModel
		{
			#region Properties
			[JsonPropertyName("source")]
			public SourceModel Source { get; set; } = null;

			[JsonPropertyName("detail")]
			public String Detail { get; set; } = null;
			#endregion

			public class SourceModel
			{
				#region Properties
				[JsonPropertyName("pointer")]
				public String Pointer { get; set; } = null;
				#endregion
			}
		}
	}
}
