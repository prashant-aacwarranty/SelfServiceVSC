using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public class Coverage
	{
		public Int32? Mileage { get; set; }

		public Int16 Term { get; set; }

		[JsonPropertyName("Type")]
		public CoverageType CoverageType { get; set; }

		public String ProgramName { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String PlanName { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? LimitOfLiability { get; set; }
	}
}
