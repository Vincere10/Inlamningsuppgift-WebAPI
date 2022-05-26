using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Inlamningsuppgift.Filter
{
    public class UseApiKeyAttribute : Attribute, IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var apiKey = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>().GetValue<string>("ApiKey");

            if (!context.HttpContext.Request.Query.TryGetValue("key", out var key))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!apiKey.Equals(key))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
