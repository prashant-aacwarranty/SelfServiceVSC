using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public class Authentication
	{
		public String PartnerCredentials { get; set; } = SelfServiceVSC.AppSettings.PaylinkPartnerCredentials;
	}
}
