using AppsGenerate.CodeGenerate.Parse.Meta;
using Newtonsoft.Json;

namespace AppsGenerate.CodeGenerate.Parse
{
    public class MetaParser
    {
        public ProjectMeta Parse(string json)
        {
            return JsonConvert.DeserializeObject<ProjectMeta>(json);
        }
    }
}