using System.Xml.Serialization;
using System.Xml;
using AAC.Libraries;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Security.AccessControl;
using System.Web;

namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	public class SCSApiClient
	{
		private readonly HttpClient http;

		public SCSApiClient(HttpClient http)
		{
			this.http = http;
			this.http.BaseAddress = new Uri(SelfServiceVSC.AppSettings.SCSAPIBaseURL);
			this.http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/xml"));
			this.http.Timeout = TimeSpan.FromSeconds(100);
		}

		public async Task<GetRatesResponse> GetRates(GetRates getRatesRequest)
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
			var json1 = JsonSerializer.Serialize(getRatesRequest, typeof(GetRates), options);

			var returnEnvelope = await Send(
				new SoapEnvelope.Envelope
				{
					Body = new SoapEnvelope.Body
					{
						GetRates = new SoapEnvelope.GetRatesWrapper
						{
							GetRatesRequest = getRatesRequest
						}
					}
				});
		

			var json0 = JsonSerializer.Serialize(returnEnvelope?.Body.GetRatesResponse.GetRatesResponse, typeof(GetRatesResponse), options);

			return returnEnvelope?.Body.GetRatesResponse.GetRatesResponse;
		}

		public async Task<GenerateContractResponse> GenerateContract(GenerateContract generateContract)
		{
			await LogRequestAsync(generateContract);
			var returnEnvelope = await Send(
				new SoapEnvelope.Envelope
				{
					Body = new SoapEnvelope.Body
					{
						GenerateContract = new SoapEnvelope.GenerateContractWrapper
						{
							GenerateContract = generateContract
						}
					}
				});
			await LogResponseAsync(returnEnvelope?.Body.GenerateContractResponse.GenerateContractResponse);
			Thread.Sleep(5000);
			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = null,
				Converters =
				{
					new JsonStringEnumConverter()
				},
				//Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};

			var json0 = JsonSerializer.Serialize(returnEnvelope?.Body.GenerateContractResponse.GenerateContractResponse, typeof(GenerateContractResponse), options);
			return returnEnvelope?.Body.GenerateContractResponse.GenerateContractResponse;
		}
		

		private async Task LogRequestAsync(GenerateContract generateContract)
		{
			//string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogFilePath);
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");
			using (StreamWriter sw = new StreamWriter(path, true))
			{
				await sw.WriteLineAsync($"{timestamp} -Request: {JsonSerializer.Serialize(generateContract)}");
			}
		}

		private async Task LogResponseAsync(GenerateContractResponse response)
		{
			//string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogFilePath);
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");
			using (StreamWriter sw = new StreamWriter(path, true))
			{
				await sw.WriteLineAsync($"{timestamp} -Response: {JsonSerializer.Serialize(response)}");
			}
		}

			public async Task<VoidContractResponse> VoidContract(VoidContract voidContract)
		{
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");
			using (StreamWriter sw = new StreamWriter(path, true))
			{
				await sw.WriteLineAsync($"{timestamp} -Void contract Request: {JsonSerializer.Serialize(voidContract)}");
			}

			var returnEnvelope = await Send(
				new SoapEnvelope.Envelope
				{
					Body = new SoapEnvelope.Body
					{
						VoidContract = new SoapEnvelope.VoidContractWrapper
						{
							VoidContract = voidContract
						}
					}
				});

			return returnEnvelope?.Body.VoidContractResponse.VoidContractResponse;
		}

		private async Task<SoapEnvelope.Envelope> Send(SoapEnvelope.Envelope envelope)
		{
			string xmlRequest;
			var serializer = new XmlSerializer(typeof(SoapEnvelope.Envelope));
			var outStream = new MemoryStream();
			
				// Serialize the envelope object to XML
				using (var xmlWriter = XmlWriter.Create(outStream, new XmlWriterSettings { Indent = true, NewLineChars = "\n" }))
				{
					serializer.Serialize(xmlWriter, envelope);
				}

				// Convert the byte array to a UTF-8 string
				var bytes = outStream.ToArray();
				xmlRequest = Encoding.UTF8.GetString(bytes);

				// Decode the HTML-encoded characters
				string decodedXml = HttpUtility.HtmlDecode(xmlRequest);
				decodedXml = decodedXml.TrimStart('\uFEFF');  // Remove BOM if present

				// Prepare the log entry
				string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");

				// Log the decoded XML to the log file
				using (StreamWriter sw = new StreamWriter(path, true))
				{
					await sw.WriteLineAsync($"{timestamp} - XmlRequest: {JsonSerializer.Serialize(decodedXml)}");
				}
				using var message = new HttpRequestMessage(HttpMethod.Post, envelope.URL)
			{
				Content = new ByteArrayContent(bytes)
				{
					Headers =
					{
						ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml")
					}
				},
				Headers =
				{
					{ "SOAPAction", envelope.SOAPAction }
				}
			};
			// Contrct service is not working
			using var response = await http.SendAsync(message);


			// the api actually seems to always return success
			// so the caller must check the responseObject.ErrorResponse property
			// which is a little awkward
			if (response.IsSuccessStatusCode)
			{
				var dataResponse = await response.Content.ReadAsStreamAsync();



				return (SoapEnvelope.Envelope)serializer.Deserialize(dataResponse);
			}
			/*else
			{
				Logger.WriteLine("SCS request (" + envelope.URL + ") failed with Status Code " + response.StatusCode.ToString());
			}*/
			string timestamps = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string paths = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "App_Data", "logfile.txt");
			using (StreamWriter sw = new StreamWriter(paths, true))
			{
				await sw.WriteLineAsync($"{timestamps} -Response: {JsonSerializer.Serialize(response)}");
			}
			return null;
		}
	}
}
