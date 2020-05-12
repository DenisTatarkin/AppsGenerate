namespace AppsGenerate.Structures.Impl
{
    public class LinkedEntityStructure : Structure
    {
        public string ShownProperty { get; set; }
        public override string ToCode()
        {
            return $"public virtual {Name} {Name} {{get;set;}} public virtual string {Name}Json {{get;set;}}";
        }
    }
}