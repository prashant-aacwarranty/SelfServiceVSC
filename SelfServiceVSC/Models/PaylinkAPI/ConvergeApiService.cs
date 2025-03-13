using System.Net.Http.Headers;
using System.Text.Json;

namespace Elavon_Converge.Models
{

	public class ConvergeApiService
	{
		private readonly HttpClient _httpClient;

		public ConvergeApiService()
		{
			_httpClient = new HttpClient();

			// You may configure additional settings for HttpClient here
		}

		public async Task<string> GetConvergeCreditCardSessionTokenAsync(string firstName, string lastName, decimal? amount)
		{
			try
			{
				// Replace with your actual Converge API credentials and URL
				var merchantId = "0023184";
				var merchantUserId = "apiuser";
				var merchantPin = "KEQ0EMBHCYZSVE35LKZS9MUDI8RCRTM3AKUWIQNBLQWWKMAQBSGJO2G2NHD6FYKM";
				var convergeApiUrl = "https://api.demo.convergepay.com/hosted-payments/transaction_token";

				var requestContent = new FormUrlEncodedContent(new Dictionary<string, string>
								{
										{ "ssl_merchant_id", merchantId },
										{ "ssl_user_id", merchantUserId },
										{ "ssl_pin", merchantPin },
										{ "ssl_transaction_type", "ccsale" },
										{ "ssl_first_name", firstName },
										{ "ssl_last_name", lastName },
										{ "ssl_get_token", "Y" },
										{ "ssl_add_token", "Y" },
										{ "ssl_amount", amount.Value.ToString ("0.00") }
								});


				var response = await _httpClient.PostAsync(convergeApiUrl, requestContent);
				await LogResponseAsync(response);
				// Handle the response according to your application's logic
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					return result;
				}
				else
				{
					// Handle non-success status codes, log, or throw an exception as needed
					Console.WriteLine($"Converge API request failed. Status code: {response.StatusCode}");
					return null;
				}
			}
			catch (Exception ex)
			{
				// Handle exceptions appropriately
				Console.WriteLine($"An error occurred: {ex.Message}");
				return null;
			}
		}
		public async Task<string> GetConvergeAchSessionTokenAsync(string firstName, string lastName, decimal? amount)
		{
			try
			{
				// Replace with your actual Converge API credentials and URL
				var merchantId = "0023184";
				var merchantUserId = "apiuser";
				var merchantPin = "KEQ0EMBHCYZSVE35LKZS9MUDI8RCRTM3AKUWIQNBLQWWKMAQBSGJO2G2NHD6FYKM";
				var convergeApiUrl = "https://api.demo.convergepay.com/hosted-payments/transaction_token";

				var requestContent = new FormUrlEncodedContent(new Dictionary<string, string>
								{
										{ "ssl_merchant_id", merchantId },
										{ "ssl_user_id", merchantUserId },
										{ "ssl_pin", merchantPin },
										{ "ssl_transaction_type", "ecspurchase" },
										{ "ssl_amount", amount.Value.ToString ("0.00") }
										//{ "ssl_amount","200.00" }
								});


				var response = await _httpClient.PostAsync(convergeApiUrl, requestContent);
				await LogResponseAsync(response);
				// Handle the response according to your application's logic
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					return result;
				}
				else
				{
					// Handle non-success status codes, log, or throw an exception as needed
					Console.WriteLine($"Converge API request failed. Status code: {response.StatusCode}");
					return null;
				}
			}
			catch (Exception ex)
			{
				// Handle exceptions appropriately
				Console.WriteLine($"An error occurred: {ex.Message}");
				return null;
			}
		}

		private async Task LogRequestAsync(TokenRequestModel generateContract, decimal? sslAmount)
		{
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");

			using (StreamWriter sw = new StreamWriter(path, true))
			{
				await sw.WriteLineAsync($"{timestamp} - Request: {JsonSerializer.Serialize(generateContract)} - SSL Amount: {sslAmount}");
			}
		}


		private async Task LogResponseAsync(HttpResponseMessage response)
		{
			//string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogFilePath);
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");
			using (StreamWriter sw = new StreamWriter(path, true))
			{
				await sw.WriteLineAsync($"{timestamp} -Response: {JsonSerializer.Serialize(response)}");
			}
		}
	}
}
