using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using mcp.HttpFactory;
using ModelContextProtocol.Server;

namespace mcp.Tools
{
    [McpServerToolType]
    public static class LeadsTools
    {
        [McpServerTool, Description("Create a new lead in the system")]
        public static async Task<string> CreateLead(
            IHttpClientFactory httpClientFactory,
            [Description("The name of the lead")] string nomeCompleto,
            [Description("The email of the lead")] string email,
            [Description("The phone of the lead")] string telefone,
            [Description("Indicates if the first condition was met.")] bool condicaoUm,
            [Description("Indicates if the secound condition was met.")] bool condicaoDois)
        {
            var client = httpClientFactory.CreateClient(LeadsHttpFactory.ClientName);

            var lead = new
            {
                NomeCompleto = nomeCompleto,
                Email = email,
                Telefone = telefone,
                CondicaoUm = condicaoUm,
                CondicaoDois = condicaoDois
            };

            var response = await client.PostAsJsonAsync("/Leads", lead);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"Erro ao criar lead: {content}";
            }

            return content;
        }
    }
}
