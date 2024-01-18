using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public class ACHPayment
	{
		public String RoutingNumber { get; set; }

		public String AccountNumber { get; set; }

		public AccountType AccountType { get; set; }

		public String BankName { get; set; }
	}
}
