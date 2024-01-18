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
	public class SellerAdminRelationship
	{
		public Authentication Authentication { get; set; } = new Authentication();
	}
}
