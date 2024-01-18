using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class UsersGetResponse : ErrorsResponse
	{
		#region Properties
		[JsonPropertyName("data")]
		public List<User> Users { get; set; } = null;
		#endregion

		public class User
		{
			#region Properties
			[JsonPropertyName("id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? Id { get; set; } = null;

			[JsonPropertyName("type")]
			public String Type { get; set; } = null;

			[JsonPropertyName("attributes")]
			public UserAttributes Attributes { get; set; } = null;
			#endregion

			public class UserAttributes
			{
				#region Properties
				[JsonPropertyName("email")]
				public String Email { get; set; } = null;

				[JsonPropertyName("first-name")]
				public String FirstName { get; set; } = null;

				[JsonPropertyName("last-name")]
				public String LastName { get; set; } = null;
				#endregion
			}
		}
	}
}
