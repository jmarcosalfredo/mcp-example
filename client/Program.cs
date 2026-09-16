using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;

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

    var mapTools = await mcpClient.ListToolsAsync();
    Console.WriteLine(mapTools);

    IChatClient ollamaClient = new OllamaApiClient(new Uri("http://localhost:11434"), "granite4.1:3b");

    AIAgent agent = ollamaClient
        .AsBuilder()
        .UseFunctionInvocation()
        .BuildAIAgent(new ChatClientAgentOptions
        {
            Name = "Agente Corinthiano",
            ChatOptions = new ChatOptions
            {
                Instructions = "You are an helpfull assistant. Only use the available tool to fullfill user requests",
                Tools = [.. mapTools]
            }
        });

    Console.WriteLine("\nInvoking agent...");
    var session = await agent.CreateSessionAsync();

    var result = await agent.RunAsync("Echo the message 'Hello Agent Framework!' with the prefix 'MCP' and repeat count 3", session);
    Console.WriteLine($"Agent response: {result}");

    result = await agent.RunAsync("What is the currente date and time?", session);
    Console.WriteLine($"Agent response: {result}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error while connecting to the client: {ex.Message}");
}
