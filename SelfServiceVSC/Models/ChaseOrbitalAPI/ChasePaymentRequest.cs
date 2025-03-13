namespace AAC.SelfServiceVSC.Models.ChaseOrbitalAPI
{
	public class ChasePaymentRequest
	{
		public decimal Amount { get; set; }
		public string CcAccountNum { get; set; }
		public string CcExp { get; set; }
		public string CcCardVerifyNum { get; set; }
}
}
