using Microsoft.AspNetCore.Http;
using MISA.HCT.ETax.Api.Properties;
using MISA.HCT.ETax.ApplicationCore.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static MISA.HCT.ETax.ApplicationCore.Enums.Enumeration;
using MISA.HCT.ETax.ApplicationCore;
using Microsoft.Extensions.Logging;

namespace MISA.HCT.ETax.Api.Middleware
{
    /// <summary>
    /// Xử lý khi có exception xảy ra
    /// </summary>
    /// CreatedBy: NVMANH (05/2020)
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        /// <summary>
        /// ErrorHandle
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            this.next = next;
        }
        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(
                new ActionServiceResult
                {
                    Success = false,
                    Message = ApplicationCore.Properties.Resources.Exception,
                    MISACode = MISACode.Exception,
                    Data = ex
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            logger.LogError("Exception : {result}", result);
            return context.Response.WriteAsync(result);
        }
    }
}
