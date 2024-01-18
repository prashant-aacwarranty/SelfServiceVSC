using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace AAC.SelfServiceVSC.Services;

// todo(recommendation): ensure that model validation works on all the other model types.
// todo: remove the testing junk.
public sealed class PaymentModel
{
	[Required]
	[MinLength(15)]
	public string CardNumber { get; internal set; } = "4111111111111111";

	[Required]
	public String Expiration { get; internal set; } = "1123";

	[Required]
	public String CardCode { get; internal set; } = "123";

	[Required]
	public String FirstName { get; internal set; } = "Bob";

	[Required]
	public String LastName { get; internal set; } = "Smith";

	[Required]
	public String Address { get; internal set; } = "123 Fake Street";

	[Required]
	public String City { get; internal set; } = "Fakeville";

	[Required]
	public String Zip { get; internal set; } = "77777";
}

public class AuthorizeDotNetOptions
{
	public bool UseSandbox { get; set; }

	[Required]
	public string Merchant { get; set; }

	[Required]
	public string TransactionKey { get; set; }
}

public class AuthorizeDotNet
{
	private readonly HttpClient http;
	private readonly AuthorizeDotNetOptions options;
	private readonly String url;
	private readonly MerchantAuthentication merchantAuth;
	private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	};

	public AuthorizeDotNet(HttpClient client, IOptions<AuthorizeDotNetOptions> config)
	{
		this.http = client;
		this.options = config.Value ?? throw new ArgumentNullException("config");

		this.url = this.options.UseSandbox
			? "https://apitest.authorize.net/xml/v1/request.api"
			: "https://api.authorize.net/xml/v1/request.api";

		if (string.IsNullOrWhiteSpace(this.options.Merchant))
		{
			throw new ApplicationException("invalid AuthorizeDotNet merchant");
		}

		if (string.IsNullOrWhiteSpace(this.options.TransactionKey))
		{
			throw new ApplicationException("invalid AuthorizeDotNet TransactionKey");
		}

		this.merchantAuth = new MerchantAuthentication(
			options.Merchant,
			options.TransactionKey
		);
	}

	public async Task<CreateTransactionResponse> ChargeCard(PaymentModel model, decimal amount, string contractId, string description, string clientIp)
	{
		const string TRAN_TYPE_CAPTURE = "authCaptureTransaction";

		var request = new CreateTransactionRequestWrapper
		{
			CreateTransactionRequest = new CreateTransactionRequest
			{
				MerchantAuthentication = this.merchantAuth,
				TransactionRequest = new TransactionRequest
				{
					CustomerIP = clientIp,
					TransactionType = TRAN_TYPE_CAPTURE,
					Amount = amount,
					Payment = new Payment
					{
						CreditCard = new CreditCard
						{
							CardNumber = model.CardNumber,
							ExpirationDate = model.Expiration,
							CardCode = model.CardCode
						}
					},
					BillTo = new CustomerAddress
					{
						FirstName = model.FirstName,
						LastName = model.LastName,
						Address = model.Address,
						City = model.City,
						Zip = model.Zip
					}
				}
			}
		};

		using var req = new HttpRequestMessage(HttpMethod.Post, this.url)
		{
			Content = JsonContent.Create(request, options: this.jsonOptions),
		};

		using var res = await this.http.SendAsync(req);

		// todo: handle the errors here or just bubble them up?
		return await res.EnsureSuccessStatusCode()
			.Content.ReadFromJsonAsync<CreateTransactionResponse>();
	}
}

// the top-level element needs the name 'createTransactionRequest'
internal class CreateTransactionRequestWrapper
{
	public CreateTransactionRequest CreateTransactionRequest { get; set; }
}


// todo: tighten this up by a lot
public class CreateTransactionResponse
{
	// this is maybe not populated, if it fails, bad times
	public JsonNode TransactionResponse { get; set; }

	// don't ask me why this is plural, their api is odd
	public ResponseMessage Messages { get; set; }
}

public class ResponseMessage
{
	public string ResultCode { get; set; }

	// array of { code: "abc123", text: "something something" }
	public JsonNode[] Message { get; set; }
}

internal class CreateTransactionRequest
{
	[JsonPropertyOrder(0)]
	public MerchantAuthentication MerchantAuthentication { get; set; }

	[JsonPropertyOrder(1)]
	public TransactionRequest TransactionRequest { get; set; }
}

internal class TransactionRequest
{
	public String TransactionType { get; set; }
	public Decimal Amount { get; set; }
	public Payment Payment { get; set; }
	public CustomerAddress BillTo { get; set; }
	public String CustomerIP { get; set; }
}

internal class Payment
{
	public CreditCard CreditCard { get; set; }
}

internal class CreditCard
{
	public String CardNumber { get; set; }
	public String ExpirationDate { get; set; }
	public String CardCode { get; set; }
}

internal class CustomerAddress
{
	public String FirstName { get; set; }
	public String LastName { get; set; }
	public String Address { get; set; }
	public String City { get; set; }
	public String Zip { get; set; }
}

// unused
// internal class LineItem
// {
// 	public String ItemId { get; set; }
// 	public String Name { get; set; }
// 	public String Description { get; set; }
// 	public Int32 Quantity { get; set; }
// 	public Decimal UnitPrice { get; set; }
// }

internal class MerchantAuthentication
{
	public String Name { get; set; }
	public String TransactionKey { get; set; }

	public MerchantAuthentication(String merchant, String transactionKey)
	{
		this.Name = merchant;
		this.TransactionKey = transactionKey;
	}
}
