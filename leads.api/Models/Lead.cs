using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leads.api.Models
{
    public class Lead
    {
        public int LeadId { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public bool CondicaoUm { get; set; }
        public bool CondicaoDois { get; set; }
    }
}
