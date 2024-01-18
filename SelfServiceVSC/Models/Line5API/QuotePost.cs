using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public class QuotePost
	{
		#region Properties
		[JsonPropertyName("data")]
		public DataModel Data { get; set; } = null;
		#endregion

		public class DataModel
		{
			#region Properties
			[JsonPropertyName("id")]
			[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
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
				[JsonPropertyName("integration-loan-id")]
				[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
				public Int64? IntegrationLoanId { get; set; } = null;

				[JsonPropertyName("skip-credit-validation")]
				public Boolean? SkipCreditValidation { get; set; } = null;
				#endregion
			}

			public class RelationshipsModel
			{
				#region Properties
				[JsonPropertyName("customer")]
				public CustomerModel Customer { get; set; } = null;

				[JsonPropertyName("vehicle")]
				public VehicleModel Vehicle { get; set; } = null;

				[JsonPropertyName("employee")]
				public EmployeeModel Employee { get; set; } = null;

				[JsonPropertyName("quote-protections")]
				[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
				public QuoteProtectionsModel QuoteProtections { get; set; } = null;
				#endregion

				public class CustomerModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public DataModel Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties
						[JsonPropertyName("type")]
						public String Type { get; set; } = "customers";

						[JsonPropertyName("attributes")]
						public AttributesModel Attributes { get; set; } = null;
						#endregion

						public class AttributesModel
						{
							#region Properties
							[JsonPropertyName("first-name")]
							public String FirstName { get; set; } = null;

							[JsonPropertyName("last-name")]
							public String LastName { get; set; } = null;

							[JsonPropertyName("ssn")]
							public String SSN { get; set; } = null;

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

							[JsonPropertyName("email")]
							public String Email { get; set; } = null;

							[JsonPropertyName("cell-number")]
							public String CellNumber { get; set; } = null;

							[JsonPropertyName("work-number")]
							public String WorkNumber { get; set; } = null;

							[JsonPropertyName("phone-number")]
							public String PhoneNumber { get; set; } = null;
							#endregion
						}
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
						[JsonPropertyName("type")]
						public String Type { get; set; } = "vehicles";

						[JsonPropertyName("attributes")]
						public AttributesModel Attributes { get; set; } = null;
						#endregion

						public class AttributesModel
						{
							#region Properties
							[JsonPropertyName("vin")]
							public String VIN { get; set; } = null;

							[JsonPropertyName("mileage")]
							[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
							public Int32? Mileage { get; set; } = null;
							#endregion
						}
					}
				}

				public class EmployeeModel
				{
					#region Properties
					[JsonPropertyName("data")]
					public DataModel Data { get; set; } = null;
					#endregion

					public class DataModel
					{
						#region Properties
						[JsonPropertyName("type")]
						public String Type { get; set; } = "employee";

						[JsonPropertyName("id")]
						[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
						public Int64? Id { get; set; } = null;
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
						[JsonPropertyName("type")]
						public String Type { get; set; } = "quote-protection";

						[JsonPropertyName("attributes")]
						public AttributesModel Attributes { get; set; } = null;

						[JsonPropertyName("relationships")]
						public RelationshipsModel Relationships { get; set; } = null;
						#endregion

						public class AttributesModel
						{
							#region Properties
							[JsonPropertyName("exclude-tax")]
							public Boolean? ExcludeTax { get; set; } = null;

							[JsonPropertyName("filled-fields")]
							public List<FilledFieldModel> FilledFields { get; set; } = null;
							#endregion

							public class FilledFieldModel
							{
								#region Properties
								[JsonPropertyName("name")]
								public String Name { get; set; } = null;

								[JsonPropertyName("value")]
								public Decimal? Value { get; set; } = null;
								#endregion
							}
						}

						public class RelationshipsModel
						{
							#region Properties
							[JsonPropertyName("protection")]
							public ProtectionModel Protection { get; set; } = null;
							#endregion

							public class ProtectionModel
							{
								#region Properties
								[JsonPropertyName("data")]
								public DataModel Data { get; set; } = null;
								#endregion

								public class DataModel
								{
									#region Properties
									[JsonPropertyName("type")]
									public String Type { get; set; } = "protection";

									[JsonPropertyName("id")]
									[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
									[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
									public Int64? Id { get; set; } = null;
									#endregion
								}
							}
						}
					}
				}
			}
		}
	}
}
