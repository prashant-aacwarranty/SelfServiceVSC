using System.Net.Http.Headers;

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

        public async Task<string> GetConvergeSessionTokenAsync(string firstName, string lastName, decimal? amount)
        {
            try
            {
                // Replace with your actual Converge API credentials and URL
                var merchantId = "0023171";
                var merchantUserId = "apiuser";
                var merchantPin = "A38JI3UAG4UQ1GBKI37REGELSMXRGAE0JKYLPAJZPZ7Y4RYATYYEIG63Q5BYD8A0";
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
    }
}
