using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public class Borrower
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String PurchaserNumber { get; set; }

		public String FullName { get; set; }

		public String Address1 { get; set; }

		public String Address2 { get; set; }

		public String City { get; set; }

		public String State { get; set; }

		public String ZipCode { get; set; }

		public String EmailAddress { get; set; }

		public String PhoneNumber { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String CellPhoneNumber { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Country { get; set; }
	}
}
