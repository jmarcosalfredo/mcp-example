using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Anthropic;

var transport = new HttpClientTransport(new HttpClientTransportOptions
{
    Endpoint = new Uri("http://localhost:6006/mcp")
});

try
{
    await using var mcpClient = await McpClient.CreateAsync(transport);
    Console.WriteLine("Client successfully connected to server.");

    var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

    var apiKey = config["Anthropic:ApiKey"]
    ?? throw new InvalidOperationException("Key 'Anthropic:ApiKey' not found! Anthropic API Key must be in user-secrets.");

    Environment.SetEnvironmentVariable("ANTHROPIC_API_KEY", apiKey);

    var mapTools = await mcpClient.ListToolsAsync();
    foreach (var tool in mapTools)
    {
        Console.WriteLine($"- {tool.Name}: {tool.Description}");
    }

    var mapResources = await mcpClient.ListResourcesAsync();
    Console.WriteLine($"\nAvaible Resources: {mapResources}");
    foreach (var resource in mapResources)
    {
        Console.WriteLine($"- {resource.Name}: {resource.Description}");
    }

    var mapPrompts = await mcpClient.ListPromptsAsync();
    Console.WriteLine($"\nAvaible Prompts: {mapPrompts}");
    foreach (var prompt in mapPrompts)
    {
        Console.WriteLine($"- {prompt.Name}: {prompt.Description}");
    }

    IChatClient claudeClient = new AnthropicClient().AsIChatClient("claude-haiku-4-5-20251001");

    AIAgent agent = claudeClient
        .AsBuilder()
        .UseFunctionInvocation()
        .BuildAIAgent(new ChatClientAgentOptions
        {
            Name = "Helpful Agent",
            ChatOptions = new ChatOptions
            {
                Instructions = "You are an helpfull assistant. Only use the available tool to fullfill user requests. Always respond in Brazilian Portuguese, regardless of the language the user writes in.",
                Tools = [.. mapTools]
            }
        });

    Console.WriteLine("\nInvoking agent...");
    var session = await agent.CreateSessionAsync();

    var sellerPrompt = await mcpClient.GetPromptAsync("seller-analyzer");

    var sellerPromptText = string.Join("\n", sellerPrompt.Messages
        .Select(m => (m.Content as TextContentBlock)?.Text)
        .Where(text => !string.IsNullOrEmpty(text)));

    var greeting = await agent.RunAsync(sellerPromptText, session);
    Console.WriteLine($"Agente: {greeting}\n");

    Console.WriteLine("\nDigite 'sair' pra encerrar.\n");

    while (true)
    {
        Console.Write("Você: ");
        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            continue;
        }

        if (input.Equals("sair", StringComparison.OrdinalIgnoreCase))
        {
            break;
        }

        var result = await agent.RunAsync(input, session);
        Console.WriteLine($"Agente: {result}\n");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error while connecting to the client: {ex.Message}");
}
