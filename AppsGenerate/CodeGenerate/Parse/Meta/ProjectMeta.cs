namespace AppsGenerate.CodeGenerate.Parse.Meta
{
    public class ProjectMeta
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string DisplayName { get; set; }
        public string DbConnectionString { get; set; }
        
        public EntityMeta[] Entities { get; set; }
        
        public EnumMeta[] Enums { get; set; }
        
        
    }
}