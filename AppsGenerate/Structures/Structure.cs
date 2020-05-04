using AppsGenerate.Structures.Impl;

namespace AppsGenerate.Structures
{
    public abstract class Structure
    {
        public ProjectStructure Project { get; set; }
        
        public Structure ParentStructure { get; set; }
        public StructureType StrucutureType { get; set; }
        public string Name { get; set; }
        
        public string DisplayName { get; set; }
        
        public AccessModType AccessModificator { get; set; }

        public string ToEmptyCode()
        {
            return $@"{AccessModificator}";
        }
        public abstract string ToCode();
    }
}