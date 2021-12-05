using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using News4Devs.Core.Exceptions;

namespace News4Devs.WebAPI.Filters
{
    public class News4DevsExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DuplicateEmailException ||
                context.Exception is DuplicateEntityException)
            {
                context.Result = new ConflictResult();
            }
            else if (context.Exception is EntityNotFoundException)
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
