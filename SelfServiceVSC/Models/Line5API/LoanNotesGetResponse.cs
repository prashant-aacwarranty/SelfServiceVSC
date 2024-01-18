using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class LoanNotesGetResponse : ErrorsResponse
	{
		#region Properties
		[JsonPropertyName("data")]
		public List<DataModel> Data { get; set; } = null;
		#endregion

		public class DataModel
		{
			#region Properties
			[JsonPropertyName("id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? Id { get; set; } = null;

			[JsonPropertyName("type")]
			public String Type { get; set; } = "loan-notes";

			[JsonPropertyName("attributes")]
			public AttributesModel Attributes { get; set; } = null;
			#endregion

			public class AttributesModel
			{
				#region Properties
				[JsonPropertyName("body")]
				public String Body { get; set; } = null;

				[JsonPropertyName("created-at")]
				public DateTime? CreatedAt { get; set; } = null;

				[JsonPropertyName("user-name")]
				public String UserName { get; set; } = null;
				#endregion
			}
		}
	}
}
