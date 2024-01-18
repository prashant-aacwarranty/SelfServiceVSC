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
	public class SellerAdminRelationshipResponse
	{
		List<SellerAdminRelationship> SellerAdminRelationships { get; set; } = null;

		public Byte ResponseCode { get; set; }

		public String ResponseMessage { get; set; }

		public IEnumerable<String> ValidationErrors { get; set; }

		public IEnumerable<String> SystemErrors { get; set; }

		public class SellerAdminRelationship
		{
			public String AdminCode { get; set; }

			public String AdminName { get; set; }

			public String SellerCode { get; set; }

			public String SellerName { get; set; }
		}
	}
}
