using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Anthropic;

var transport = new StdioClientTransport(new StdioClientTransportOptions
{
    Command = "dotnet",
    Arguments = ["..\\mcp\\bin\\Debug\\net10.0\\mcp.dll",]
});

try
{
    await using var mcpClient = await McpClient.CreateAsync(transport);
    Console.WriteLine("Client successfully connected to server.");

    _ = mcpClient.Completion.ContinueWith((task) =>
    {
        var details = task.Result;
        Console.WriteLine("\n ===== Connection Completion Details =====");

        if (details.Exception != null)
        {
            Console.WriteLine($"Exception: {details.Exception.GetType().Name}");
            Console.WriteLine($"Exception Message: {details.Exception.Message}");
        }
        else
        {
            Console.WriteLine("Closure: Graceful (no exception!)");
        }

        if (details is StdioClientCompletionDetails stdioDetails)
        {
            Console.WriteLine($"Process ID: {stdioDetails.ProcessId}");
            Console.WriteLine($"Exit Code: {stdioDetails.ExitCode}");
            if (stdioDetails.StandardErrorTail is { Count: > 0 })
            {
                Console.WriteLine($"Sterr Tail: {string.Join(Environment.NewLine, stdioDetails.StandardErrorTail)}");
            }
        }
        Console.WriteLine("\n====================================");
    }, TaskScheduler.Default);

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
    Console.WriteLine($"\nAvaible Prompts: {mapResources}");
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
                Instructions = "You are an helpfull assistant. Only use the available tool to fullfill user requests",
                Tools = [.. mapTools]
            }
        });

    Console.WriteLine("\nInvoking agent...");
    var session = await agent.CreateSessionAsync();

    var prompResult = await mcpClient.GetPromptAsync("analyze-leadcount");

    var promptText = string.Join("\n", prompResult.Messages
    .Select(m => (m.Content as TextContentBlock)?.Text)
    .Where(text => !string.IsNullOrEmpty(text)));

    var resultTwo = await agent.RunAsync(promptText, session);
    Console.WriteLine($"\nAgent response: {resultTwo}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error while connecting to the client: {ex.Message}");
}
