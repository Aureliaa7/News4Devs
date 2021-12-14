using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using News4Devs.Shared.Exceptions;

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
            else if (context.Exception is FailedHttpRequestException)
            {
                //TODO do smth
                System.Console.WriteLine(context.Exception.Message);
            }
        }
    }
}
