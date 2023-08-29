using Microsoft.AspNetCore.Mvc.Filters;
using AuthKeyApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthKeyApp.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "Api-Key";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key was not provided"
                };
                return;
            }

            var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

            // var user = dbContext.Users.SingleOrDefault(u => u.ApiKeys.Any(k => k.PublicKey == extractedApiKey));

            var extractedApiKeyString = extractedApiKey.ToString();
            var user = dbContext.Users.SingleOrDefault(u => u.ApiKeys.Any(k => k.PublicKey == extractedApiKeyString));

            Console.WriteLine($"extractedApiKeyString: {extractedApiKeyString}");
            Console.WriteLine($"user: {user}");

            if (user == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key is not valid"
                };
                return;
            }

            await next();
        }
    }
}