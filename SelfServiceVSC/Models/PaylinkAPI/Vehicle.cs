using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public class Vehicle
	{
		public String Make { get; set; }

		public String Model { get; set; }

		public Int16? Year { get; set; }

		public Int32? Odometer { get; set; }

		public String VIN { get; set; }
	}
}
