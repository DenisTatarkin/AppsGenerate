using System.Collections.Generic;
using System.Text;

namespace AppsGenerate.Structures.Impl
{
    public class EntityStructure : Structure
    {
        private List<PropertyStructure> _properties;
        
        public EntityStructure()
        {
            _properties = new List<PropertyStructure>();
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
        
        public bool RemoveProperty(PropertyStructure property)
        {
            return _properties.Remove(property);
        }

        public override string ToCode()
        {
            var code = new StringBuilder();
            _properties.ForEach(x => code.AppendLine(x.ToCode()));
            return code.ToString();
        }
    }
}