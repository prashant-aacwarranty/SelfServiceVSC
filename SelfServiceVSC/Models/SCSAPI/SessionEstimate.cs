namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Quotes based on PlanRates, for transmission to browser
	/// </summary>
	public class SessionEstimate
	{
		#region Properties
		public String ProductTypeCode { get; set; } = null;

		public String PlanDescription { get; set; } = null;

		public Int64? PlanId { get; set; } = null;

		public Int16? Term { get; set; } = null;

		public Int32? Mileage { get; set; } = null;

		public Decimal? DeductAmt { get; set; } = null;

		public Int64? RateId { get; set; } = null;

		public Decimal? RetailRate { get; set; } = null;

		public Decimal? NetRate { get; set; } = null;

		public Int32? ExpirationMileage { get; set; } = null;

		public DateTime? ExpirationDate { get; set; } = null;

		public Boolean Selected { get; set; } = false;
		#endregion

		#region Constructors
		public SessionEstimate()
		{

		}

		public SessionEstimate(
			Plan plan,
			RateClassMoney rateClassMoney)
		{
			ProductTypeCode = plan.ProductTypeCode;
			PlanDescription = plan.PlanDescription;
			PlanId = plan.PlanId;
			Term = rateClassMoney.TermMile.Term;
			Mileage = rateClassMoney.TermMile.Mileage;
			DeductAmt = rateClassMoney.Deductible.DeductAmt;
			RateId = rateClassMoney.Rate.RateId;
			var surcharges = rateClassMoney.Options.Where(option => option.IsSurcharge == true).Sum(option => option.RetailRate);
			RetailRate = rateClassMoney.Rate.RetailRate + surcharges;
			NetRate = rateClassMoney.Rate.NetRate + surcharges;
			ExpirationMileage = rateClassMoney.Rate.ExpirationMileage;
			ExpirationDate = rateClassMoney.Rate.ExpirationDate;
		}
		#endregion

		#region Client Version
		public class ClientEstimate
		{
			#region Properties
			public String ProductTypeCode { get; set; } = null;

			public String PlanDescription { get; set; } = null;

			public Int64? PlanId { get; set; } = null;

			public Int16? Term { get; set; } = null;

			public Int32? Mileage { get; set; } = null;

			public Decimal? DeductAmt { get; set; } = null;

			public Int64? RateId { get; set; } = null;

			public Decimal? RetailRate { get; set; } = null;

			public Int32? ExpirationMileage { get; set; } = null;

			public DateTime? ExpirationDate { get; set; } = null;

			public Boolean Selected { get; set; } = false;
			#endregion
		}

		public ClientEstimate GetClientEstimate()
		{
			return new ClientEstimate
			{
				ProductTypeCode = ProductTypeCode,
				PlanDescription = PlanDescription,
				PlanId = PlanId,
				Term = Term,
				Mileage = Mileage,
				DeductAmt = DeductAmt,
				RateId = RateId,
				RetailRate = RetailRate,
				ExpirationMileage = ExpirationMileage,
				ExpirationDate = ExpirationDate,
				Selected = Selected
			};
		}

		#endregion
	}
}
