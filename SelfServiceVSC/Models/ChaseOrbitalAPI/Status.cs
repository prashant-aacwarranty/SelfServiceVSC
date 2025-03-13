using Newtonsoft.Json;

namespace AAC.SelfServiceVSC.Models.ChaseOrbitalAPI
{
	public class Status
	{

		[JsonProperty("procStatus")]
		public string ProcStatus { get; set; }

		[JsonProperty("procStatusMessage")]
		public string ProcStatusMessage { get; set; }

		[JsonProperty("hostRespCode")]
		public string HostRespCode { get; set; }

		[JsonProperty("respCode")]
		public string RespCode { get; set; }

		[JsonProperty("approvalStatus")]
		public string ApprovalStatus { get; set; }

		[JsonProperty("authorizationCode")]
		public string AuthorizationCode { get; set; }

		[JsonProperty("pymtBrandAuthResponseCode")]
		public string PymtBrandAuthResponseCode { get; set; }

		[JsonProperty("pymtBrandResponseCodeCategory")]
		public string PymtBrandResponseCodeCategory { get; set; }
	}
}
