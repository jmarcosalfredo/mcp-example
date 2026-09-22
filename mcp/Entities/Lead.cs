using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace mcp.Entities
{
    public class Lead
    {
        [JsonPropertyName("nomeCompleto")]
        public required string Name { get; set; }

        public required string Email { get; set; }

        [JsonPropertyName("telefone")]
        public required string PhoneNumber { get; set; }

        [JsonPropertyName("condicaoUm")]
        public bool ConditionOne { get; set; }

        [JsonPropertyName("condicaoDois")]
        public bool ConditionTwo { get; set; }
    }
}
