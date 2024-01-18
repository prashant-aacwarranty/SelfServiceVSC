using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public enum AccountType : UInt32
	{
		SAVINGS = 1 << 0,
		CHECKING = 1 << 1
	}
}
