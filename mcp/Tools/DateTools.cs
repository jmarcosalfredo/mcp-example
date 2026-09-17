using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ModelContextProtocol.Server;

namespace mcp.Tools
{
    [McpServerToolType]
    public static class DateTools
    {
        [McpServerTool, Description("Returns the current date and time")]
        public static string GetCurrentDateTime()
        {
            return DateTime.Now.ToString();
        }
    }
}
