namespace AAC.SelfServiceVSC.Models.GoogleReview
{
	public class Review
	{
		public string Author_Name { get; set; }
		public string AuthorUrl { get; set; }
		public string Language { get; set; }
		public string OriginalLanguage { get; set; }
		public string Profile_Photo_Url { get; set; }
		public int Rating { get; set; }
		public string RelativeTimeDescription { get; set; }
		public string Text { get; set; }
		public long Time { get; set; }
		public bool Translated { get; set; }

	}
}
