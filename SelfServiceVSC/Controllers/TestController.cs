using Microsoft.AspNetCore.Mvc;
using AAC.SelfServiceVSC.Services;

namespace SelfServiceVSC.Controllers;

#if DEBUG

public class TestController : Controller
{
	private readonly AuthorizeDotNet authorizeDotNet;

	public TestController(AuthorizeDotNet authorizeDotNet)
	{
		this.authorizeDotNet = authorizeDotNet;
	}

	// todo: removeme or just compile me out before release
	[HttpPost("/test/pay")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> TestCard(PaymentModel payment)
	{
		var ip = this.HttpContext.Connection.LocalIpAddress.ToString();
		var res = await authorizeDotNet.ChargeCard(payment, 100, "ABC123", "this is a test", ip);

		return Ok(res);
	}
}

#endif
