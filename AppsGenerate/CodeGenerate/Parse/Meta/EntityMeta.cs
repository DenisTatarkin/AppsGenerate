namespace AppsGenerate.CodeGenerate.Parse.Meta
{
    public class EntityMeta
    {
        public string Name { get; set; }
        
        public string DisplayName { get; set; }
        
        public string ParentStructureName { get; set; }
        
        public PropertyMeta[] Properties { get; set; }
        
    }
}