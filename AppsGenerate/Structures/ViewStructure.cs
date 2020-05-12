using AppsGenerate.Structures;

namespace AppsGenerate.Structures
{
    public abstract class ViewStructure
    {
        public ViewStructure(){}
        public ViewStructure(Structure structure)
        {
            Structure = structure;
        }
        
        public string Name { get; set; }
        
        public string DisplayName { get; set; }
        
        public Structure Structure { get; set; }
        
        public abstract string ToCode();
    }
}