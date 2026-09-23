using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leads.api.Models;
using leads.api.Services;
using leads.contracts;
using MassTransit;

namespace leads.api.Consumers
{
    public class CreateLeadConsumer : IConsumer<CreateLeadCommand>
    {
        private readonly ILeadService _service;

        public CreateLeadConsumer(ILeadService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<CreateLeadCommand> context)
        {
            var msg = context.Message;

            var lead = new Lead
            {
                NomeCompleto = msg.NomeCompleto,
                Email = msg.Email,
                Telefone = msg.Telefone,
                CondicaoUm = msg.CondicaoUm,
                CondicaoDois = msg.CondicaoDois
            };

            var response = await _service.CreateAsync(lead);

            if (!response.Success)
            {
                throw new InvalidOperationException(response.Message);
            }
        }
    }
}
