using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public enum BillingMethod : UInt32
	{
		BILL = 1 << 0,
		CREDIT = 1 << 1,
		ACH = 1 << 2
	}
}
