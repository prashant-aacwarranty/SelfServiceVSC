using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public class Agent
	{
		public String SellerCode { get; set; }

		public String AdminCode { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String DealerNumber { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String SalesPersonName { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String SalesPersonEmail { get; set; }

		public Decimal DealerCost { get; set; }
	}
}
