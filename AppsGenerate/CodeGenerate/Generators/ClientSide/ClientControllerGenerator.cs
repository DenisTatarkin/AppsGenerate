using System.IO;
using AppsGenerate.Structures;

namespace AppsGenerate.CodeGenerate.Generators
{
    public class ClientControllerGenerator : ClientGenerator
    {
        public override FileInfo Generate(ViewStructure structure, string path)
        {
            Path = path + "/wwwroot/ClientApp/app/controller";
            var info = CreateFile(structure.Structure, path);
            InsertCode(info, structure);
            CloseCode(info, structure.Structure);
            return info;
        }
        
        private FileInfo CreateFile(Structure structure, string path)
        {
            using (var fs = File.Create($"{Path}/{structure.Name}Controller.js"))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine($@"Ext.define('{structure.Project.Name}.controller.{structure.Name}Controller', {{
    extend: 'Ext.app.Controller',
    views: ['{structure.Name}List', '{structure.Name}Window'],
    stores: ['{structure.Name}Store'],
    models: ['{structure.Name}Model'],

    init: function () {{
        this.control({{
            '{structure.Name.ToLower()}-list':{{
                added: function (grid) {{
                    grid.getStore().load();
                }}
            }},
            //Действия в гриде
            '{structure.Name.ToLower()}-list button[action=new]': {{
                click: this.openWindow
            }},
            '{structure.Name.ToLower()}-list button[action=edit]': {{
                click: this.openEditWindow
            }},
            '{structure.Name.ToLower()}-list button[action=delete]': {{
                click: this.delete
            }},
            '{structure.Name.ToLower()}-list button[action=refresh]': {{
                click: this.loadGrid
            }},
            '{structure.Name.ToLower()}-list button[action=filter]': {{
                click: this.filter
            }},
            //Действия на форме
            '{structure.Name.ToLower()}-window button[action=clear]': {{
                click: this.clearForm
            }},
            'artist-window button[action=save]': {{
                click: this.save
            }}
        }});
    }},

    openWindow: function () {{
        return Ext.widget('{structure.Name.ToLower()}-window');
    }},

    openEditWindow: function (button) {{
        let item = this.getSelectedItem(button);
        if (!item)
            return;

        let wnd = this.openWindow();
        wnd.down('form').loadRecord(item);
    }},

    delete: function (button) {{
        let item = this.getSelectedItem(button);
        item.erase({{
            success: function () {{
                let grid = button.up('grid');
                grid.getStore().load();
            }}
        }});       
    }},

    save: function (button) {{
        let form = button.up('window').down('form');
        let values = form.getValues();
        let method = values.id && parseInt(values.id) > 0 ? 'PUT' : 'POST'; 
        form.submit({{
            url: form.getRecord().getProxy().getUrl(),
            method: method,
            success: function () {{
                let grid = Ext.ComponentQuery.query('artists-list')[0];
                grid.getStore().load();
            }},
            failure: function () {{
                Ext.msg.alert('Ошибка сохранения', 'Произошла ошибка!');
            }}
        }});
    }},

    getSelectedItem: function (button) {{
        let grid = button.up('tabpanel').down('artists-list');
        let items = grid.getSelectionModel().getSelected().items;
        if (items.length == 0) {{
            Ext.Msg.alert('Ошибка', 'Выберите запись и повторите.', Ext.emptyFn);
            return;
        }}
        return items[0];
    }},

    loadGrid: async function (btn) {{
        let grid = btn.up('grid');
        grid.getStore().load();
    }},
    
    filter: async function (btn){{
        let grid = btn.up('grid');
        let field = grid.down('textfield');
        var response = Ext.Ajax.request({{
            url: grid.getStore().getProxy().getUrl() + '/filter',
            params: {{query : field.getRawValue()}},
            async: false
        }});

        if (response.status != 200) {{
            Ext.Msg.alert('Ошибка', response.responseText, Ext.emptyFn);
            return false;
        }}
        
        let data = Ext.JSON.decode(response.responseText);
        grid.getStore().loadData(data)
    }},
    
    clearForm: function(button){{
        button.up('window').down('form').reset();
    }}
}});");
                return new FileInfo($"{structure.Name}Controller.js");
            }
        }

        private void InsertCode(FileInfo file, ViewStructure structure)
        {
            return;
        }

        private void CloseCode(FileInfo file, Structure structure)
        {
            return;
        }
    }
}