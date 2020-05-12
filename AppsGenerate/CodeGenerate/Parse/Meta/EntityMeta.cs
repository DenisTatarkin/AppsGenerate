namespace AppsGenerate.CodeGenerate.Parse.Meta
{
    public class EntityMeta
    {
        public string Name { get; set; }
        
        public string DisplayName { get; set; }
        
        public string ParentStructureName { get; set; }
        
        public PropertyMeta[] Properties { get; set; }
        
        public LinkedEntityMeta[] LinkedEntities { get; set; }
        
        public bool IsChoosed { get; set; }
        
        public string ShownProperty { get; set; }
        
    }
}