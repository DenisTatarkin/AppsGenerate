using System;
using System.IO;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class ModelGenerator : ClientGenerator
    {
        public override FileInfo Generate(ViewStructure structure, string path)
        {
            Path = path + "/wwwroot/ClientApp/app/model";
            var info = CreateFile(structure.Structure, path);
            InsertCode(info, structure);
            CloseCode(info, structure.Structure);
            return info;
        }

        private FileInfo CreateFile(Structure structure, string path)
        {
            using (var fs = File.Create($"{Path}/{structure.Name}Model.js"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($@"Ext.define('{structure.Project.Name}.model.{structure.Name}', {{
                   extend: 'Ext.data.Model',
                    idProperty: 'id',
                    fields:
                            {{
                  ");
                return new FileInfo($"{structure.Name}Model.js");
            }
        }

        private void InsertCode(FileInfo file, ViewStructure structure)
        {
            File.AppendAllText(Path + "/" + file.Name, structure.ToCode());
        }

        private void CloseCode(FileInfo file, Structure structure)
        {
            File.AppendAllLines(Path + "/" + file.Name, new[]
            {
               $@", proxy: {{
        type: 'rest',
        url: '/api/{structure.Name}',
        writer: {{
            type: 'json',
                writeAllFields: true
            }}
        }}
    }}
    }});"
            });
        }
    }
}