using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi;

namespace leads.api.Configurations
{
    public static class SwaggerConfig
    {
        private static readonly string AppName = "Cadastro de Leads";

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", AppName);
                options.RoutePrefix = "swagger";
                options.DocumentTitle = AppName;
            });

            return app;
        }
    }
}
