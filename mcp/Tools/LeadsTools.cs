using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using mcp.Entities;
using mcp.HttpFactory;
using mcp.Services;
using ModelContextProtocol.Server;

namespace mcp.Tools
{
    [McpServerToolType]
    public static class LeadsTools
    {
        [McpServerTool, Description("Create a new lead in the system")]
        public static async Task<string> CreateLead(
            LeadsService leadsService,
            [Description("The name of the lead")] string nomeCompleto,
            [Description("The email of the lead")] string email,
            [Description("The phone of the lead")] string telefone,
            [Description("Indicates if the first condition was met.")] bool condicaoUm,
            [Description("Indicates if the secound condition was met.")] bool condicaoDois)
        {
            var request = new Lead
            {
                Name = nomeCompleto,
                Email = email,
                PhoneNumber = telefone,
                ConditionOne = condicaoUm,
                ConditionTwo = condicaoDois
            };

            var (success, content) = await leadsService.CreateLeadAsync(request);

            return success ? content : $"Erro ao criar lead: {content}";
        }
    }
}
