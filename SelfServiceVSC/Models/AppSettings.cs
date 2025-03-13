namespace AAC.SelfServiceVSC.Models
{
	/// <summary>
	/// Store application settings for quick access.
	/// </summary>
	internal class AppSettings
	{
		/// <summary>
		/// The title of the application.
		/// </summary>
		internal String ApplicationTitle { get; set; }

		/// <summary>
		/// The title of the application, formatted for display.
		/// </summary>
		internal String ApplicationTitleFormatted { get; set; }

		/// <summary>
		/// The title of the application, shortened for quick references.
		/// </summary>
		internal String ApplicationTitleShort { get; set; }

		/// <summary>
		/// The title of the application, abbreviated for tiny references.
		/// </summary>
		internal String ApplicationTitleAbbreviation { get; set; }

		/// <summary>
		/// The business' main phone number.
		/// </summary>
		internal String PhoneNumberMain { get; set; }

		/// <summary>
		/// Whether the current executing environment is a development environment.
		/// </summary>
		internal Boolean IsDevelopment { get; set; } = false;

		/// <summary>
		/// The current build version of the application.
		/// </summary>
		internal String ApplicationVersion { get; set; }

		/// <summary>
		/// Session timeout in minutes.
		/// </summary>
		internal Int32 IdleTimeout { get; set; }

		internal String SCSAPIBaseURL { get; set; } = @"https://rating.scsautoexpress.com/scs.express.webservice/";

		internal String SCSAPIUserId { get; set; }

		internal String SCSAPIPassword { get; set; }

		internal String Line5APIBaseURL { get; set; } = @"https://www.line5.com/api/v1";

		internal String Line5APIKey { get; set; }

		internal String PaylinkAPIBaseURL { get; set; } = @"https://qa.paylinkdirect.com/contractapi/";

		internal String PaylinkPartnerCredentials { get; set; }

		internal String MailServer { get; set; } = "aacwarranty.com";

		internal Int32 MailPort { get; set; } = 465;

		internal String MailUsername { get; set; } = "programming@aacwarranty.com";

		internal String MailPassword { get; set; }
		internal String FlowrouteUsername { get; set; }
		internal String FlowroutePassword { get; set; }
	}
}
