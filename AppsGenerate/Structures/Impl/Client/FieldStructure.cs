using System;
using System.Runtime.CompilerServices;
using AppsGenerate.CodeGenerate.Services;

namespace AppsGenerate.Structures.Impl
{
    public class FieldStructure : ViewStructure
    {
        public FieldStructure(PropertyStructure property) : base(property)
        {
            Name = property.Name.ToLower();
            Type = ExtJsService.GetExtType(property.Type);
            DisplayName = property.DisplayName;
        }
        
        public String Type { get; set; }
        
        public override string ToCode()
        {
            return $"{{name:'{Name}',type:'{Type}'}}";
        }
    }
}