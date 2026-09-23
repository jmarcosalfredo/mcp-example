using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leads.contracts
{
    public record CreateLeadCommand
    {
        public required string NomeCompleto { get; init; }
        public required string Email { get; init; }
        public required string Telefone { get; init; }
        public required bool CondicaoUm { get; init; }
        public required bool CondicaoDois { get; init; }
    }

}
