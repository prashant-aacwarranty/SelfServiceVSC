using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class Attributes
	{
		#region Users Attributes
		[JsonPropertyName("email")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Email { get; set; } = null;

		[JsonPropertyName("first-name")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String FirstName { get; set; } = null;

		[JsonPropertyName("last-name")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String LastName { get; set; } = null;
		#endregion

		#region Protections Attributes
		[JsonPropertyName("provider")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Provider { get; set; } = null;

		[JsonPropertyName("name")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Name { get; set; } = null;

		[JsonPropertyName("form_number")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String FormNumber { get; set; } = null;

		[JsonPropertyName("fields")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<Field> Fields { get; set; } = null;

		[JsonPropertyName("protection-category-id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public Int32? ProtectionCategoryId { get; set; } = null;

		[JsonPropertyName("protection-category-name")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String ProtectionCategoryName { get; set; } = null;

		[JsonPropertyName("brochure")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Brochure { get; set; } = null;
		#endregion

		#region Quotes Attributes
		[JsonPropertyName("integration-loan-id")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public Int32? IntegrationLoanId { get; set; } = null;

		[JsonPropertyName("skip-credit-validation")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Boolean? SkipCreditValidation { get; set; } = null;

		[JsonPropertyName("term")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public Int32? Term { get; set; } = null;

		[JsonPropertyName("down-payment")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? DownPayment { get; set; } = null;

		[JsonPropertyName("interest-percent")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public Int32? InterestPercent { get; set; } = null;
		#endregion

		#region Customers Attributes
		// FirstName/first-name covered in Users

		// LastName/last-name covered in Users

		[JsonPropertyName("ssn")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String SSN { get; set; } = null;

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

		// Email/email covered in Users

		[JsonPropertyName("cell-number")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String CellNumber { get; set; } = null;

		[JsonPropertyName("work-number")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String WorkNumber { get; set; } = null;

		[JsonPropertyName("phone-number")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String PhoneNumber { get; set; } = null;

		[JsonPropertyName("credit-score-tier")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String CreditScoreTier { get; set; } = null;
		#endregion

		#region Vehicles Attributes
		[JsonPropertyName("vin")]
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
		public String VIN { get; set; } = null;

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
		public String MSRP { get; set; } = null;

		[JsonPropertyName("finance-term")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public Int32? FinanceTerm { get; set; } = null;

		[JsonPropertyName("in-service-date")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public DateTime? InServiceDate { get; set; } = null;

		[JsonPropertyName("status")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Status { get; set; } = null;
		#endregion

		#region QuoteProtections Attributes
		[JsonPropertyName("exclude-tax")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Boolean? ExcludeTax { get; set; } = null;

		[JsonPropertyName("filled-fields")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<FilledField> FilledFields { get; set; } = null;

		// Term/term covered in Quotes

		// Name/name covered in Protections

		[JsonPropertyName("months")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
		public Int32? Months { get; set; } = null;

		// Mileage/mileage covered in Vehicles

		[JsonPropertyName("price")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String Price { get; set; } = null;
		#endregion

		#region Quotes Response Attributes
		[JsonPropertyName("link-to-quote-wizard")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String LinkToQuoteWizard { get; set; } = null;

		// IntegrationLoanId/integration-loan-id covered in Quotes

		// Status/status covered in Vehicles

		// Term/term covered in QuoteProtections

		[JsonPropertyName("monthly-payment")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? MonthlyPayment { get; set; } = null;

		[JsonPropertyName("protections-total")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? ProtectionsTotal { get; set; } = null;

		[JsonPropertyName("taxed-total")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? TaxedTotal { get; set; } = null;

		[JsonPropertyName("tax")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? Tax { get; set; } = null;

		[JsonPropertyName("subtotal")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? Subtotal { get; set; } = null;

		// DownPayment/down-payment

		[JsonPropertyName("total")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? Total { get; set; } = null;

		[JsonPropertyName("doc-stamp-fee")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? DocStampFee { get; set; } = null;

		[JsonPropertyName("financed-total")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? FinancedTotal { get; set; } = null;

		[JsonPropertyName("base-fee")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? BaseFee { get; set; } = null;

		[JsonPropertyName("buy-down-fee")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? BuyDownFee { get; set; } = null;

		[JsonPropertyName("additional-term-fee")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? AdditionalTermFee { get; set; } = null;

		[JsonPropertyName("fees")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Decimal? Fees { get; set; } = null;
		#endregion
	}
}
