using AAC.SelfServiceVSC.Models.Filters;
using AAC.SelfServiceVSC.Models.SCSAPI;
using AAC.SelfServiceVSC.Services;

namespace AAC.SelfServiceVSC
{
	public class Startup
	{
		public static void ConfigureServices(IServiceCollection services, IConfigurationRoot config)
		{
			services.AddHsts(strictTransport =>
			{
				strictTransport.IncludeSubDomains = true;
				strictTransport.MaxAge = TimeSpan.FromDays(365);
			});

			services.AddControllers();
			services.AddControllersWithViews();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			var settings = config.GetSection("LocalSettings");

			SelfServiceVSC.AppSettings.IdleTimeout = Int32.Parse(
				settings.GetValue<String>("IdleTimeout"));

			services.AddDistributedMemoryCache();
			services.AddSession(options =>
			{
				// todo(recommend): secure defaults, lower the idle timeout
				options.IdleTimeout = TimeSpan.FromMinutes(SelfServiceVSC.AppSettings.IdleTimeout);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				options.Cookie.SameSite = SameSiteMode.Strict;
			});

			// todo(recommendation): code cleanup, do some Bind IConfiguration things
			SelfServiceVSC.AppSettings.ApplicationTitle = settings.GetValue<String>("ApplicationTitle");
			SelfServiceVSC.AppSettings.ApplicationTitleFormatted = settings.GetValue<String>("ApplicationTitleFormatted");
			SelfServiceVSC.AppSettings.ApplicationTitleShort = settings.GetValue<String>("ApplicationTitleShort");
			SelfServiceVSC.AppSettings.ApplicationTitleAbbreviation = settings.GetValue<String>("ApplicationTitleAbbreviation");
			SelfServiceVSC.AppSettings.PhoneNumberMain = settings.GetValue<String>("PhoneNumberMain");

			SelfServiceVSC.AppSettings.Line5APIBaseURL = settings.GetValue<String>("Line5APIBaseURL");

			var Line5APIKey = settings.GetValue<String>("Line5APIKey");
			SelfServiceVSC.AppSettings.Line5APIKey = String.IsNullOrWhiteSpace(Line5APIKey) ? "" : Environment.GetEnvironmentVariable(Line5APIKey);
			SelfServiceVSC.AppSettings.PaylinkAPIBaseURL = settings.GetValue<String>("PaylinkAPIBaseURL");

			var PaylinkPartnerCredentials = settings.GetValue<String>("PaylinkPartnerCredentials");
			SelfServiceVSC.AppSettings.PaylinkPartnerCredentials = String.IsNullOrWhiteSpace(PaylinkPartnerCredentials)
				? ""
				: Environment.GetEnvironmentVariable(PaylinkPartnerCredentials);

			SelfServiceVSC.AppSettings.MailServer = settings.GetValue<String>("MailServer");

			// todo: check this but I'm pretty sure we don't need to parse ourselves.
			SelfServiceVSC.AppSettings.MailPort = settings.GetValue("MailPort", 465);
			SelfServiceVSC.AppSettings.MailUsername = settings.GetValue<String>("MailUsername");

			var MailPassword = settings.GetValue<String>("MailPassword");

			// todo: get this from Dave and convert to a user secret
			SelfServiceVSC.AppSettings.MailPassword = String.IsNullOrWhiteSpace(MailPassword) ? "" : Environment.GetEnvironmentVariable(MailPassword);

			SelfServiceVSC.AppSettings.SCSAPIBaseURL = config.GetValue("SCS_API_URL", "https://staging.fiadmin.com/scs.webservice/");
			SelfServiceVSC.AppSettings.SCSAPIUserId = config.GetValue("SCS_API_USERNAME", "DTC");

			// this one is set in dotnet user-secrets
			SelfServiceVSC.AppSettings.SCSAPIPassword = config.GetValue<String>("SCS_API_PASSWORD");

			// eagerly validate all these required options
			services.AddOptions<AuthorizeDotNetOptions>()
				.Bind(config.GetSection("AuthorizeDotNet"))
				.ValidateDataAnnotations()
				.ValidateOnStart();

			services.AddHttpClient<AuthorizeDotNet>("authorize-dot-net-api-client");
			services.AddHttpClient<SCSApiClient>("scs-api-client");

			// todo(rec): let's just ditch the nonsense custom logger
			// services.AddLogging(log => log.AddConsole().AddSerilog());
		}
	}
}
