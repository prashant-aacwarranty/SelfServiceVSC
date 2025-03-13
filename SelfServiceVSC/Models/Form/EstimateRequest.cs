using System.Text.Json.Serialization;

namespace AAC.SelfServiceVSC.Models.Form
{
	// todo: rec make these required
	public class EstimateRequest
	{
		[JsonPropertyName("mileage")]
		public String Mileage { get; set; } = null;

		[JsonIgnore]
		public Int32? MileageInt
		{
			get
			{
				Int32 mileage;
				Int32.TryParse(Mileage, out mileage);

				return mileage == 0 ? null : mileage;
			}
		}

		[JsonIgnore]
		public String NewUsed
		{
			get
			{
				//return MileageInt <= 20000 ? "N" : "U";
				return MileageInt <= 20000 ? "N" : "N";

			}
		}

		[JsonPropertyName("vin")]
		public String VIN { get; set; } = null;

		[JsonPropertyName("make")]
		public String Make { get; set; } = null;

		[JsonPropertyName("model")]
		public String Model { get; set; } = null;

		[JsonPropertyName("trim")]
		public String Trim { get; set; } = null;

		[JsonPropertyName("year")]
		public String Year { get; set; } = null;

		[JsonIgnore]
		public Int16? YearInt
		{
			get
			{
				Int16 year;
				Int16.TryParse(Year, out year);

				return year == 0 ? null : year;
			}
		}
	}
}
