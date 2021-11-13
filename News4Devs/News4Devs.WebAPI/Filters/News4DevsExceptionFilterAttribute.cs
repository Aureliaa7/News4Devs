using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using News4Devs.Core;
using News4Devs.Core.Exceptions;

namespace News4Devs.WebAPI.Filters
{
    public class News4DevsExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DuplicateEmailException)
            {
                var errorDetails = new ExceptionDetails(Constants.Conflict, context.Exception.Message);
                context.Result = new ConflictObjectResult(errorDetails);
            }
            else if (context.Exception is EntityNotFoundException)
            {
                var errorDetails = new ExceptionDetails(Constants.NotFound, context.Exception.Message);
                context.Result = new NotFoundObjectResult(errorDetails);
            }
        }
    }
}
