using System.IO;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class DaoGenerator : ServerGenerator
    {
        public override FileInfo Generate(Structure structure, string path)
        {
            using (var fs = File.Create($"{path}/Data/{structure.Name}DAO.cs"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($"namespace {structure.Project.Name}.Data{{");
                writer.WriteLine(UsingsService.GetUsings(structure.Project.Name));
                writer.WriteLine($"public class {structure.Name}DAO:DataAccessObject<{structure.Name}>{{");
                writer.WriteLine($"public {structure.Name}DAO() : base(){{}}");
                writer.WriteLine("}}");
            }

            return new FileInfo($"{structure.Name}DAO.cs");
        }
    }
}