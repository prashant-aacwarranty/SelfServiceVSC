using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AAC.SelfServiceVSC.Models.Form
{
	public class QuoteRequest
	{
		[JsonPropertyName("fname")]
		public String FirstName { get; set; } = null;

		[JsonPropertyName("lname")]
		public String LastName { get; set; } = null;

		[JsonIgnore]
		public String FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}

		[JsonPropertyName("ssn")]
		public String SSN { get; set; } = null;

		[JsonPropertyName("dob")]
		public String DOB { get; set; } = null;

		[JsonPropertyName("email")]
		public String Email { get; set; } = null;

		[JsonPropertyName("phone")]
		public String Phone { get; set; } = null;

		[JsonIgnore]
		public String PhoneNumber
		{
			get
			{
				return Regex.Replace(Phone, @"\D", "");
			}
		}

		[JsonPropertyName("address1")]
		public String Address1 { get; set; } = null;

		[JsonPropertyName("address2")]
		public String Address2 { get; set; } = null;

		[JsonPropertyName("city")]
		public String City { get; set; } = null;

		[JsonPropertyName("state")]
		public String State { get; set; } = null;

		[JsonPropertyName("zip")]
		public String Zip { get; set; } = null;
	}
}
