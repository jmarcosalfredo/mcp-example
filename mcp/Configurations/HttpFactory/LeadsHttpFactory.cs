using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace mcp.HttpFactory
{
    public static class LeadsHttpFactory
    {
        public const string ClientName = "LeadsHttp";
        public static IServiceCollection AddLeadsHttpFactory(this IServiceCollection services)
        {
            services.AddHttpClient(ClientName, client =>
            {
                client.BaseAddress = new Uri("http://localhost:5006");
            });

            return services;
        }
    }
}
