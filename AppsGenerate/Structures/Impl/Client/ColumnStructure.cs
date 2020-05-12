namespace AppsGenerate.Structures.Impl
{
    public class ColumnStructure : ViewStructure
    {
        public ColumnStructure(){}
        public ColumnStructure(PropertyStructure property) : base(property)
        {
            Name = property.Name;
            DisplayName = property.DisplayName;
        }

        public override string ToCode()
        {
            return $@"{{
               text: '{DisplayName}',
               dataIndex: '{Name.ToLower()}',
               flex: 1,
               sortable: false
               }}";
        }
    }
}