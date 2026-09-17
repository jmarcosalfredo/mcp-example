using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;

namespace mcp.Tools
{
    [McpServerToolType]
    public static class StringTools
    {
        [McpServerTool, Description("Converts text to uppercase, lowercase or title case")]
        public static string ConvertCase(
            [Description("The text to convert")]
            string text,
            [Description("The target case type")]
            [AllowedValues("uppercase", "lowercase", "title")]
            string caseType = "uppercase"
        )
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Text cannot be null or whitespace", nameof(text));
            }

            var result = caseType.ToLower() switch
            {
                "uppercase" => text.ToUpper(),
                "lowercase" => text.ToLower(),
                "title" => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLowerInvariant()),
                _ => text
            };

            return result;
        }

        [McpServerTool, Description("Returns the lenght of the given text.")]
        public static int GetLenght(
            [Description("The text to get the lenght of")]
            string text
        )
        {
            return text.Length;
        }
    }
}
