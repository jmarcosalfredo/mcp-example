using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ModelContextProtocol.Server;

namespace mcp.Tools
{
    [McpServerToolType, Description("Contains some tools used to provide math methodes")]
    public static class MathTools
    {
        [McpServerTool, Description("Adds two numbers togther.")]
        public static int Add(
            [Description("The first number to add.")]
            int a,
            [Description("The second number to add.")]
            int b
        )
        {
            return a + b;
        }

        [McpServerTool, Description("Multiply two numbers togther.")]
        public static int Multiply(
            [Description("The first number to multiply.")]
            int a,
            [Description("The second number to multiply.")]
            int b
        )
        {
            return a * b;
        }
    }
}
