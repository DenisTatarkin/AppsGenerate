using System.IO;
using AppsGenerate.Structures;

namespace AppsGenerate.CodeGenerate.Generators
{
    public abstract class ClientGenerator
    {
        public abstract FileInfo Generate(ViewStructure structure, string path);
        
        public string Path { get; set; }
    }
}