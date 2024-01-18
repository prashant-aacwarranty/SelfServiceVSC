using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public enum DownpaymentMethod : UInt32
	{
		CREDIT = 1 << 0,
		ACH = 1 << 1
	}
}
