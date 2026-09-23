using System;
using System.ComponentModel;
using ModelContextProtocol.Server;
using leads.contracts;
using MassTransit;

namespace mcp.Tools
{
    [McpServerToolType]
    public static class LeadsTools
    {
        [McpServerTool, Description("Create a new lead in the system")]
        public static async Task<string> CreateLead(
            ISendEndpointProvider sendEndpointProvider,
            [Description("The name of the lead")] string nomeCompleto,
            [Description("The email of the lead")] string email,
            [Description("The phone of the lead")] string telefone,
            [Description("Indicates if the first condition was met.")] bool condicaoUm,
            [Description("Indicates if the secound condition was met.")] bool condicaoDois)
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-lead"));

            var command = new CreateLeadCommand
            {
                NomeCompleto = nomeCompleto,
                Email = email,
                Telefone = telefone,
                CondicaoUm = condicaoUm,
                CondicaoDois = condicaoDois
            };

            await endpoint.Send(command);

            return "Pedido de cadastro enviado com sucesso! O lead será registrado no sistema em instantes.";
        }
    }
}
