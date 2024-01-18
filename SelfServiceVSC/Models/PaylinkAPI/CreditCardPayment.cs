using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public class CreditCardPayment
	{
		public String CreditCardNumber { get; set; }

		[JsonPropertyName("ExpirationMonth")]
		public String ExpirationMonthString
		{
			get
			{
				return ("0" + ExpirationMonth.ToString()).Right(2);
			}
		}

		[JsonIgnore]
		public Byte ExpirationMonth { get; set; }

		[JsonPropertyName("ExpirationYear")]
		public String ExpirationYearString
		{
			get
			{
				return ("000" + ExpirationYear.ToString()).Right(4);
			}
		}

		[JsonIgnore]
		public UInt16 ExpirationYear { get; set; }

		public String NameOnCard { get; set; }
	}
}
