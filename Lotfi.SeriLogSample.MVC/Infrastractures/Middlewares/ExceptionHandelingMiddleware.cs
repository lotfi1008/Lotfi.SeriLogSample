using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Lotfi.SeriLogSample.MVC.Infrastractures.Middlewares
{
    public class ExceptionHandelingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandelingMiddleware> logger;

        public ExceptionHandelingMiddleware(RequestDelegate next,ILogger<ExceptionHandelingMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception ex)
            {

                HadleException(httpContext,ex);
            }
        }

        private async Task HadleException(HttpContext httpContext, Exception ex)
        {
            logger.LogError("LocalIpAddress:"+httpContext.Connection.LocalIpAddress+"| LocalPort:"+ httpContext.Connection.LocalPort);
            logger.LogError("RemoteIpAddress:" + httpContext.Connection.RemoteIpAddress+ "| RemotePort:" + httpContext.Connection.RemotePort);
            logger.LogError("HttpMethod:",httpContext.Request.Method.ToString());
            logger.LogError("HttpPath:",httpContext.Request.Path.Value);
            logger.LogError("QueryString:", httpContext.Request.QueryString.Value);
            logger.LogError("Scheme:", httpContext.Request.Scheme);
            logger.LogError("Name:", httpContext.User?.Identity?.Name);
            
            logger.LogError("Message :"+ex.Message);

            var response = httpContext.Response;
            var customeException = ex as BaseCustomException;
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Enexpected Error";

            var description = "Enexpected Error";


            if (customeException!=null)
            {
                message = customeException.Message;
                description = customeException.Description;
                statusCode = customeException.Code;
            }

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(new CustomeErrorResponse
            {
                Message=message,
                Description=description
            }));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandelingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandelingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandelingMiddleware>();
        }
    }
}
