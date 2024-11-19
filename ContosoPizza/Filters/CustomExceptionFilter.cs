using System.Net;
using System.Web.Http.Filters;
using ExceptionFilterAttribute = System.Web.Http.Filters.ExceptionFilterAttribute;

namespace ContosoPizza.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            // Log the exception (you can use any logging framework here)
            // For simplicity, we're using Console.WriteLine
            Console.WriteLine(context.Exception);

            // Create a custom response
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("💩 An unexpected error occurred. Please try again later."),
                ReasonPhrase = "💩"
            };

            context.Response = response;
        }
    }

}
