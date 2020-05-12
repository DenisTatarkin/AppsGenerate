using System.Collections.Generic;
using System.Text;

namespace AppsGenerate.Structures.Impl
{
    public class EntityStructure : Structure
    {
        private List<PropertyStructure> _properties;

        private List<LinkedEntityStructure> _linkedEntities;

        public EntityStructure()
        {
            _properties = new List<PropertyStructure>();
            _linkedEntities = new List<LinkedEntityStructure>();
            StrucutureType = StructureType.Class;
        }

        public void AddProperty(PropertyStructure property)
        {
            _properties.Add(property);
        }

        public List<PropertyStructure> GetProperties()
        {
            return _properties;
        }
        
        public bool RemoveLinkedEntity(LinkedEntityStructure entity)
        {
            return _linkedEntities.Remove(entity);
        }
        
        public void AddLinkedEntity(LinkedEntityStructure entity)
        {
            _linkedEntities.Add(entity);
        }

        public List<LinkedEntityStructure> GetLinkedEntities()
        {
            return _linkedEntities;
        }
        
        public bool RemoveProperty(PropertyStructure property)
        {
            return _properties.Remove(property);
        }

        public override string ToCode()
        {
            var code = new StringBuilder();
            _properties.ForEach(x => code.AppendLine(x.ToCode()));
            _linkedEntities.ForEach(x => code.AppendLine(x.ToCode()));
            return code.ToString();
        }
    }
}