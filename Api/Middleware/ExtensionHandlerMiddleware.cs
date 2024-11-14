using Api.Http.Models;
using System.Net;
using System.Text.Json;

namespace Api.Middleware
{
    public class ExtensionHandlerMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize(new BadResponseModel { Message = ex.Message });

                await response.WriteAsync(result);
            }
        }
    }
}
