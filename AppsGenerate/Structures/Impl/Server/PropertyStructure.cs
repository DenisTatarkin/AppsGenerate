using System;

namespace AppsGenerate.Structures.Impl
{
    public class PropertyStructure : Structure
    {
        public Type Type { get; set; }
        
        public override string ToCode()
        {
            return $"{Enum.GetName(typeof(AccessModType), AccessModificator).ToLower()} {Type.FullName} {Name} {{get;set;}}";
        }
    }
}