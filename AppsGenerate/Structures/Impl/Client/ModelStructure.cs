using System;
using System.Collections.Generic;
using System.Linq;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;
using Microsoft.VisualBasic;

namespace AppsGenerate.Structures.Impl
{
    public class ModelStructure : ViewStructure
    {
        public List<FieldStructure> Fields { get; set; }
        public List<AssociationStructure> Associations { get; set; }
        
        public ModelStructure(EntityStructure structure) : base(structure)
        {
            Fields = new List<FieldStructure>();
            Associations = new List<AssociationStructure>();
            ImplementFields();
        }

        private void ImplementFields()
        {
            var entity = Structure as EntityStructure;
            
            entity.GetProperties().ForEach(x => Fields.Add(new FieldStructure(x)));
            entity.GetLinkedEntities().ForEach(x => Associations.Add(new AssociationStructure(x)));
        }

        public override string ToCode()
        {
            var code = $"fields:[{{name:'id',type:'number'}},{String.Join(",",Fields.Select(x => x.ToCode()))},";
            if (Associations.Count > 0)
            {
                Associations.ForEach(x => code += $"{{name : '{x.Name.ToLower()}{x.ShownProperty.ToLower()}', type: 'string', mapping : '{x.Name.Remove(0, 1).Insert(0, Char.ToLower(x.Name[0]).ToString())}.{x.ShownProperty.ToLower()}'}},");
                code += "],";
                code += $"associations:[{String.Join(",", Associations.Select(x => x.ToCode()))}],";
            }
            else
                code += "],";

            return code;
        }
    }
}