using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Peach.Host.Views;

namespace Peach.Host.Filters
{

    public class ResponseFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result != null)
            {
                if (context.Result is ObjectResult)
                {
                    ObjectResult? objectResult = context.Result as ObjectResult;
                    if (objectResult?.Value?.GetType().Name == nameof(ResponseView))
                    {
                        var result = objectResult.Value as ResponseView;

                        context.Result = new ObjectResult(result);
                    }
                    else
                    {
                        context.Result = new ObjectResult(new ResponseView(200, data: objectResult?.Value));
                    }
                }
                else if (context.Result is EmptyResult)
                {
                    context.Result = new ObjectResult(new ResponseView(200));
                }
                else if (context.Result is ResponseView modelStateResult2)
                {
                    context.Result = new ObjectResult(modelStateResult2);
                }
            }
            else
            {
                context.Result = new ObjectResult(new ResponseView(200));
            }

            base.OnActionExecuted(context);
        }
    }
}
