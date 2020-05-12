using System;
using System.IO;
using System.Linq;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;

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
                var entityStructure = structure as EntityStructure;
                writer.WriteLine($"public override ICollection<{structure.Name}> Filter(string query){{");
                
                if (entityStructure.GetProperties().Count > 0)
                    writer.WriteLine(
                        $"return _session.Query<{structure.Name}>().Where(x => {String.Join("||", entityStructure.GetProperties().Select(x => $"x.{x.Name}.ToString() == query"))}).ToList();}}");
                else
                    writer.WriteLine("throw new NotImplementedException();}");
                
                writer.WriteLine("}}");
            }

            return new FileInfo($"{structure.Name}DAO.cs");
        }
    }
}