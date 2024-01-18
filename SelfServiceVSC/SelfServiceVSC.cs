using AAC.SelfServiceVSC.Models;

namespace AAC.SelfServiceVSC
{
	public class SelfServiceVSC
	{
		/// <summary>
		/// Store application settings for quick access.
		/// </summary>
		internal static AppSettings AppSettings { get; set; } = new AppSettings();

		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Configuration
				.AddEnvironmentVariables()
				.AddUserSecrets(typeof(SelfServiceVSC).Assembly);

			Startup.ConfigureServices(builder.Services, builder.Configuration);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseHsts();
			}
			else
			{
				AppSettings.IsDevelopment = true;
			}

			AppSettings.ApplicationVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseSession();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			// todo(rec): secure defaults, also, no CORS because we're on the same site not crossing origins
			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			app.UseMiddleware<ErrorHandlerMiddleware>();
			app.UseRouting();
			app.UseEndpoints(x => x.MapControllers());

			app.Run();
		}
	}
}
