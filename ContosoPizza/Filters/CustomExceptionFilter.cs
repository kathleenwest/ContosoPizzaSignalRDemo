using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContosoPizza.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {

        public CustomExceptionFilter()
        {

        }

        public override void OnException(ExceptionContext context)
        {
            Console.WriteLine($"{context.Exception} 💩 An unexpected error occurred. Please try again later.");

            if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                context.Result = new ObjectResult("💩 An unexpected error occurred. Please try again later.")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
