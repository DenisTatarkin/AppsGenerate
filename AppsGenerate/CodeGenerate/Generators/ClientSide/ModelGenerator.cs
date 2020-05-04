using System;
using System.IO;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class ModelGenerator : ClientGenerator
    {
        public override FileInfo Generate(ViewStructure structure)
        {
            var info = CreateFile(structure.Structure);
            InsertCode(info, structure);
            CloseCode(info, structure.Structure);
            return info;
        }

        private FileInfo CreateFile(Structure structure)
        {
            using (var fs = File.Create($"{structure.Name}Model.js"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($@"Ext.define('ProjectName.model.{structure.Name}', {{
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
            File.AppendAllText(file.Name, structure.ToCode());
        }

        private void CloseCode(FileInfo file, Structure structure)
        {
            File.AppendAllLines(file.Name, new[]
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