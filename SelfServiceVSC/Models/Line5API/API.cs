using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.Line5API
{
	public static class API
	{
		public static String APIKey
		{
			get
			{
				return SelfServiceVSC.AppSettings.Line5APIKey;
			}
		}

		public static async Task<UsersGetResponse> UsersGet()
		{
			return await Send<UsersGetResponse, String>("", HttpMethod.GET, "users");
		}

		public static async Task<ProtectionsResponse> ProtectionsGet()
		{
			return await Send<ProtectionsResponse, String>("", HttpMethod.GET, "protections");
		}

		public static async Task<QuotePostResponse> QuotePost(
			QuotePost quotePost)
		{
			return await Send<QuotePostResponse, QuotePost>(quotePost, HttpMethod.POST, "quotes");
		}

		public static async Task<QuotePostResponse> QuotePatch(
			QuotePatch quotePatch)
		{
			return await Send<QuotePostResponse, QuotePatch>(quotePatch, HttpMethod.PATCH, "quotes");
		}

		public static async Task<QuoteFinalizePatchResponse> QuotePatchFinalize(
			QuoteFinalizePatch quotePatchFinalize)
		{
			return await Send<QuoteFinalizePatchResponse, QuoteFinalizePatch>(quotePatchFinalize, HttpMethod.PATCH, "quotes/finalize");
		}

		public static async Task<Boolean> VehiclesPatch(
			VehiclesPatch vehiclesPatch)
		{
			return await Send<Boolean, VehiclesPatch>(vehiclesPatch, HttpMethod.PATCH, "vehicles");
		}

		public static async Task<RatesGetResponse> RatesGet(
			RatesGet ratesGet)
		{
			return await Send<RatesGetResponse, RatesGet>(ratesGet, HttpMethod.GET, "rates");
		}

		public static async Task<QuoteProtectionsGetResponse> QuoteProtectionsGet(
			QuoteProtectionsGet quoteProtectionsGet)
		{
			return await Send<QuoteProtectionsGetResponse, QuoteProtectionsGet>(quoteProtectionsGet, HttpMethod.GET, "quote_protections");
		}

		public static async Task<QuoteProtectionPostResponse> QuoteProtectionPost(
			QuoteProtectionPost quoteProtectionPost)
		{
			return await Send<QuoteProtectionPostResponse, QuoteProtectionPost>(quoteProtectionPost, HttpMethod.POST, "quote-protections");
		}

		public static async Task<QuoteProtectionPatchResponse> QuoteProtectionPatch(
			QuoteProtectionPatch quoteProtectionPatch)
		{
			return await Send<QuoteProtectionPatchResponse, QuoteProtectionPatch>(quoteProtectionPatch, HttpMethod.PATCH, "quote-protections");
		}

		public static async Task<Boolean> QuoteProtectionRevisePatch(
			QuoteProtectionRevisePatch quoteProtectionRevisePatch)
		{
			return await Send<Boolean, QuoteProtectionRevisePatch>(quoteProtectionRevisePatch, HttpMethod.PATCH, "quote-protections/" + quoteProtectionRevisePatch.Data.Id.ToString());
		}

		public static async Task<Boolean> QuoteProtectionDelete(
			Int64? quoteProtectionId)
		{
			return await Send<Boolean, String>("", HttpMethod.DELETE, "quote-protections/" + quoteProtectionId.ToString());
		}

		public static async Task<LoanGetResponse> LoanGet(
			Int64? loanId)
		{
			return await Send<LoanGetResponse, String>("", HttpMethod.GET, "loans/" + loanId.ToString());
		}

		public static async Task<LoanPackagesGetResponse> LoanPackagesGet(
			Int64? loanId)
		{
			return await Send<LoanPackagesGetResponse, String>("", HttpMethod.GET, "loans/" + loanId.ToString() + "/loan_packagesf");
		}

		public static async Task<LoanNotesGetResponse> LoanNotesGet(
			Int64? loanId)
		{
			return await Send<LoanNotesGetResponse, String>("", HttpMethod.GET, "loans/" + loanId.ToString() + "/loan_notes");
		}

		public static async Task<Byte[]> DownloadPackage(
			Int64? loanId = null,
			Int64? packageId = null,
			String url = null)
		{
			loanId ??= Int64.Parse(Regex.Match(url, @"(?<=https?://[a-z\.\d]+/api/v1/loans/)\d+").Value);
			packageId ??= Int64.Parse(Regex.Match(url, @"(?<=https?://[a-z\.\d]+/api/v1/loans/\d+/loan_packages/)\d+").Value);

			return await Send<Byte[], String>("", HttpMethod.GET, "loans/" + loanId.ToString() + "/loan_packages/" + packageId.ToString() + "/download");
		}

		private enum HttpMethod : UInt32
		{
			None = 0,
			GET = 1 << 0,
			POST = 1 << 1,
			PUT = 1 << 2,
			PATCH = 1 << 3,
			DELETE = 1 << 4
		}

		private static System.Net.Http.HttpMethod GetMethod(
			HttpMethod method)
		{
			switch (method)
			{
				case HttpMethod.GET:
					return System.Net.Http.HttpMethod.Get;
				case HttpMethod.POST:
					return System.Net.Http.HttpMethod.Post;
				case HttpMethod.PUT:
					return System.Net.Http.HttpMethod.Put;
				case HttpMethod.PATCH:
					return System.Net.Http.HttpMethod.Patch;
				case HttpMethod.DELETE:
					return System.Net.Http.HttpMethod.Delete;
				default:
					return null;
			}
		}

		private static async Task<TResponse> Send<TResponse, TRequest>(
			TRequest obj,
			HttpMethod method,
			String url)
		{
			var json = JsonSerializer.Serialize(obj);

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(SelfServiceVSC.AppSettings.Line5APIBaseURL);
				client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.api+json"));
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", APIKey);
				client.Timeout = TimeSpan.FromSeconds(300);

				var httpContent = new StringContent(json, Encoding.UTF8, "application/vnd.api+json");

				var message = new HttpRequestMessage(GetMethod(method), url);
				message.Content = httpContent;

				HttpResponseMessage httpRequest = null;

				httpRequest = await client.SendAsync(message);

				String stringResponse = null;
				Byte[] byteResponse = null;
				TResponse responseObject = default(TResponse);

				try
				{
					switch (typeof(TResponse).Name)
					{
						case "Boolean":
							responseObject = (TResponse)TypeDescriptor.GetConverter(typeof(TResponse)).ConvertFrom(httpRequest.IsSuccessStatusCode);
							break;
						case "String":
							stringResponse = httpRequest.Content.ReadAsStringAsync().Result;
							responseObject = (TResponse)(Object)stringResponse;
							break;
						case "Byte[]":
							byteResponse = httpRequest.Content.ReadAsByteArrayAsync().Result;
							responseObject = (TResponse)(Object)byteResponse;
							break;
						default:
							stringResponse = httpRequest.Content.ReadAsStringAsync().Result;
							responseObject = JsonSerializer.Deserialize<TResponse>(stringResponse);
							break;
					}
				}
				catch (Exception ex)
				{
					Logger.WriteError(ex);
					if (typeof(TResponse) != typeof(Boolean) && typeof(TResponse) != typeof(String))
					{
						responseObject = default(TResponse);
					}
					else
					{
						responseObject = (TResponse)TypeDescriptor.GetConverter(typeof(TResponse)).ConvertFrom(httpRequest.IsSuccessStatusCode);
					}
				}

				if (!httpRequest.IsSuccessStatusCode)
				{
					switch (httpRequest.StatusCode)
					{
						case System.Net.HttpStatusCode.BadRequest:
							Logger.WriteLine("Service connection failed: Bad URL request");
							break;
						case System.Net.HttpStatusCode.Unauthorized:
							Logger.WriteLine("Service connection failed: API Key rejected");
							break;
						case System.Net.HttpStatusCode.Forbidden:
							Logger.WriteLine("Service connection failed: Forbidden");
							break;
						case System.Net.HttpStatusCode.NotFound:
							Logger.WriteLine("Service connection failed: 404 not found");
							break;
						case System.Net.HttpStatusCode.UnprocessableEntity:
							Logger.WriteLine("Service connection failed: Invalid data submitted");
							break;
						case System.Net.HttpStatusCode.InternalServerError:
							Logger.WriteLine("Service connection failed: Internal server error");
							break;
						case System.Net.HttpStatusCode.ServiceUnavailable:
							Logger.WriteLine("Service connection failed: Server offline");
							break;
						default:
							Logger.WriteLine("Service connection failed: Status unknown");
							break;
					}

					throw new Exception("Service connection failed: " + httpRequest.StatusCode.ToString());
				}

				return responseObject;
			}
		}
	}
}
