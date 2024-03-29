using System.IO;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class ControllerGenerator : ServerGenerator
    {
        public override FileInfo Generate(Structure structure, string path)
        {
            using (var fs = File.Create($"{path}/Controllers/{structure.Name}Controller.cs"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($"namespace {structure.Project.Name}.Controller{{");
                writer.WriteLine(UsingsService.GetUsings(structure.Project.Name));
                writer.WriteLine($"public class {structure.Name}Controller:BaseController<{structure.Name}>{{");
                writer.WriteLine($"public {structure.Name}Controller(IDataAccessObject<{structure.Name}> db) : base(db){{}}");
                writer.WriteLine("}}");
            }

            return new FileInfo($"{structure.Name}Controller.cs");
        }
    }
}