using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using mcp.Services;
using ModelContextProtocol.Server;

namespace mcp.Prompts
{
    [McpServerPromptType]
    public static class DynamicPrompts
    {
        [McpServerPrompt(Name = "analyze-leadcount"),
        Description("Analyze how many leads we got this season")]
        public static async Task<string> AnalyzeLeadsCount(
            LeadsService leadsService)
        {
            var leadsCount = await leadsService.LeadsCountAsync();

            return $@"You are a helpful assistant that tells the user how many leads the API have, and you know the number of total number is: {leadsCount}.

            Provide:
            1.The concrete number of leads.
            2.One random tip to improve engagement.
            3.A motivational message";
        }
    }
}
