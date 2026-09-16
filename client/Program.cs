using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

var transport = new StdioClientTransport(new StdioClientTransportOptions
{
    Command = "dotnet",
    Arguments = ["..\\mcp\\bin\\Debug\\net10.0\\mcp.dll",]
});

try
{
    await using var client = await McpClient.CreateAsync(transport);
    Console.WriteLine("Client successfully connected to server.");

    var tools = await client.ListToolsAsync();
    Console.WriteLine($"Available tools: {tools.Count}");

    foreach (var tool in tools)
    {
        Console.WriteLine($"Tool: {tool.Name}: {tool.Description}");
    }

    Console.WriteLine("\nCalling 'echo' tool with: 'hello MCP'");

    var result1 = await client.CallToolAsync(
        "echo",
        new Dictionary<string, object?>
        {
            { "message", "\nHello, from MCP Client!" },
            { "prefix", "\nMCP Client says:" },
            { "repeatCount", 2}
        }
    );
    Console.WriteLine($"Echo Tool Result: {((TextContentBlock)result1.Content[0]).Text}");

    var result2 = await client.CallToolAsync(
        "echo",
        new Dictionary<string, object?>
        {
            { "message", "\nHello, from MCP Client!" }
        }
    );
    Console.WriteLine($"Echo Tool Result: {((TextContentBlock)result2.Content[0]).Text}");

    Console.WriteLine("\nCalling 'echo' tool with with invalid input to demontrate error handling");

    try
    {
        var result3 = await client.CallToolAsync(
        "echo",
        new Dictionary<string, object?>
        {
            { "message", 12345 }
        }
    );
        Console.WriteLine($"Echo Tool Result: {((TextContentBlock)result3.Content[0]).Text}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error calling Echo Tool with invalid input: {ex.Message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error while connecting to the client: {ex.Message}");
}
