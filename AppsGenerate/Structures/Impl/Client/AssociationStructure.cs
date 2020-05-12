using System;

namespace AppsGenerate.Structures.Impl
{
    public class AssociationStructure : ViewStructure
    {
        public string Type { get; set; }
        public string ShownProperty { get; set; }
        public AssociationStructure(LinkedEntityStructure structure) : base(structure)
        {
            Name = structure.Name;
            ShownProperty = structure.ShownProperty;
        }

        public override string ToCode()
        {
            return
                $"{{type:'hasOne', model:'{Structure.Name}Model', name:'{Structure.Name.Remove(0, 1).Insert(0, Char.ToLower(Structure.Name[0]).ToString())}'}}";

        }
    }
}