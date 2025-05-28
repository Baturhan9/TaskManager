using Microsoft.AspNetCore.Diagnostics;
using TaskManager.Exceptions;
using TaskManager.Models.ErrorModels;

namespace TaskManager.Api.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void UseCustomSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.OAuthClientId("swagger-ui");
                c.OAuthAppName("Swagger UI");
            });
        }

        public static void UseExceptionHandler(this WebApplication app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeatures != null)
                    {
                        context.Response.StatusCode = contextFeatures.Error switch
                        {
                            BadRequestException => StatusCodes.Status400BadRequest,
                            NotFoundException => StatusCodes.Status404NotFound,
                            ForbiddenException => StatusCodes.Status403Forbidden,
                            _ => StatusCodes.Status500InternalServerError,
                        };

                        logger.LogError($"Smth went wrong {contextFeatures.Error.ToString()}");

                        await context.Response.WriteAsync(
                            new ErrorDetails()
                            {
                                message = contextFeatures.Error.Message,
                                statusCode = context.Response.StatusCode,
                            }.ToString()
                        );
                    }
                });
            });
        }
    }
}
