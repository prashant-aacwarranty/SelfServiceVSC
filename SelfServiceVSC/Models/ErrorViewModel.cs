namespace AAC.SelfServiceVSC.Models
{
	public class ErrorViewModel
	{
		public String RequestId { get; set; }

		public bool ShowRequestId => !String.IsNullOrEmpty(RequestId);
	}
}
