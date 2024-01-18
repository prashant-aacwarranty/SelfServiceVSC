using Microsoft.AspNetCore.Mvc.Filters;

namespace AAC.SelfServiceVSC.Models.Filters
{
	// todo(recommendation): remove dead code
	public class ActionFilter : IActionFilter
	{
		public void OnActionExecuting(
			ActionExecutingContext context)
		{
			var controller = context.Controller.GetType();
			var action = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).MethodInfo;


		}

		public void OnActionExecuted(
			ActionExecutedContext context)
		{

		}
	}
}
