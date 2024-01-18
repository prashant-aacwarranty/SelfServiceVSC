using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class LoanGetResponse : ErrorsResponse
	{
		#region Properties
		[JsonPropertyName("data")]
		public DataModel Data { get; set; } = null;

		[JsonPropertyName("included")]
		public List<IncludedModel> Included { get; set; } = null;
		#endregion

		public class DataModel
		{
			#region Properties
			[JsonPropertyName("id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? Id { get; set; } = null;

			[JsonPropertyName("type")]
			public String Type { get; set; } = "loans";

			[JsonPropertyName("attributes")]
			public AttributesModel Attributes { get; set; } = null;

			[JsonPropertyName("relationships")]
			public RelationshipsModel Relationships { get; set; } = null;
			#endregion

			public class AttributesModel
			{
				#region Properties
				[JsonPropertyName("status")]
				public String Status { get; set; } = null;

				[JsonPropertyName("funded-at")]
				public String FundedAt { get; set; } = null;

				[JsonPropertyName("start-date")]
				public String StartDate { get; set; } = null;

				[JsonPropertyName("payment-day")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? PaymentDay { get; set; } = null;

				[JsonPropertyName("first-payment-date")]
				public String FirstPaymentDate { get; set; } = null;

				[JsonPropertyName("balance")]
				public String Balance { get; set; } = null;

				[JsonPropertyName("term")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Term { get; set; } = null;

				[JsonPropertyName("down-payment")]
				public String DownPayment { get; set; } = null;

				[JsonPropertyName("short-fund-fee")]
				public String ShortFundFee { get; set; } = null;

				[JsonPropertyName("monthly-payment")]
				public String MonthlyPayment { get; set; } = null;

				[JsonPropertyName("tax")]
				public String Tax { get; set; } = null;

				[JsonPropertyName("base-fee")]
				public String BaseFee { get; set; } = null;

				[JsonPropertyName("buy-down-fee")]
				public String BuyDownFee { get; set; } = null;

				[JsonPropertyName("additional-term-fee")]
				public String AdditionalTermFee { get; set; } = null;

				[JsonPropertyName("doc-stamp-fee")]
				public String DocStampFee { get; set; } = null;

				[JsonPropertyName("financed-total")]
				public String FinancedTotal { get; set; } = null;
				#endregion
			}

			public class RelationshipsModel
			{
				#region Properties
				[JsonPropertyName("unread-loan-notes")]
				public UnreadLoanNotesModel UnreadLoanNotes { get; set; } = null;

				[JsonPropertyName("actual-cover-sheet")]
				public ActualCoverSheetModel ActualCoverSheet { get; set; } = null;

				[JsonPropertyName("customer")]
				public CustomerModel Customer { get; set; } = null;

				[JsonPropertyName("vehicle")]
				public VehicleModel Vehicle { get; set; } = null;

				[JsonPropertyName("quote-protections")]
				public QuoteProtectionsModel QuoteProtections { get; set; } = null;
				#endregion

				public class UnreadLoanNotesModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public List<DataModel> Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties

						#endregion
					}
				}

				public class ActualCoverSheetModel
				{
					#region Properties
					[JsonPropertyName("id")]
					[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
					public Int64? Id { get; set; } = null;

					[JsonPropertyName("type")]
					public String Type { get; set; } = "loan-packages";
					#endregion
				}

				public class CustomerModel
				{
					#region Properties
					[JsonPropertyName("id")]
					[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
					public Int64? Id { set; get; } = null;

					[JsonPropertyName("type")]
					public String Type { get; set; } = "customers";
					#endregion
				}

				public class VehicleModel
				{
					#region Properties
					[JsonPropertyName("id")]
					[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
					public Int64? Id { set; get; } = null;

					[JsonPropertyName("type")]
					public String Type { get; set; } = "vehicles";
					#endregion
				}

				public class QuoteProtectionsModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public List<DataModel> Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties
						[JsonPropertyName("id")]
						[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
						public Int64? Id { set; get; } = null;

						[JsonPropertyName("type")]
						public String Type { get; set; } = "quote-protections";
						#endregion
					}
				}
			}
		}

		public class IncludedModel
		{
			#region Properties
			[JsonPropertyName("id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? Id { get; set; } = null;

			[JsonPropertyName("type")]
			public String Type { get; set; } = null;

			[JsonPropertyName("attributes")]
			public AttributesModel Attributes { get; set; } = null;

			[JsonPropertyName("links")]
			public LinksModel Links { get; set; } = null;

			[JsonPropertyName("term")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int32? Term { get; set; } = null;

			[JsonPropertyName("name")]
			public String Name { get; set; } = null;

			[JsonPropertyName("months")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int32? Months { get; set; } = null;

			[JsonPropertyName("mileage")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int32? Mileage { get; set; } = null;

			[JsonPropertyName("price")]
			public String Price { get; set; } = null;

			[JsonPropertyName("financing_limit")]
			public String FinancingLimit { get; set; } = null;

			[JsonPropertyName("exclude_tax")]
			public Boolean? ExcludeTax { get; set; } = null;

			[JsonPropertyName("protection_label_id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? ProtectionLabelId { get; set; } = null;

			[JsonPropertyName("coverage_id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? CoverageId { get; set; } = null;

			[JsonPropertyName("deductible_id")]
			[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
			public Int64? DeductibleId { get; set; } = null;
			#endregion

			public class AttributesModel
			{
				#region Properties
				[JsonPropertyName("first-name")]
				public String FirstName { get; set; } = null;

				[JsonPropertyName("last-name")]
				public String LastName { get; set; } = null;

				[JsonPropertyName("date-of-birth")]
				public String DateOfBirth { get; set; } = null;

				[JsonPropertyName("address-1")]
				public String Address1 { get; set; } = null;

				[JsonPropertyName("address-2")]
				public String Address2 { get; set; } = null;

				[JsonPropertyName("city")]
				public String City { get; set; } = null;

				[JsonPropertyName("state")]
				public String State { get; set; } = null;

				[JsonPropertyName("postal-code")]
				public String PostalCode { get; set; } = null;

				[JsonPropertyName("phone-number")]
				public String PhoneNumber { get; set; } = null;

				[JsonPropertyName("cell-number")]
				public String CellNumber { get; set; } = null;

				[JsonPropertyName("work-number")]
				public String WorkNumber { get; set; } = null;

				[JsonPropertyName("credit-score-tier")]
				public String CreditScoreTier { get; set; } = null;

				[JsonPropertyName("term")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Term { get; set; } = null;

				[JsonPropertyName("name")]
				public String Name { get; set; } = null;

				[JsonPropertyName("months")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Months { get; set; } = null;

				[JsonPropertyName("mileage")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Mileage { get; set; } = null;

				[JsonPropertyName("price")]
				public String Price { get; set; } = null;

				[JsonPropertyName("year")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Year { get; set; } = null;

				[JsonPropertyName("make")]
				public String Make { get; set; } = null;

				[JsonPropertyName("model")]
				public String Model { get; set; } = null;

				[JsonPropertyName("vin")]
				public String Vin { get; set; } = null;

				[JsonPropertyName("purchase-price")]
				public String PurchasePrice { get; set; } = null;

				[JsonPropertyName("financed-amount")]
				public String FinancedAmount { get; set; } = null;

				[JsonPropertyName("msrp")]
				public String Msrp { get; set; } = null;

				[JsonPropertyName("finance-term")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? FinanceTerm { get; set; } = null;

				[JsonPropertyName("in-service-date")]
				public String InServiceDate { get; set; } = null;

				[JsonPropertyName("status")]
				public String Status { get; set; } = null;

				[JsonPropertyName("documents-file-name")]
				public String DocumentsFileName { get; set; } = null;

				[JsonPropertyName("documents-content-type")]
				public String DocumentsContentType { get; set; } = null;

				[JsonPropertyName("documents-file-size")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? DocumentsFileSize { get; set; } = null;
				#endregion
			}

			public class LinksModel
			{
				#region Properties
				[JsonPropertyName("download")]
				public String Download { get; set; } = null;
				#endregion
			}
		}
	}
}
