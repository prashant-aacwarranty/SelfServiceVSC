using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class SessionQuote
	{
		#region Properties
		public Int64? QuoteId { get; set; } = null;

		public Int32? Term { get; set; } = null;

		public Decimal? MonthlyPayment { get; set; } = null;

		public Decimal? ProtectionsTotal { get; set; } = null;

		public Decimal? TaxedTotal { get; set; } = null;

		public Decimal? Tax { get; set; } = null;

		public Decimal? Subtotal { get; set; } = null;

		public Decimal? DownPayment { get; set; } = null;

		public Decimal? Total { get; set; } = null;

		public Decimal? DocStampFee { get; set; } = null;

		public Decimal? FinancedTotal { get; set; } = null;

		public Decimal? BaseFee { get; set; } = null;

		public Decimal? BuyDownFee { get; set; } = null;

		public Decimal? InterestPercent { get; set; } = null;

		public Decimal? AdditionalTermFee { get; set; } = null;

		public Decimal? Fees { get; set; } = null;

		public Decimal? MinDownPayment { get; set; } = null;

		public Decimal? MaxDownPayment { get; set; } = null;
		#endregion

		#region Constructors
		public SessionQuote()
		{

		}

		public SessionQuote(
			QuotePostResponse quotePostResponse)
		{
			QuoteId = quotePostResponse.Data.Id;
			Term = quotePostResponse.Data.Attributes.Term;
			MonthlyPayment = quotePostResponse.Data.Attributes.MonthlyPayment;
			ProtectionsTotal = quotePostResponse.Data.Attributes.ProtectionsTotal;
			TaxedTotal = quotePostResponse.Data.Attributes.TaxedTotal;
			Tax = quotePostResponse.Data.Attributes.Tax;
			Subtotal = quotePostResponse.Data.Attributes.Subtotal;
			DownPayment = quotePostResponse.Data.Attributes.DownPayment;
			Total = quotePostResponse.Data.Attributes.Total;
			DocStampFee = quotePostResponse.Data.Attributes.DocStampFee;
			FinancedTotal = quotePostResponse.Data.Attributes.FinancedTotal;
			BaseFee = quotePostResponse.Data.Attributes.BaseFee;
			BuyDownFee = quotePostResponse.Data.Attributes.BuyDownFee;
			InterestPercent = quotePostResponse.Data.Attributes.InterestPercent;
			AdditionalTermFee = quotePostResponse.Data.Attributes.AdditionalTermFee;
			Fees = quotePostResponse.Data.Attributes.Fees;
			MinDownPayment = quotePostResponse.Data.Attributes.MinDownPayment;
			MaxDownPayment = quotePostResponse.Data.Attributes.MaxDownPayment;
		}
		#endregion

		#region Client Version
		public class ClientQuote
		{
			#region Properties
			public Int64? QuoteId { get; set; } = null;

			public Int32? Term { get; set; } = null;

			public Decimal? MonthlyPayment { get; set; } = null;

			public Decimal? ProtectionsTotal { get; set; } = null;

			public Decimal? TaxedTotal { get; set; } = null;

			public Decimal? Tax { get; set; } = null;

			public Decimal? Subtotal { get; set; } = null;

			public Decimal? DownPayment { get; set; } = null;

			public Decimal? Total { get; set; } = null;

			public Decimal? DocStampFee { get; set; } = null;

			public Decimal? FinancedTotal { get; set; } = null;

			public Decimal? BaseFee { get; set; } = null;

			public Decimal? BuyDownFee { get; set; } = null;

			public Decimal? InterestPercent { get; set; } = null;

			public Decimal? AdditionalTermFee { get; set; } = null;

			public Decimal? Fees { get; set; } = null;

			public Decimal? MinDownPayment { get; set; } = null;

			public Decimal? MaxDownPayment { get; set; } = null;
			#endregion
		}

		public ClientQuote GetClientQuote()
		{
			return new ClientQuote
			{
				QuoteId = QuoteId,
				Term = Term,
				MonthlyPayment = MonthlyPayment,
				ProtectionsTotal = ProtectionsTotal,
				TaxedTotal = TaxedTotal,
				Tax = Tax,
				Subtotal = Subtotal,
				DownPayment = DownPayment,
				Total = Total,
				DocStampFee = DocStampFee,
				FinancedTotal = FinancedTotal,
				BaseFee = BaseFee,
				BuyDownFee = BuyDownFee,
				InterestPercent = InterestPercent,
				AdditionalTermFee = AdditionalTermFee,
				Fees = Fees,
				MinDownPayment = MinDownPayment,
				MaxDownPayment = MaxDownPayment
			};
		}
		#endregion
	}
}
