using System;
using System.IO;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class MapGenerator : ServerGenerator
    {
        public override FileInfo Generate(Structure structure, string path)
        {
            var entityStructure = structure as EntityStructure;
            using (var fs = File.Create($"{path}/Data/{structure.Name}Map.cs"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($"namespace {entityStructure.Project.Name}.Map{{");
                writer.WriteLine(UsingsService.GetUsings(structure.Project.Name));
                writer.WriteLine($"public class {structure.Name}Map:ClassMapping<{structure.Name}>{{");
                writer.WriteLine($"public {structure.Name}Map(){{");
                writer.WriteLine("Id(x => x.Id, map =>  map.Generator(Generators.Native));");
                entityStructure.GetProperties().ForEach(x => writer.WriteLine($"Property(x=>x.{x.Name});"));
                entityStructure.GetLinkedEntities().ForEach(x => writer.WriteLine($"ManyToOne(x => x.{x.Name}, c => c.Column(\"{x.Name.ToLower()}_id\"));"));
                writer.WriteLine("}}}");
            }

            return new FileInfo($"{structure.Name}Map.cs");
        }
    }
}