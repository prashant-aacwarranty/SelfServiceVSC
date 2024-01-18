using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public class Home
	{
		public String Address { get; set; }

		public String City { get; set; }

		public String State { get; set; }

		public String PostalCode { get; set; }
	}
}
