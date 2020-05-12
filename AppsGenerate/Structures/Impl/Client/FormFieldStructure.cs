using System;
using AppsGenerate.CodeGenerate.Services;

namespace AppsGenerate.Structures.Impl
{
    public class FormFieldStructure : ViewStructure
    {
        public String Type { get; set; }
        
        public FormFieldStructure(PropertyStructure property) : base(property)
        {
            Name = property.Name.ToLower();
            Type = ExtJsService.GetFieldType(property.Type);
            DisplayName = property.DisplayName;
        }

        public override string ToCode()
        {
            return $@"{{xtype: '{Type}',
                name: '{Name}',
                fieldLabel: '{DisplayName}',
                margin: 10}}";
        }
    }
}