using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Form
{
	public class ModifyQuoteRequest
	{
		[JsonPropertyName("term")]
		public String Term { get; set; } = null;

		[JsonIgnore]
		public Int32? TermInt
		{
			get
			{
				Int32 term;
				Int32.TryParse(Term, out term);
				return term == 0 ? null : term;
			}
		}

		[JsonPropertyName("downpayment")]
		public String DownPayment { get; set; } = null;

		[JsonIgnore]
		public Decimal? DownPaymentDecimal
		{
			get
			{
				Decimal downpayment;
				Decimal.TryParse(DownPayment, out downpayment);
				return downpayment;
			}
		}
	}
}
