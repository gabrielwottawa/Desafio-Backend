using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MotorbikeRental.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApplicationException)
                context.Result = new BadRequestObjectResult(new { error = context.Exception.Message });
            else
                context.Result = new StatusCodeResult(500);

            base.OnException(context);
        }
    }
}