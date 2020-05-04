using System;
using System.Text;

namespace AppsGenerate.CodeGenerate.Services
{
    public static class UsingsService
    {
        private static string[] usings = new[] {"System", "System.Collections.Generic"};
        
        public static string GetUsings()
        {
            var code = new StringBuilder();
            
            foreach (var u in usings)
                code.AppendLine($"using {u};");

            return code.ToString();
        }
    }
}