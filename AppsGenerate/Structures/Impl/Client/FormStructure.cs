using System;
using System.Collections.Generic;
using System.Linq;

namespace AppsGenerate.Structures.Impl
{
    public class FormStructure : ViewStructure
    {
        private List<FormFieldStructure> _fields;
        private List<LinkedEntityField> _linkedEntityFields;
        public FormStructure(Structure structure) : base(structure)
        {
            _fields = new List<FormFieldStructure>();
            _linkedEntityFields = new List<LinkedEntityField>();
            ImplementFields();
        }
        
        private void ImplementFields()
        {
            var entity = Structure as EntityStructure;
            
            entity.GetProperties().ForEach(x => _fields.Add(new FormFieldStructure(x)));
            
            entity.GetLinkedEntities().ForEach(x => _linkedEntityFields.Add(new LinkedEntityField(x)));
        }

        public override string ToCode()
        {
            return $"items:[ {{xtype:'form', items:[{{xtype: 'textfield',name: 'id',hidden: true,margin: 10}},{String.Join(",",_fields.Select(x => x.ToCode()))},{String.Join(",",_linkedEntityFields.Select(x => x.ToCode()))}]}}],";
        }
    }
}