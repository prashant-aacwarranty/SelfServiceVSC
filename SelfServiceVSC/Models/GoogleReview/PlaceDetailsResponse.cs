using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AAC.SelfServiceVSC.Models.GoogleReview
{
	public class PlaceDetailsResponse
	{
		public List<object> HtmlAttributions { get; set; }
		public PlaceResult Result { get; set; }
		public string Status { get; set; }

	}
}
