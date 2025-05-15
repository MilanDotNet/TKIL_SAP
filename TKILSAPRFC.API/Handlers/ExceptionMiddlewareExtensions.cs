using Microsoft.AspNetCore.Diagnostics;
using TKILSAPRFC.Core.Helpers;
using System.Net;

namespace TKILSAPRFC.API.Handlers
{
    public static  class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ApiResponse()
                        {
                            Status = HttpStatusCode.InternalServerError,
                            Errors = new List<string> { contextFeature.Error.Message }
                        }.ToString());
                    }
                });
            });
        }
    }
}
