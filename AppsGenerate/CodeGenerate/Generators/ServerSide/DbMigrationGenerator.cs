using System;
using System.IO;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class DbMigrationGenerator : ServerGenerator
    {
        public override FileInfo Generate(Structure structure, string path)
        {
            var entityStructure = structure as EntityStructure;
            
            using (var fs = File.Create($"{path}/Migrations/{structure.Name}Migration.cs"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($"namespace {entityStructure.Project.Name}.Migration{{");
                writer.WriteLine(UsingsService.GetUsings());
                writer.WriteLine($"[Migration({DateTime.Now.Year}{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Hour}{DateTime.Now.Minute}00)]");
                writer.WriteLine($"public class {structure.Name}Migration:Migration{{");
                writer.WriteLine($"public Up(){{");
                writer.WriteLine($"Create.Table(\"{structure.Name}\")");
                writer.WriteLine(".WithColumn(\"Id\").AsInt64().PrimaryKey().Identity");
                entityStructure.GetProperties().ForEach(x => writer.WriteLine($".WithColumn(\"{x.Name}\").As{x.Type}()"));
                writer.WriteLine(";}}");
            }

            return new FileInfo($"{structure.Name}Migration.cs");
 
        }
    }
}