using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public static class API
	{
		public static String PartnerCredentials
		{
			get
			{
				return SelfServiceVSC.AppSettings.PaylinkPartnerCredentials;
			}
		}

		public static async Task<SubmitContractResponse> SubmitContract(
			Contract contract)
		{
			return await Send<SubmitContractResponse, Contract>(contract, "SubmitContract/?format=json", HttpMethod.Post);
		}

		public static async Task<SellerAdminRelationshipResponse> SellerAdminRelationship()
		{
			return await Send<SellerAdminRelationshipResponse, Authentication>(new Authentication(), "api/SellerAdminRelationship/?format=json", HttpMethod.Patch);
		}

		private static async Task<TResponse> Send<TResponse, TRequest>(
			TRequest obj,
			String url,
			HttpMethod httpMethod)
		{
			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = null,
				Converters =
				{
					new JsonStringEnumConverter()
				},
				//Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var json = JsonSerializer.Serialize(obj, typeof(TRequest), options);

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(SelfServiceVSC.AppSettings.PaylinkAPIBaseURL);
				client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
				client.Timeout = TimeSpan.FromSeconds(300);

				var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

				var message = new HttpRequestMessage(httpMethod, url);
				message.Content = httpContent;

				HttpResponseMessage httpRequest = null;

				httpRequest = await client.SendAsync(message);

				String stringResponse = null;
				TResponse responseObject = default;

				try
				{
					stringResponse = httpRequest.Content.ReadAsStringAsync().Result;
					responseObject = JsonSerializer.Deserialize<TResponse>(stringResponse);
				}
				catch (Exception ex)
				{
					Logger.WriteError(ex);
					responseObject = default;
				}

				if (!httpRequest.IsSuccessStatusCode)
				{
					switch (httpRequest.StatusCode)
					{
						case System.Net.HttpStatusCode.BadRequest:
							Logger.WriteLine("Service connection failed: Bad URL request");
							break;
						case System.Net.HttpStatusCode.Unauthorized:
							Logger.WriteLine("Service connection failed: API authentication rejected");
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
