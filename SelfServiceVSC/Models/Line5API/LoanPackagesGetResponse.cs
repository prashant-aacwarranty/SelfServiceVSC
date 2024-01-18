using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class LoanPackagesGetResponse : ErrorsResponse
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
			public String Type { get; set; } = "loan-packages";

			[JsonPropertyName("attributes")]
			public AttributesModel Attributes { get; set; } = null;

			[JsonPropertyName("links")]
			public LinksModel Links { get; set; } = null;
			#endregion

			public class AttributesModel
			{
				#region Properties
				[JsonPropertyName("documents-file-name")]
				public String DocumentsFileName { get; set; } = null;

				[JsonPropertyName("documents-content-type")]
				public String DocumentsContentType { get; set; } = null;

				[JsonPropertyName("documents-file-size")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? DocumentsFileSize { get; set; } = null;
				#endregion
			}

			public class LinksModel
			{
				#region Properties
				[JsonPropertyName("download")]
				public String Download { get; set; } = null;
				#endregion
			}
		}
	}
}
