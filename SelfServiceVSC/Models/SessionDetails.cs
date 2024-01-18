using AAC.SelfServiceVSC.Models.SCSAPI;
using AAC.SelfServiceVSC.Models.Form;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models
{
	/// <summary>
	/// Store application settings for quick access.
	/// </summary>
	public class SessionDetails
	{
		#region Properties
		public EstimateRequest EstimateRequest { get; set; }

		public QuoteRequest QuoteRequest { get; set; }

		public String EstimateId { get; set; } = null;

		public PaymentRequest PaymentRequest { get; set; }

		public List<SessionEstimate> SessionEstimates { get; set; }

		public string OdometerImage { get; set; }

		[JsonIgnore]
		public SessionEstimate SelectedSessionEstimate
		{
			get
			{
				return SessionEstimates.FirstOrDefault(estimate => estimate.Selected);
			}
		}

		public class Rate
		{
			public PlanRate PlanRate { get; set; } = null;

			public RateClassMoney RateClassMoney { get; set; } = null;

			public Boolean Selected { get; set; } = false;
		}

		public List<Rate> Rates { get; set; }

		[JsonIgnore]
		public Rate SelectedRate
		{
			get
			{
				return Rates.FirstOrDefault(rate => rate.Selected);
			}
		}

		public Line5API.SessionQuote Quote { get; set; }

		public Int32? InitialTerm { get; set; } = null;

		public Int64? LoanId { get; set; } = null;

		public String ContractNumber
		{
			get
			{
				return GeneratedContract?.ContractNumber ?? String.Format("DTC{0}", EstimateRequest.VIN.Substring(EstimateRequest.VIN.Length - 8));
			}
		}

		public FinalizeContractRequest FinalizeContract { get; set; } = null;

		public GenerateContractResponse GeneratedContract { get; set; } = null;
		#endregion

		#region Methods
		public void Reset()
		{
			EstimateRequest = null;
			QuoteRequest = null;
			EstimateId = null;
			SessionEstimates = null;
			Rates = null;
			Quote = null;
			InitialTerm = null;
			LoanId = null;
			FinalizeContract = null;
			GeneratedContract = null;
		}

		public void SelectEstimate(
			Int64? rateId)
		{
			SessionEstimates.ForEach(
				estimate =>
				{
					estimate.Selected = estimate.RateId == rateId;
				});

			Rates.ForEach(
				rate =>
				{
					rate.Selected = rate.RateClassMoney.Rate.RateId == rateId;
				});
		}

		#endregion
	}

	// usage: Session.GetSessionDetails

	public static class SessionExtensions
	{
		private const string SESSION_KEY = "Details";

		public static void SetSessionDetails(this ISession session, SessionDetails sessionDetails)
		{
			session.SetString(SESSION_KEY, JsonSerializer.Serialize(sessionDetails));
		}

		public static SessionDetails GetSessionDetails(this ISession session)
		{
			// this seems wildly incorrect
			var sessionData = session.GetString(SESSION_KEY);
			var returnSession = new SessionDetails();

			if (!String.IsNullOrWhiteSpace(sessionData))
			{
				try
				{
					returnSession = JsonSerializer.Deserialize<SessionDetails>(sessionData);
				}
				catch
				{
					returnSession = new SessionDetails();
				}
			}

			return returnSession;
		}
	}
}
