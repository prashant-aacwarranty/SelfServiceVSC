using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;
using SelfServiceVSC.Controllers;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public class SubmitContractResponse
	{
		public String ServiceContractNumber { get; set; }

		public Byte ResponseCode { get; set; }

		public String ResponseMessage { get; set; }

		public IEnumerable<String> ValidationErrors { get; set; }

		public IEnumerable<String> SystemErrors { get; set; }
	}
}
