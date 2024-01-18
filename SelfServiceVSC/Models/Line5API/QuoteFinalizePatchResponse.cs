using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class QuoteFinalizePatchResponse : ErrorsResponse
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

			[JsonPropertyName("included")]
			public List<IncludedModel> Included { get; set; } = null;
			#endregion

			public class AttributesModel
			{
				#region Properties
				[JsonPropertyName("status")]
				public String Status { get; set; } = null;

				[JsonPropertyName("funded-at")]
				public DateTime? FundedAt { get; set; } = null;

				[JsonPropertyName("start-date")]
				public DateTime? StartDate { get; set; } = null;

				[JsonPropertyName("payment-day")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? PaymentDay { get; set; } = null;

				[JsonPropertyName("first-payment-date")]
				public DateTime? FirstPaymentDate { get; set; } = null;

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
				[JsonPropertyName("loannotes")]
				public LoanNotesModel LoanNotes { get; set; } = null;

				[JsonPropertyName("unreadloannotes")]
				public UnreadLoanNotesModel UnreadLoanNotes { get; set; } = null;

				[JsonPropertyName("actualcoversheet")]
				public ActualCoverSheetModel ActualCoverSheet { get; set; } = null;

				[JsonPropertyName("customer")]
				public CustomerModel Customer { get; set; } = null;

				[JsonPropertyName("vehicle")]
				public VehicleModel Vehicle { get; set; } = null;

				[JsonPropertyName("quoteprotections")]
				public QuoteProtectionsModel QuoteProtections { get; set; } = null;
				#endregion

				public class LoanNotesModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public DataModel Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties
						#endregion
					}
				}

				public class UnreadLoanNotesModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public DataModel Data { get; set; } = null;
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
						public String Type { get; set; } = null;
						#endregion
					}
				}

				public class CustomerModel
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
						public String Type { get; set; } = "customers";
						#endregion
					}
				}

				public class VehicleModel
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
						public String Type { get; set; } = "vehicles";
						#endregion
					}
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
						public Int64? Id { get; set; } = null;

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
			#endregion

			public class AttributesModel
			{
				#region Properties
				[JsonPropertyName("documents-file-name")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String DocumentsFileName { get; set; } = null;

				[JsonPropertyName("documents-content-type")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String DocumentsContentType { get; set; } = null;

				[JsonPropertyName("documents-file-size")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? DocumentsFileSize { get; set; } = null;

				[JsonPropertyName("first-name")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String FirstName { get; set; } = null;

				[JsonPropertyName("last-name")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String LastName { get; set; } = null;

				[JsonPropertyName("date-of-birth")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String DateOfBirth { get; set; } = null;

				[JsonPropertyName("address-1")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String Address1 { get; set; } = null;

				[JsonPropertyName("address-2")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String Address2 { get; set; } = null;

				[JsonPropertyName("city")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String City { get; set; } = null;

				[JsonPropertyName("state")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String State { get; set; } = null;

				[JsonPropertyName("postal-code")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String PostalCode { get; set; } = null;

				[JsonPropertyName("phone-number")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String PhoneNumber { get; set; } = null;

				[JsonPropertyName("cell-number")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String CellNumber { get; set; } = null;

				[JsonPropertyName("work-number")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String WorkNumber { get; set; } = null;

				[JsonPropertyName("credit-score-tier")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String CreditScoreTier { get; set; } = null;

				[JsonPropertyName("year")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Year { get; set; } = null;

				[JsonPropertyName("make")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String Make { get; set; } = null;

				[JsonPropertyName("model")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String Model { get; set; } = null;

				[JsonPropertyName("vin")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String Vin { get; set; } = null;

				[JsonPropertyName("mileage")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Mileage { get; set; } = null;

				[JsonPropertyName("purchase-price")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String PurchasePrice { get; set; } = null;

				[JsonPropertyName("financed-amount")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String FinancedAmount { get; set; } = null;

				[JsonPropertyName("msrp")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String Msrp { get; set; } = null;

				[JsonPropertyName("finance-term")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? FinanceTerm { get; set; } = null;

				[JsonPropertyName("in-service-date")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String InServiceDate { get; set; } = null;

				[JsonPropertyName("status")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String Status { get; set; } = null;

				[JsonPropertyName("term")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Term { get; set; } = null;

				[JsonPropertyName("name")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String Name { get; set; } = null;

				[JsonPropertyName("months")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int32? Months { get; set; } = null;

				[JsonPropertyName("price")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public String Price { get; set; } = null;
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
