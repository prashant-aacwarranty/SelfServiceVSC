namespace AAC.SelfServiceVSC.Models.GoogleReview
{
	public class PlaceResult
	{

		public double Rating { get; set; }
		public List<Review> Reviews { get; set; }
		public int User_Ratings_Total { get; set; }

	}
}
