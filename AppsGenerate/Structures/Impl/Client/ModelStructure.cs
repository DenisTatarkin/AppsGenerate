using System;
using System.Collections.Generic;
using System.Linq;
using AppsGenerate.CodeGenerate.Services;
using AppsGenerate.Structures;

namespace AppsGenerate.Structures.Impl
{
    public class ModelStructure : ViewStructure
    {
        public List<FieldStructure> Fields { get; set; }
        
        public ModelStructure(EntityStructure structure) : base(structure)
        {
            Fields = new List<FieldStructure>();
            ImplementFields();
        }

        private void ImplementFields()
        {
            var entity = Structure as EntityStructure;
            
            entity.GetProperties().ForEach(x => Fields.Add(new FieldStructure(x)));
        }

        public override string ToCode()
        {
            return $"[{String.Join(",",Fields.Select(x => x.ToCode()))}]";
        }
    }
}