using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Form
{
	// todo: clean this up


	public class PaymentRequest
	{
		[JsonPropertyName("payment_card_name")]
		public String PaymentCardName { get; set; } = null;

		[JsonPropertyName("payment_card_number")]
		public String PaymentCardNumber { get; set; } = null;

		[JsonPropertyName("payment_card_cvv")]
		public String PaymentCardCVV { get; set; } = null;

		[JsonPropertyName("payment_card_expiration")]
		public String PaymentCardExpiration { get; set; } = null;

		[JsonPropertyName("payment_card_zip_code")]
		public String PaymentCardZipCode { get; set; } = null;

		[JsonPropertyName("bank_transfer_name")]
		public String BankTransferName { get; set; } = null;

		[JsonPropertyName("bank_transfer_account_type")]
		public String BankTransferAccountType { get; set; } = null;

		[JsonPropertyName("bank_transfer_routing_number")]
		public String BankTransferRoutingNumber { get; set; } = null;

		[JsonPropertyName("bank_transfer_account_number")]
		public String BankTransferAccountNumber { get; set; } = null;

		[JsonPropertyName("bank_transfer_routing_number_confirmation")]
		public String BankTransferRoutingNumberConfirmation { get; set; } = null;

		[JsonPropertyName("bank_transfer_account_number_confirmation")]
		public String BankTransferAccountNumberConfirmation { get; set; } = null;
	}
}
