using System.IO;
using AppsGenerate.Structures;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class GridGenerator : ClientGenerator
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
            using (var fs = File.Create($"{Path}/{structure.Name}List.js"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($@"Ext.define('{structure.Project.Name}.view.{structure.Name}List', {{
                   extend: 'Ext.grid.Panel',
    alias: 'widget.{structure.Name.ToLower()}-list',
    store: '{structure.Name}Store',
    itemId: '{structure.Name.ToLower()}-list',
    height: 900,
    scrollable: true,
    initComponent: function () {{
                    Ext.apply(this, {{
                    dockedItems:
                        [{{
                        xtype: 'toolbar',
                        docked: 'top',
                        items: [{{
                            iconCls: 'fa fa-refresh',
                            action: 'refresh'
                        }},
                        {{
                            text: 'Создать',
                            action: 'new'
                        }}, {{
                            text: 'Редактировать',
                            action: 'edit'
                        }}, {{
                            text: 'Удалить',
                            action: 'delete'
                        }},
                        {{
                            xtype: 'textfield'
                        }},
                        {{text: 'Поиск',
                            action: 'filter'}}]
                    }}]
                }});

                this.callParent(arguments);
                    }},");
                return new FileInfo($"{structure.Name}List.js");
            }
        }

        private void InsertCode(FileInfo file, ViewStructure structure)
        {
            File.AppendAllText(Path + "/" + file.Name, structure.ToCode());
        }

        private void CloseCode(FileInfo file, Structure structure)
        {
            File.AppendAllLines(Path + "/" + file.Name, new[]
            {"});"});
        }
    }
}