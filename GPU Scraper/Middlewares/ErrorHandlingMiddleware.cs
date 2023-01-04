using GPU_Scraper.Middlewares.Exceptions;

namespace GPU_Scraper.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next.Invoke(context);
			}
            catch (BadRequestException badRequestException)
            {

                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (NotFoundException notFoundException)
			{

				context.Response.StatusCode = 404;
				await context.Response.WriteAsync(notFoundException.Message);
			}
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Coś poszło nie tak. Spróbuj ponownie później");
            }
        }
    }
}
