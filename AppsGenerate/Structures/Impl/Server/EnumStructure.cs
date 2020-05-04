using System;
using System.Collections.Generic;

namespace AppsGenerate.Structures.Impl
{
    public class EnumStructure : Structure
    {
        public List<String> _consts;

        public EnumStructure()
        {
            _consts = new List<string>();
            StrucutureType = StructureType.Enum;
        }
        public override string ToCode()
        {
            return String.Join(",\n", _consts);
        }
    }
}