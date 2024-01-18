using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class QuoteFinalizePatch
	{
		#region Properties
		[JsonPropertyName("data")]
		public DataModel Data { get; set; } = null;
		#endregion

		public class DataModel
		{
			#region Properties
			[JsonPropertyName("id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? Id { get; set; } = null;

			[JsonPropertyName("type")]
			public String Type { get; set; } = "quotes";

			[JsonPropertyName("attributes")]
			public AttributesModel Attributes { get; set; } = null;

			[JsonPropertyName("relationships")]
			public RelationshipsModel Relationships { get; set; } = null;
			#endregion

			public class AttributesModel
			{
				#region Properties
				[JsonPropertyName("first_payment_date")]
				public String FirstPaymentDate { get; set; } = null;
				#endregion
			}

			public class RelationshipsModel
			{
				#region Properties
				[JsonPropertyName("downpayment_payment_method")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public PaymentMethodModel DownpaymentPaymentMethod { get; set; } = null;

				[JsonPropertyName("payment_method")]
				public PaymentMethodModel PaymentMethod { get; set; } = null;
				#endregion

				public class PaymentMethodModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public DataModel Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties
						[JsonPropertyName("type")]
						public String Type { get; set; } = "payment_methods";

						[JsonPropertyName("attributes")]
						public AttributesModel Attributes { get; set; } = null;
						#endregion

						public class AttributesModel
						{
							#region Properties
							[JsonPropertyName("form_of_payment_type")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String FormOfPaymentType { get; set; } = "payment_card";

							[JsonPropertyName("payment_card_name")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String PaymentCardName { get; set; } = null;

							[JsonPropertyName("payment_card_number")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String PaymentCardNumber { get; set; } = null;

							[JsonPropertyName("payment_card_cvv")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String PaymentCardCVV { get; set; } = null;

							[JsonPropertyName("payment_card_expiration")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String PaymentCardExpiration { get; set; } = null;

							[JsonPropertyName("payment_card_zip_code")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String PaymentCardZipCode { get; set; } = null;


							[JsonPropertyName("bank_transfer_routing_number")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String BankTransferRoutingNumber { get; set; } = null;

							[JsonPropertyName("bank_transfer_account_number")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String BankTransferAccountNumber { get; set; } = null;

							[JsonPropertyName("bank_transfer_routing_number_confirmation")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String BankTransferRoutingNumberConfirmation { get; set; } = null;

							[JsonPropertyName("bank_transfer_account_number_confirmation")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String BankTransferAccountNumberConfirmation { get; set; } = null;

							[JsonPropertyName("bank_transfer_name")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String BankTransferName { get; set; } = null;

							[JsonPropertyName("bank_transfer_account_type")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public String BankTransferAccountType { get; set; } = "checking";

							[JsonIgnore]
							private Boolean? _sameAsDeployment = null;

							[JsonPropertyName("same_as_downpayment")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public Boolean? SameAsDownpayment
							{
								get
								{
									return (_sameAsDeployment ?? false) ? _sameAsDeployment : null;
								}
								set => _sameAsDeployment = value;
							}

							[JsonIgnore]
							private Boolean? _provideLater = null;

							[JsonPropertyName("provide_later")]
							[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
							public Boolean? ProvideLater
							{
								get
								{
									return (_provideLater ?? false) ? _provideLater : null;
								}
								set => _provideLater = value;
							}
							#endregion
						}
					}
				}
			}
		}
	}
}
