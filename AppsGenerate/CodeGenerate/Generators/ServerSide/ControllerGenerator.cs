using System.IO;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class ControllerGenerator : ServerGenerator
    {
        public override FileInfo Generate(Structure structure)
        {
            using (var fs = File.Create($"{structure.Name}Controller.cs"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($"namespace {structure.Project.Name}.Controller{{");
                writer.WriteLine(UsingsService.GetUsings());
                writer.WriteLine($"public class {structure.Name}Controller:BaseController<{structure.Name}>{{");
                writer.WriteLine($"public {structure.Name}Controller(IDataAccessObject<{structure.Name}> db) : base(db){{}}");
                writer.WriteLine("}}");
            }

            return new FileInfo($"{structure.Name}Controller.cs");
        }
    }
}