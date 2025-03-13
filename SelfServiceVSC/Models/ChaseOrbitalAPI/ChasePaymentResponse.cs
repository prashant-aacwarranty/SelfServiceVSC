using SelfServiceVSC.Controllers;

namespace AAC.SelfServiceVSC.Models.ChaseOrbitalAPI
{
	public class ChasePaymentResponse
	{
		public string TransType { get; set; }

		public Merchant Merchant { get; set; }

		public PaymentInstrument PaymentInstrument { get; set; }

		public Order Order { get; set; }

		public AvsBilling AvsBilling { get; set; }

		public CardholderVerification CardholderVerification { get; set; }
	}
}
