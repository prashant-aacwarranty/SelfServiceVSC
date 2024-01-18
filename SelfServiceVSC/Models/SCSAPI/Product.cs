using System.Xml.Serialization;

namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	/// <summary>
	/// Product object for GetRatesRequests
	/// </summary>
	public class Product
	{
		#region Properties
		/// <summary>
		/// Grouping tag used to allow specifying more than one product in a single request.
		/// </summary>
		[XmlAttribute(AttributeName = "Product")]
		public String ProductGrouping { get; set; } = null;

		/// <summary>
		/// Note: This is a general list of Product Type Codes. Providers can customize the Product Type Codes they use so other codes not listed here may be used in addition to or instead of the codes listed here.
		/// Valid values:
		///		VSC (Vehicle Service Contract);
		///		GAP(GAP);
		///		MNT(Maintenance);
		///		AP(Appearance Protection);
		///		PDR(Paintless Dent Repair);
		///		LTW(Limited Warranty);
		///		RHT(Tire & Wheel);
		///		THP(Theft Protection);
		///		EWT(Extended Wear & Tear);
		///		ETCH(Etch);
		///		KEY(Key Replacement);
		///		OTH(Other Aftermarket Product)
		/// </summary>
		public Int32? Code { get; set; } = null;
		
		public Boolean CodeSpecified { get { return Code != null; } }

		/// <summary>
		/// Not needed
		/// </summary>
		public Int16? Description { get; set; } = null;
		
		public Boolean DescriptionSpecified { get { return Description != null; } }

		/// <summary>
		/// Not needed
		/// </summary>
		public Int32? ExtractPosition { get; set; } = null;
		
		public Boolean ExtractPositionSpecified { get { return ExtractPosition != null; } }
		#endregion
	}
}
