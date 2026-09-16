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

    async Task CallToolWithHandlingAsync(string toolName, Dictionary<string, object?> parameters)
    {
        try
        {
            Console.WriteLine($"\nCalling Tool: '{toolName}'");
            Console.WriteLine($"\nParameters: '{string.Join(", ", parameters.Select(p => $"{p.Key} = {p.Value}"))}'");
            var response = await client.CallToolAsync(toolName, parameters);
            if (response.IsError == true)
            {
                Console.WriteLine($"\nTool call failed.");

                if (response.Content != null)
                {
                    foreach (var content in response.Content)
                    {
                        if (content is TextContentBlock textBlock)
                        {
                            Console.WriteLine($"\nResult: {textBlock.Text}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"\nTool call succeeded.");

                foreach (var content in response.Content)
                {
                    if (content is TextContentBlock textBlock)
                    {
                        Console.WriteLine($"\nResult: {textBlock.Text}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while calling tool '{toolName}': {ex.GetType().Name} - {ex.Message}");
        }
    }

    await CallToolWithHandlingAsync("echo", new Dictionary<string, object?>
    {
        { "message", "\nHello, World!" },
        { "prefix", "\nClient Server:" },
        { "repeatCount", 3 }
    });

    await CallToolWithHandlingAsync("echo", new Dictionary<string, object?>
    {
        { "message", "\nHello, World!" },
    });

    await CallToolWithHandlingAsync("echo", new Dictionary<string, object?> //Error example
    {
        { "message", 12345 },
    });

    await CallToolWithHandlingAsync("echo", new Dictionary<string, object?> //Error example
    {
        { "message", "\nHello, World!" },
        { "repeatCount", -1 }
    });
}
catch (Exception ex)
{
    Console.WriteLine($"Error while connecting to the client: {ex.Message}");
}
