namespace AppsGenerate.Structures.Impl
{
    public class LinkedEntityField : ViewStructure
    {
        public string ShownProperty { get; set; } 
        public LinkedEntityField(LinkedEntityStructure structure) : base(structure)
        {
            Name = structure.Name;
            DisplayName = structure.DisplayName;
            ShownProperty = structure.ShownProperty;
        }

        public override string ToCode()
        {
            return $@"{{
                xtype: 'textfield',
                        name: '{Structure.Name}Json',
                        fieldLabel: '{Structure.DisplayName}',
                        itemId: '{Structure.Name.ToLower()}-field',
                        setValue: function (value) {{
                if (Ext.isObject(value) && value.{ShownProperty.ToLower()}) {{
                    this.setRawValue(value.{ShownProperty.ToLower()});
                }}
                else {{
                    this.setRawValue(value);
                }}

                this.value = value;
            }},
            getSubmitValue: function () {{
                return Ext.encode(this.value);
            }},
            margin: 10,
                        triggers: {{
                search: {{
                    cls: 'x-form-search-trigger',
                    handler: function () {{
                        Ext.widget('{Structure.Name.ToLower()}choose-window').show();
                    }}
                }}
            }}
            }}";
        }
    }
}