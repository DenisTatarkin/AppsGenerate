using System;
using System.Text;

namespace AppsGenerate.CodeGenerate.Services
{
    public static class UsingsService
    {
        private static string[] usings = new[]
        {
            "System", "System.Collections.Generic","'Name'.Controllers",
            "'Name'.Data","'Name'.Model","NHibernate.Mapping.ByCode","NHibernate.Mapping.ByCode.Conformist", 
            "Newtonsoft.Json", "System.Linq"
        };
        
        public static string GetUsings(string projectName)
        {
            var code = new StringBuilder();
            
            foreach (var u in usings)
                code.AppendLine($"using {u.Replace("'Name'",projectName)};");

            return code.ToString();
        }
    }
}