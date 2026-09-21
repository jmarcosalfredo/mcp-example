using System.ComponentModel;
using ModelContextProtocol.Server;
using Newtonsoft.Json;

namespace mcp.Resources
{
    [McpServerResourceType]
    public static class SystemResources
    {
        [McpServerResource(
            Name = "system-info",
            UriTemplate = "system://info",
            MimeType = "application/json"
        ),
        Description("Return basic information about the running .NET environment.")]
        public static string GetSystemInfo()
        {
            var info = new
            {
                DotNetVersion = Environment.Version.ToString(),
                OperatingSystem = Environment.OSVersion.VersionString,
                ProcessorCount = Environment.ProcessorCount,
                CurrentDirectory = Environment.CurrentDirectory,
                TimeStamp = DateTime.UtcNow
            };

            return JsonConvert.SerializeObject(info, Formatting.Indented);
        }

        [McpServerResource(
            Name = "app-settings",
            UriTemplate = "config://settings",
            MimeType = "application/json"
        ),
        Description("Returns applications setting and environment variables.")]
        public static string GetAppSettings()
        {
            var settings = new
            {
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
                ServerStarted = DateTime.UtcNow,
                SupportedFeatures = new[] { "tools", "resources", "prompts" }
            };

            return JsonConvert.SerializeObject(settings, Formatting.Indented);
        }
    }
}
