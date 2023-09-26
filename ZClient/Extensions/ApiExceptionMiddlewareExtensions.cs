using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace APICatalogo.Extensions;

public static class ApiExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        //UseExceptionHandler define a utilização do middleware de tratamento de exceções que é chamado globalmente quando dispara exceção
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                //contextFeature irá receber informações dos detalhes do erro
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                //TODO: fazer tratamento de exceções no client
            });
        });
    }
}
