using System;
using System.Collections.Generic;
using System.Linq;

namespace AppsGenerate.Structures.Impl
{
    public class GridStructure : ViewStructure
    {
        public GridStructure(Structure structure) : base(structure)
        {
            _columns = new List<ColumnStructure>();
            ImplementColumns();
        }

        private List<ColumnStructure> _columns;

        private void ImplementColumns()
        {
            var entity = Structure as EntityStructure;
            entity.GetProperties().ForEach(x => _columns.Add(new ColumnStructure(x)));
            entity.GetLinkedEntities().ForEach(x => _columns.Add(new ColumnStructure
            {
                DisplayName = x.DisplayName,
                Name = $"{x.Name.ToLower()}{x.ShownProperty.ToLower()}",
            }));
        }
        
        public override string ToCode()
        {
            return " columns:[" + String.Join(",", _columns.Select(x => x.ToCode())) + "]";
        }
    }
}