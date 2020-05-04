using System;
using System.IO;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class StructureGenerator : ServerGenerator
    {

        public override FileInfo Generate(Structure structure)
        {
            var info = CreateFile(structure);
            InsertCode(info, structure);
            CloseCode(info);

            if (structure.GetType() == typeof(EntityStructure))
            {
                var mapGenerator = new MapGenerator();
                mapGenerator.Generate(structure as EntityStructure);
                
                var controllerGenerator = new ControllerGenerator();
                controllerGenerator.Generate(structure as EntityStructure);
            }

            return info;
        }
        
        private FileInfo CreateFile(Structure structure)
        {
            using (var fs = File.Create($"{structure.Name}.cs"))
            using (var writer = new StreamWriter(fs))
            {
                //todo: namespace
                writer.WriteLine(UsingsService.GetUsings());
                writer.WriteLine(Enum.GetName(typeof(AccessModType), structure.AccessModificator).ToLower() + " " +
                    Enum.GetName(typeof(StructureType), structure.StrucutureType).ToLower() + " " +
                    structure.Name + 
                    (structure.ParentStructure != null ? ":" + structure.ParentStructure.Name : "") + 
                                                                                               " {");
                writer.WriteLine("//GeneratedCode");
            }

            return new FileInfo($"{structure.Name}.cs");
        }

        private void InsertCode(FileInfo file, Structure structure)
        {
            File.AppendAllLines(file.Name, structure.ToCode().Split("\n"));
        }
        
        private void CloseCode(FileInfo file)
        {
            File.AppendAllLines(file.Name, new[] {
                "}"
            });
        }
    }
}