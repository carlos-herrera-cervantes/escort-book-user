using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EscortBookUser.Attributes
{
    public class ImageGuardAttribute : TypeFilterAttribute
    {
        public ImageGuardAttribute() : base(typeof(ImageGuardFilter)) {}

        private class ImageGuardFilter : IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.HttpContext.Request.HasFormContentType)
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var image = context.HttpContext.Request.Form.Files.FirstOrDefault(k => k.Name == "image");

                if (image is null)
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                await next();
            }
        }
    }
}
