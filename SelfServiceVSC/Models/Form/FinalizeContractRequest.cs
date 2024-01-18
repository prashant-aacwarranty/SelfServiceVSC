using AAC.SelfServiceVSC.Models.PaylinkAPI;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AAC.SelfServiceVSC.Models.Form
{
	public class FinalizeContractRequest
	{
		public Boolean PaymentPlan { get; set; }

		#region Pay in Full Inputs
		public String PaymentCardName { get; set; } = null;

		public String PaymentCardNumber { get; set; } = null;

		public String PaymentCardExpiration { get; set; } = null;

		[JsonIgnore]
		public Byte PaymentCardMonth
		{
			get
			{
				if (string.IsNullOrEmpty(PaymentCardExpiration))
					return Byte.MinValue;

				if (!Regex.IsMatch(PaymentCardExpiration, @"\d{2}/\d{4}"))
				{
					throw new Exception("Invalid expiration date.");
				}
				return Byte.Parse(Regex.Match(PaymentCardExpiration, @"^\d{2}").Value);
			}
		}

		[JsonIgnore]
		public UInt16 PaymentCardYear
		{
			get
			{
				if (string.IsNullOrEmpty(PaymentCardExpiration))
					return Byte.MinValue;
				if (!Regex.IsMatch(PaymentCardExpiration, @"\d{2}/\d{4}"))
				{
					throw new Exception("Invalid expiration date.");
				}
				return UInt16.Parse(Regex.Match(PaymentCardExpiration, @"\d{4}$").Value);
			}
		}
		#endregion

		#region Payment Plan Inputs
		public String Downpayment { get; set; }

		public String Term { get; set; }

		public Boolean SameMonthly { get; set; }

		[JsonIgnore]
		public BillingMethod DownpaymentBillingMethod
		{
			get
			{
				return DownpaymentBankTransferRoutingNumber != null ? BillingMethod.ACH : BillingMethod.CREDIT;
			}
		}

		public String DownpaymentPaymentCardName { get; set; } = null;

		public String DownpaymentPaymentCardNumber { get; set; } = null;

		public String DownpaymentPaymentCardExpiration { get; set; } = null;

		[JsonIgnore]
		public Byte DownpaymentPaymentCardMonth
		{
			get
			{
				if (!string.IsNullOrEmpty(DownpaymentPaymentCardExpiration))
				{
					if (!Regex.IsMatch(DownpaymentPaymentCardExpiration, @"\d{2}/\d{2}"))
					{
						throw new Exception("Invalid expiration date.");
					}
					return Byte.Parse(Regex.Match(DownpaymentPaymentCardExpiration, @"^\d{2}").Value);
				}
				else
					return Byte.MinValue;
			}
		}

		[JsonIgnore]
		public UInt16 DownpaymentPaymentCardYear
		{
			get
			{
				if (!string.IsNullOrEmpty(DownpaymentPaymentCardExpiration))
				{
					if (!Regex.IsMatch(DownpaymentPaymentCardExpiration, @"\d{2}/\d{2}"))
					{
						throw new Exception("Invalid expiration date.");
					}
					return UInt16.Parse(Regex.Match(DownpaymentPaymentCardExpiration, @"\d{2}$").Value);
				}
					return UInt16.MinValue;
			}
		}

		public String DownpaymentBankTransferName { get; set; } = null;

		public String DownpaymentBankTransferAccountType { get; set; } = null;

		[JsonIgnore]
		public AccountType DownpaymentBankTransferAccountTypeEnum
		{
			get
			{
				return DownpaymentBankTransferAccountType == "SAVINGS" ? AccountType.SAVINGS : AccountType.CHECKING;
			}
		}

		public String DownpaymentBankTransferRoutingNumber { get; set; } = null;

		public String DownpaymentBankTransferAccountNumber { get; set; } = null;

		public String DownpaymentBankTransferRoutingNumberConfirmation { get; set; } = null;

		public String DownpaymentBankTransferAccountNumberConfirmation { get; set; } = null;

		[JsonIgnore]
		public BillingMethod MonthlyBillingMethod
		{
			get
			{
				return MonthlyBankTransferRoutingNumberCalculated != null ? BillingMethod.ACH : BillingMethod.CREDIT;
			}
		}

		public String MonthlyPaymentCardName { get; set; } = null;

		[JsonIgnore]
		public String MonthlyPaymentCardNameCalculated
		{
			get
			{
				return SameMonthly ? DownpaymentPaymentCardName : MonthlyPaymentCardName;
			}
		}

		public String MonthlyPaymentCardNumber { get; set; } = null;

		[JsonIgnore]
		public String MonthlyPaymentCardNumberCalculated
		{
			get
			{
				return SameMonthly ? DownpaymentPaymentCardNumber : MonthlyPaymentCardNumber;
			}
		}

		public String MonthlyPaymentCardExpiration { get; set; } = null;

		public String MonthlyPaymentCardExpirationCalculated
		{
			get
			{
				return SameMonthly ? DownpaymentPaymentCardExpiration : MonthlyPaymentCardExpiration;
			}
		}

		[JsonIgnore]
		public Byte MonthlyPaymentCardMonth
		{
			get
			{
				if (!string.IsNullOrEmpty(MonthlyPaymentCardExpirationCalculated))
				{
					if (!Regex.IsMatch(MonthlyPaymentCardExpirationCalculated, @"\d{2}/\d{4}"))
					{
						throw new Exception("Invalid expiration date.");
					}
					return Byte.Parse(Regex.Match(MonthlyPaymentCardExpirationCalculated, @"^\d{2}").Value);
				}
				else
					return Byte.MinValue;
			}
		}

		[JsonIgnore]
		public UInt16 MonthlyPaymentCardYear
		{
			get
			{
				if (!string.IsNullOrEmpty(MonthlyPaymentCardExpirationCalculated))
				{
					if (!Regex.IsMatch(MonthlyPaymentCardExpirationCalculated, @"\d{2}/\d{4}"))
					{
						throw new Exception("Invalid expiration date.");
					}
					return UInt16.Parse(Regex.Match(MonthlyPaymentCardExpirationCalculated, @"\d{4}$").Value);
				}
				else
					return Byte.MinValue;
			}
		}

		public String MonthlyBankTransferName { get; set; } = null;

		[JsonIgnore]
		public String MonthlyBankTransferNameCalculated
		{
			get
			{
				return SameMonthly ? DownpaymentBankTransferName : MonthlyBankTransferName;
			}
		}

		public String MonthlyBankTransferAccountType { get; set; } = null;

		[JsonIgnore]
		public AccountType MonthlyBankTransferAccountTypeEnum
		{
			get
			{
				return SameMonthly ? DownpaymentBankTransferAccountTypeEnum : (MonthlyBankTransferAccountType == "SAVINGS" ? AccountType.SAVINGS : AccountType.CHECKING);
			}
		}

		public String MonthlyBankTransferRoutingNumber { get; set; } = null;

		[JsonIgnore]
		public String MonthlyBankTransferRoutingNumberCalculated
		{
			get
			{
				return SameMonthly ? DownpaymentBankTransferRoutingNumber : MonthlyBankTransferRoutingNumber;
			}
		}

		public String MonthlyBankTransferAccountNumber { get; set; } = null;

		[JsonIgnore]
		public String MonthlyBankTransferAccountNumberCalculated
		{
			get
			{
				return SameMonthly ? DownpaymentBankTransferAccountNumber : MonthlyBankTransferAccountNumber;
			}
		}

		public String MonthlyBankTransferRoutingNumberConfirmation { get; set; } = null;

		[JsonIgnore]
		public String MonthlyBankTransferRoutingNumberConfirmationCalculated
		{
			get
			{
				return SameMonthly ? DownpaymentBankTransferRoutingNumberConfirmation : MonthlyBankTransferRoutingNumberConfirmation;
			}
		}

		public String MonthlyBankTransferAccountNumberConfirmation { get; set; } = null;

		[JsonIgnore]
		public String MonthlyBankTransferAccountNumberConfirmationCalculated
		{
			get
			{
				return SameMonthly ? DownpaymentBankTransferAccountNumberConfirmation : MonthlyBankTransferAccountNumberConfirmation;
			}
		}
		#endregion

		#region Billing Address Inputs
		public Boolean SameBilling { get; set; }

		public String BillingFirstName { get; set; } = null;

		public String BillingLastName { get; set; } = null;

		[JsonIgnore]
		public String BillingFullName
		{
			get
			{
				return BillingFirstName + " " + BillingLastName;
			}
		}

		public String BillingAddress1 { get; set; } = null;

		public String BillingCity { get; set; } = null;

		public String BillingState { get; set; } = null;

		public String BillingZip { get; set; } = null;
		#endregion

		public String DigitallySigned { get; set; } = null;

		public IFormFile OdometerImage { get; set; } = null;
	}
}
