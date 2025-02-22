using Ebret4m4n.Entities.ErrorModels;
using Ebret4m4n.Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Ebret4m4n.API.Extenstions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (context is not null)
                    {
                        context.Response.StatusCode = contextFeature?.Error switch
                        {
                            BadRequestException => StatusCodes.Status400BadRequest,
                            NotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                            
                        };

                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            Message = contextFeature?.Error.Message,
                            StatusCode = context.Response.StatusCode
                        }.ToString());
                    }
                });
            });
        }

    }
}
