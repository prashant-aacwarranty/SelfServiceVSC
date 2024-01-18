using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class RatesGet
	{
		#region Properties
		[JsonPropertyName("data")]
		public DataModel Data { get; set; } = null;
		#endregion

		public class DataModel
		{
			#region Properties
			[JsonPropertyName("id")]
			public String Id { get; set; } = null;

			[JsonPropertyName("type")]
			public String Type { get; set; } = "quotes";

			[JsonPropertyName("relationships")]
			public RelationshipsModel Relationships { get; set; } = null;
			#endregion

			public class RelationshipsModel
			{
				#region Properties
				[JsonPropertyName("protection-label")]
				public ProtectionLabelModel ProtectionLabel { get; set; } = null;
				#endregion

				public class ProtectionLabelModel
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
						public String Type { get; set; } = "quotes";
						#endregion
					}
				}
			}
		}
	}
}
