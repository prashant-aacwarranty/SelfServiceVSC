using System.Xml.Serialization;
using System.Xml;
using AAC.Libraries;

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
			this.http.Timeout = TimeSpan.FromSeconds(50);
		}

		public async Task<GetRatesResponse> GetRates(GetRates getRatesRequest)
		{
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

			return returnEnvelope?.Body.GetRatesResponse.GetRatesResponse;
		}

		public async Task<GenerateContractResponse> GenerateContract(GenerateContract generateContract)
		{
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

			return returnEnvelope?.Body.GenerateContractResponse.GenerateContractResponse;
		}

		public async Task<VoidContractResponse> VoidContract(VoidContract voidContract)
		{
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
			var serializer = new XmlSerializer(typeof(SoapEnvelope.Envelope));
			var outStream = new MemoryStream();
			using (var xmlWriter = XmlWriter.Create(outStream, new XmlWriterSettings { Indent = false, NewLineChars = "" }))
			{
				serializer.Serialize(xmlWriter, envelope);
			}

			var bytes = outStream.ToArray();

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
			else
			{
				Logger.WriteLine("SCS request (" + envelope.URL + ") failed with Status Code " + response.StatusCode.ToString());
			}

			return null;
		}
	}
}
