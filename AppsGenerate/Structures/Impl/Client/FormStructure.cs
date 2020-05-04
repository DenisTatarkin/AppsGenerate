using System;
using System.Collections.Generic;
using System.Linq;

namespace AppsGenerate.Structures.Impl
{
    public class FormStructure : ViewStructure
    {
        private List<FormFieldStructure> _fields;
        public FormStructure(Structure structure) : base(structure)
        {
            _fields = new List<FormFieldStructure>();
            ImplementFields();
        }
        
        private void ImplementFields()
        {
            var entity = Structure as EntityStructure;
            
            entity.GetProperties().ForEach(x => _fields.Add(new FormFieldStructure(x)));
        }

        public override string ToCode()
        {
            return $"items:[ {{xtype:'form', items:[{String.Join(",",_fields.Select(x => x.ToCode()))}]}}],";
        }
    }
}