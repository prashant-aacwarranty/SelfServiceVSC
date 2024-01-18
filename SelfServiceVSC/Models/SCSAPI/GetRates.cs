using System.Xml.Serialization;

namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Schema used to request quotes from SCS.
	/// </summary>
	public class GetRates
	{
		#region Properties
		/// <summary>
		/// Third Party Administrator Code. The three- or four-letter code assigned to each supported SCS Auto provider.
		/// </summary>
		public String TpaCode { get; set; } = null;

		/// <summary>
		/// User ID. Required for secure access to SCS Auto eRating Web Service.
		/// </summary>
		public String UserId { get; set; } = null;

		/// <summary>
		/// Password. Required for secure access to SCS Auto eRating Web Service.
		/// </summary>
		public String Password { get; set; } = null;

		/// <summary>
		/// The dealer number. A unique identified assigned by each SCS Auto provider to a dealer/producer.
		/// </summary>
		public String DealerNo { get; set; } = null;

		/// <summary>
		/// Date of sale for which the rates are to be returned.
		/// </summary>
		[XmlElement(DataType = "date")]
		public DateTime? SaleDate { get; set; } = null;

		public Boolean SaleDateSpecified { get { return SaleDate != null; } }

		/// <summary>
		/// Flag used to determine whether to return new or used plans. If * is sent all eligible plans are returned (both new and used vehicle plans).
		/// Valid values:
		///		N – New
		///		U – Used
		///		* - All eligible plans(both new and used)
		/// </summary>
		public String NewUsed { get; set; } = null;

		/// <summary>
		/// Vehicle Identification Number
		/// Note: If the VIN is provided, Vehicle year, make, model, and vehicle attributes are not needed for rating.
		/// </summary>
		public String VIN { get; set; } = null;

		/// <summary>
		/// Vehicle trim
		/// If the VIN has multiple Trims associated with it, a value is required to uniquely identify the vehicle.The trim must be valid for the VIN.
		/// To get valid trim values for a VIN, there is a GetTrimsByVIN web service that can be used to get all valid trim values for a VIN.
		/// </summary>
		public String Trim { get; set; } = null;

		/// <summary>
		/// Only required when VIN is not provided.
		/// </summary>
		public Int16? VehicleYear { get; set; } = null;

		public Boolean VehicleYearSpecified { get { return VehicleYear != null; } }

		/// <summary>
		/// Only required when VIN is not provided.
		/// </summary>
		public String VehicleMake { get; set; } = null;

		/// <summary>
		/// Only required when VIN is not provided.
		/// </summary>
		public String VehicleModel { get; set; } = null;

		/// <summary>
		/// Payload capacity of the vehicle. Trucks only.
		/// Only required when VIN is not provided.
		/// </summary>
		public String TonRating { get; set; } = null;

		/// <summary>
		/// Engine Information.
		/// Only required when VIN is not provided.
		/// </summary>
		public String AspirationCode { get; set; } = null;

		/// <summary>
		/// Driving Wheels.
		/// Only required when VIN is not provided.
		/// </summary>
		public String DrivingWheels { get; set; } = null;

		/// <summary>
		/// Segmentation Code.
		/// Only required when VIN is not provided.
		/// </summary>
		public String SegmentationCode { get; set; } = null;

		/// <summary>
		/// Cylinders.
		/// Only required when VIN is not provided.
		/// </summary>
		public String Cylinders { get; set; } = null;

		/// <summary>
		/// Fuel Type.
		/// Only required when VIN is not provided.
		/// </summary>
		public String FuelType { get; set; } = null;

		/// <summary>
		/// Gross Vehicle Weight
		/// Only required when VIN is not provided.
		/// </summary>
		public String GVWR { get; set; } = null;

		/// <summary>
		/// Only required when VIN is not provided.
		/// Asset Type code should be used.
		/// </summary>
		public String AssetType { get; set; } = null;

		/// <summary>
		/// Only required when VIN is not provided.
		/// </summary>
		public String BodyStyle { get; set; } = null;

		/// <summary>
		/// Only required when VIN is not provided.
		/// </summary>
		public Int32? EngineSize { get; set; } = null;

		public Boolean EngineSizeSpecified { get { return EngineSize != null; } }

		/// <summary>
		/// CC = cubic centimeters
		/// CID = cubic inches
		/// Only required when VIN is not provided.
		/// </summary>
		public String EngineSizeUnit { get; set; } = null;

		/// <summary>
		/// Only required when VIN is not provided.
		/// Valid values:
		///		Y = Complete Vehicle
		///		N= Incomplete Vehicle
		/// </summary>
		public String CompleteVehicle { get; set; } = null;

		/// <summary>
		/// Vehicle Purchase Price
		/// This field may be required in order to get rates for some plans such as VSC, GAP, and prepaid maintenance.
		/// </summary>
		public Decimal? VehiclePurchasePrice { get; set; } = null;

		public Boolean VehiclePurchasePriceSpecified { get { return VehiclePurchasePrice != null; } }

		/// <summary>
		/// Vehicle Odometer Reading at time of sale
		/// </summary>
		public Int32? VehicleOdometer { get; set; } = null;

		public Boolean VehicleOdometerSpecified { get { return VehicleOdometer != null; } }

		/// <summary>
		/// In Service Date.
		/// This field may be required in order to get rates for some plans such as VSC and prepaid maintenance.
		/// </summary>
		[XmlElement(DataType = "date")]
		public DateTime? InserviceDate { get; set; } = null;

		public Boolean InserviceDateSpecified { get { return InserviceDate != null; } }

		/// <summary>
		/// Vehicle Purchase Date.
		/// This field may be required in order to get rates for some plans such as VSC and prepaid maintenance.
		/// </summary>
		[XmlElement(DataType = "date")]
		public DateTime? VehiclePurchaseDate { get; set; } = null;

		public Boolean VehiclePurchaseDateSpecified { get { return VehiclePurchaseDate != null; } }

		/// <summary>
		/// Financed Amount. Required to get GAP and Extended Wear and Tear rates. (Note: Some Providers may use these fields for other products besides GAP and EWT)
		/// </summary>
		public Decimal? FinancedAmount { get; set; } = null;

		public Boolean FinancedAmountSpecified { get { return FinancedAmount != null; } }

		/// <summary>
		/// Finance Term. Required to get GAP and Extended Wear and Tear rates. (Note: Some Providers may use these fields for other products besides GAP and EWT)
		/// </summary>
		public Int16? FinanceTerm { get; set; } = null;

		public Boolean FinanceTermSpecified { get { return FinanceTerm != null; } }

		/// <summary>
		/// If it is blank value will be ‘ANY’. Required to get GAP and Extended Wear and Tear rates. (Note: Some Providers may use these fields for other products besides GAP and EWT)
		/// Valid values:
		///		B - Balloon
		///		L- Lease
		///		F- Loan
		/// </summary>
		public String FinanceType { get; set; } = null;

		/// <summary>
		/// Required to get GAP and Extended Wear and Tear rates. (Note: Some Providers may use these fields for other products besides GAP and EWT)
		/// </summary>
		public Decimal? FinanceApr { get; set; } = null;

		public Boolean FinanceAprSpecified { get { return FinanceApr != null; } }

		/// <summary>
		/// Required to get GAP and Extended Wear and Tear rates. (Note: Some Providers may use these fields for other products besides GAP and EWT)
		/// </summary>
		public Decimal? MSRP { get; set; } = null;

		public Boolean MSRPSpecified { get { return MSRP != null; } }

		/// <summary>
		/// Not used
		/// </summary>
		public Boolean? GIPIteration { get; set; } = null;

		public Boolean GIPIterationSpecified { get { return GIPIteration != null; } }

		/// <summary>
		/// Not used
		/// </summary>
		public Decimal? MonthlyPayment { get; set; } = null;

		public Boolean MonthlyPaymentSpecified { get { return MonthlyPayment != null; } }

		/// <summary>
		/// Not used
		/// </summary>
		public DateTime? DOBBuyer { get; set; } = null;

		public Boolean DOBBuyerSpecified { get { return DOBBuyer != null; } }

		/// <summary>
		/// Not used
		/// </summary>
		public DateTime? DOBCoBuyer { get; set; } = null;

		public Boolean DOBCoBuyerSpecified { get { return DOBCoBuyer != null; } }

		/// <summary>
		/// Not used
		/// </summary>
		public Int16? FullManufWarrMonths { get; set; } = null;

		public Boolean FullManufWarrMonthsSpecified { get { return FullManufWarrMonths != null; } }

		/// <summary>
		/// Not used
		/// </summary>
		public Int32? FullManufWarrMiles { get; set; } = null;

		public Boolean FullManufWarrMilesSpecified { get { return FullManufWarrMiles != null; } }

		/// <summary>
		/// Not used
		/// </summary>
		public Int16? PowerTrainManufWarrMonths { get; set; } = null;

		public Boolean PowerTrainManufWarrMonthsSpecified { get { return PowerTrainManufWarrMonths != null; } }

		/// <summary>
		/// Not used
		/// </summary>
		public Int32? PowerTrainManufWarrMiles { get; set; } = null;

		public Boolean PowerTrainManufWarrMilesSpecified { get { return PowerTrainManufWarrMiles != null; } }

		/// <summary>
		/// Not used
		/// </summary>
		public String Surcharges { get; set; } = null;

		/// <summary>
		/// Not used
		/// </summary>
		public String CultureName { get; set; } = null;

		/// <summary>
		/// Additional Base Warranty Term (Months)
		/// </summary>
		public Int16? AdditionalBaseWarrantyTerm { get; set; } = null;

		public Boolean AdditionalBaseWarrantyTermSpecified { get { return AdditionalBaseWarrantyTerm != null; } }

		/// <summary>
		/// Additional Base Warranty Miles
		/// </summary>
		public Int32? AdditionalBaseWarrantyMiles { get; set; } = null;

		public Boolean AdditionalBaseWarrantyMilesSpecified { get { return AdditionalBaseWarrantyMiles != null; } }

		/// <summary>
		/// Additional Powertrain Warranty Term (Months)
		/// </summary>
		public Int16? AdditionalPowertrainWarrantyTerm { get; set; } = null;

		public Boolean AdditionalPowertrainWarrantyTermSpecified { get { return AdditionalPowertrainWarrantyTerm != null; } }

		/// <summary>
		/// Additional Powertrain Warranty Miles
		/// </summary>
		public Int32? AdditionalPowertrainWarrantyMiles { get; set; } = null;

		public Boolean AdditionalPowertrainWarrantyMilesSpecified { get { return AdditionalPowertrainWarrantyMiles != null; } }

		/// <summary>
		/// Ownership of the vehicle
		/// Valid values:
		///		<blank>
		///		‘N’ – New
		///		‘P’ – Pre-Owned
		/// </summary>
		public String Ownership { get; set; } = null;

		/// <summary>
		/// May be required for GAP and Extended Wear and Tear rates • (Note: Some Providers may use these fields for other products besides GAP and EWT)
		/// </summary>
		public Decimal? NADA { get; set; } = null;

		public Boolean NADASpecified { get { return NADA != null; } }

		/// <summary>
		/// SCS Lienholder Number
		/// </summary>
		public String LienNumber { get; set; } = null;

		/// <summary>
		/// "ProductCollection" object (collection of product objects)
		/// </summary>
		public List<Product> ProductCollection { get; set; } = null;

		/// <summary>
		/// Filter object
		/// </summary>
		public Filter Filter { get; set; } = null;
		#endregion
	}
}
