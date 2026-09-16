using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Trace;
}
);

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();

[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Echoes the message back to the client with optional prefix and repetition count")]
    public static string Echo(
        [Description("The message to echo back.")] string message,
        [Description("Optional prefix to add before the message.")] string? prefix = null,
        [Description("The number of times to repeat the message.")] int repeatCount = 1)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return "Error: Message cannot be empty.";
        }

        if (repeatCount < 1 || repeatCount > 10)
        {
            return "Error: Repeat count must be between 1 and 10.";
        }

        try
        {
            string result = string.IsNullOrWhiteSpace(prefix) ? message : $"{prefix} {message}";

            if (repeatCount > 1)
            {
                result = string.Join(" ", Enumerable.Repeat(result, repeatCount));
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error: An unexpected error occurred while processing the echo request. {ex.Message}";
        }
    }

    [McpServerTool, Description("Returns the current date and time")]
    public static string GetCurrentDateTime()
    {
        return DateTime.Now.ToString();
    }
}
