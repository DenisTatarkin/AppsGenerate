using System.IO;
using AppsGenerate.Structures;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class ChooseFormWindowGenerator : ClientGenerator
    {
        public override FileInfo Generate(ViewStructure structure, string path)
        {
            Path = path + "/wwwroot/ClientApp/app/view";
            var info = CreateFile(structure.Structure, path);
            return info;
        }

        private FileInfo CreateFile(Structure structure, string path)
        {
            using (var fs = File.Create($"{Path}/{structure.Name}ChooseWindow.js"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($@"Ext.define('{structure.Project.Name}.view.{structure.Name}ChooseWindow', {{
                    extend: 'Ext.window.Window',
    alias: 'widget.{structure.Name.ToLower()}choose-window',
    title: 'Выбор {structure.DisplayName}',
    autoShow: true,
    items:
        [{{
                    items: {{ xtype: '{structure.Name.ToLower()}-list', dockedItems: [], width: 600, height:500}}
                }}],
                buttons:
        [
            {{
                    text: 'Выбрать',
                    scope: this,
                    action: 'save'
                }}
                ]
                }});");
                return new FileInfo($"{structure.Name}ChooseWindow.js");
            }
        }
        }
    }
