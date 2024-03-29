using System.IO;
using AppsGenerate.Structures;

namespace AppsGenerate.CodeGenerate.Generators
{
    public abstract class ServerGenerator
    {
        public abstract FileInfo Generate(Structure structure, string path);
        
        public string Path { get; set; }
    }
}