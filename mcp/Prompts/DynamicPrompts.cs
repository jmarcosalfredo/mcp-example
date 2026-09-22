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

        [McpServerPrompt(Name = "seller-analyzer"),
        Description("Use the LeadsTools to register new leads, analyzing if the user met with the two contitions")]
        public static async Task<string> SellerPrompt(
            LeadsService leadsService)
        {
            return $@"You are a lead evaluator; you need to gather the necessary information from the user to determine if they are a qualified lead and then register them in our database.

            Information to register user in the database:
            1-Name.
            2-Email.
            3-Phone.
            4-ConditionOne: The user must have a debt exceeding five thousand reais.
            5-ConditionTwo: The user must have an income greater than three thousand reais.

            Behavior:
            1- Be polite, provide a friendly welcome message to start the chat, and always try to gather user information in a friendly, non-aggressive manner.
            2- Only register the user at datebase if ConditionOne and ConditioTwo be true.";
        }
    }
}
