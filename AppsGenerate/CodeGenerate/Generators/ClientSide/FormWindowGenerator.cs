using System.IO;
using AppsGenerate.Structures;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class FormWindowGenerator : ClientGenerator
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
            using (var fs = File.Create($"{structure.Name}Window.js"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($@"Ext.define('ProjectName.view.{structure.Name}Window', {{
                   extend: 'Ext.window.Window',
    alias: 'widget.{structure.Name.ToLower()}-window',
    title: 'Title',
    layout: 'fit',
    autoShow: true,
    initComponent: function () {{
                    this.callParent(arguments);
                this.down('form').loadRecord(Ext.create('ProjectName.model.{structure.Name}Model'));
                    }},");
                return new FileInfo($"{structure.Name}Window.js");
            }
        }

        private void InsertCode(FileInfo file, ViewStructure structure)
        {
            File.AppendAllText(file.Name, structure.ToCode());
        }

        private void CloseCode(FileInfo file, Structure structure)
        {
            File.AppendAllLines(file.Name, new[]
                {@",
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