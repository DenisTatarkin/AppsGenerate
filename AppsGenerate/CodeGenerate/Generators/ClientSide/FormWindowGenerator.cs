using System.IO;
using AppsGenerate.Structures;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class FormWindowGenerator : ClientGenerator
    {
        public override FileInfo Generate(ViewStructure structure, string path)
        {
            Path = path + "/wwwroot/ClientApp/app/view";
            var info = CreateFile(structure.Structure, path);
            InsertCode(info, structure);
            CloseCode(info, structure.Structure);
            return info;
        }
        
        private FileInfo CreateFile(Structure structure, string path)
        {
            using (var fs = File.Create($"{Path}/{structure.Name}Window.js"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($@"Ext.define('{structure.Project.Name}.view.{structure.Name}Window', {{
                   extend: 'Ext.window.Window',
    alias: 'widget.{structure.Name.ToLower()}-window',
    title: 'Title',
    layout: 'fit',
    autoShow: true,
    initComponent: function () {{
                    this.callParent(arguments);
                this.down('form').loadRecord(Ext.create('{structure.Project.Name}.model.{structure.Name}Model'));
                    }},");
                return new FileInfo($"{structure.Name}Window.js");
            }
        }

        private void InsertCode(FileInfo file, ViewStructure structure)
        {
            File.AppendAllText(Path + "/" + file.Name, structure.ToCode());
        }

        private void CloseCode(FileInfo file, Structure structure)
        {
            File.AppendAllLines(Path + "/" + file.Name, new[]
                {@"
    buttons:
        [
            {
                text: 'Очистить',
                scope: this,
                action: 'clear'
            },
            {
                text: 'Сохранить',
                scope: this,
                action: 'save'
            }
        ]
});"});
        }
    }
}