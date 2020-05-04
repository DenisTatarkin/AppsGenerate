using System.IO;
using AppsGenerate.Structures;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class StoreGenerator : ClientGenerator
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
            using (var fs = File.Create($"{structure.Name}Store.js"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($@"Ext.define('{structure.Project.Name}.store.{structure.Name}', {{
                   extend: 'Ext.data.Store',
                   model: 'Fest.model.{structure.Name}Model',
    autoload: true,
    storeId: '{structure.Name}Store',
    proxy: {{
                    pageParam: undefined,
        startParam: undefined,
        limitParam: undefined,
        noCache: false,
        type: 'rest',
        url: '/api/{structure.Name.ToLower()}'
    }}");
                return new FileInfo($"{structure.Name}Store.js");
            }
        }

        private void InsertCode(FileInfo file, ViewStructure structure)
        {
           return;
        }

        private void CloseCode(FileInfo file, Structure structure)
        {
            File.AppendAllLines(file.Name, new[]
            {"});"});
        }
    }
}