using System;
using System.IO;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class StructureGenerator : ServerGenerator
    {

        public override FileInfo Generate(Structure structure, string path)
        {
            Path = path + "/Models";
            var info = CreateFile(structure, path);
            InsertCode(info, structure);
            CloseCode(info);
            return info;
        }
        
        private FileInfo CreateFile(Structure structure, string path)
        {
            using (var fs = File.Create($"{Path}/{structure.Name}.cs"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($"namespace {structure.Project.Name}.Model{{");
                writer.WriteLine(UsingsService.GetUsings(structure.Project.Name));
                writer.WriteLine(Enum.GetName(typeof(AccessModType), structure.AccessModificator).ToLower() + " " +
                    Enum.GetName(typeof(StructureType), structure.StrucutureType).ToLower() + " " +
                    structure.Name + 
                    (structure.ParentStructure != null ? ":" + structure.ParentStructure.Name : ""));

                if (structure is EntityStructure)
                {
                    if (structure.ParentStructure == null)
                        writer.Write(":IHaveId");
                    else
                        writer.Write(", IHaveId");
                    if((structure as EntityStructure).GetLinkedEntities().Count != 0)
                        writer.Write(", IJson");
                    writer.WriteLine("{");
                    writer.WriteLine("public virtual long Id { get; set; }");
                }
            }

            return new FileInfo($"{structure.Name}.cs");
        }

        private void InsertCode(FileInfo file, Structure structure)
        {
            File.AppendAllLines(Path + "/" + file.Name, structure.ToCode().Split("\n"));
            if (structure is EntityStructure)
            {
                var entity = structure as EntityStructure;

                if (entity.GetLinkedEntities().Count > 0)
                {
                    var code = @"public virtual void SetPropertiesFromJson()
                    {";
                    entity.GetLinkedEntities().ForEach(x => code += $"{x.Name} = JsonConvert.DeserializeObject<{x.Name}>({x.Name}Json);\n");
                    code += "}";
                    File.AppendAllLines(Path + "/" + file.Name, new[] {code});
                }
            }
        }
        
        private void CloseCode(FileInfo file)
        {
            File.AppendAllLines(Path + "/" + file.Name, new[] {
                "}}"
            });
        }
    }
}