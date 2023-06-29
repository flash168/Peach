using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Peach.Application.Configuration;

namespace Peach.Host.Filters
{
    public class VodValidationFilter: IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var apiPwd = AppSettingsHelper.GetContent<string>("AppConfig", "ApiPwd");

            // 在执行控制器函数之前的逻辑处理
            if (!context.ModelState.IsValid)
            {
               
                // 模型验证失败，返回错误响应
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // 在执行控制器函数之后的逻辑处理
        }


    }
}
