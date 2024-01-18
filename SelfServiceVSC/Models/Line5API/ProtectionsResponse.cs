using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class ProtectionsResponse : ErrorsResponse
	{
		#region Properties
		[JsonPropertyName("data")]
		public List<Protection> Protections { get; set; } = null;
		#endregion

		public class Protection
		{
			#region Properties
			[JsonPropertyName("id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? Id { get; set; } = null;

			[JsonPropertyName("type")]
			public String Type { get; set; } = null;

			[JsonPropertyName("attributes")]
			public ProtectionAttributes Attributes { get; set; } = null;
			#endregion

			public class ProtectionAttributes
			{
				#region Properties
				[JsonPropertyName("provider")]
				public String Provider { get; set; } = null;

				[JsonPropertyName("name")]
				public String Name { get; set; } = null;

				[JsonPropertyName("form_number")]
				public String FormNumber { get; set; } = null;

				[JsonPropertyName("attributes")]
				public List<ProtectionAttributesField> Fields { get; set; } = null;

				[JsonPropertyName("protection-category-id")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? ProtectionCategoryId { get; set; } = null;

				[JsonPropertyName("protection-category-name")]
				public String ProtectionCategoryName { get; set; } = null;

				[JsonPropertyName("brochure")]
				public String Brochure { get; set; } = null;
				#endregion

				public class ProtectionAttributesField
				{
					#region Properties
					[JsonPropertyName("name")]
					public String Name { get; set; } = null;

					[JsonPropertyName("type")]
					public String Type { get; set; } = null;

					[JsonPropertyName("required")]
					public String Required { get; set; } = null;
					#endregion
				}
			}
		}
	}
}
