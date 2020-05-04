using System;
using System.IO;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class MapGenerator : ServerGenerator
    {
        public override FileInfo Generate(Structure structure)
        {
            var entityStructure = structure as EntityStructure;
            using (var fs = File.Create($"{structure.Name}Map.cs"))
            using (var writer = new StreamWriter(fs))
            {
                //todo: namespace
                writer.WriteLine(UsingsService.GetUsings());
                writer.WriteLine($"public class {structure.Name}Map:ClassMapping<{structure.Name}>{{");
                writer.WriteLine($"public {structure.Name}Map(){{");
                writer.WriteLine("Id(x => x.Id, map =>  map.Generator(Generators.Native));");
                entityStructure.GetProperties().ForEach(x => writer.WriteLine($"Property(x=>x.{x.Name});"));
                writer.WriteLine("}");
            }

            return new FileInfo($"{structure.Name}Map.cs");
        }
    }
}