using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Peach.Domain;
using Peach.Host.Views;

namespace Peach.Host.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var ex = context.Exception;

            _logger.LogError("Path {Path} message {Exception}", context.HttpContext.Request.Path, context.Exception);

            if (ex is BusinessException exception)
            {
                context.Result = new OkObjectResult(new ResponseView(exception.Code, exception.Message));
            }
            else
            {
                context.Result = new OkObjectResult(new ResponseView(500, ex.Message));
            }

            context.ExceptionHandled = true;


            return Task.CompletedTask;
        }
    }
}