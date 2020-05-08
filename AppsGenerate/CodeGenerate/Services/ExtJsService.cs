using System;
using System.Runtime.CompilerServices;

namespace AppsGenerate.CodeGenerate.Services
{
    public static class ExtJsService
    {
        public static string GetExtType(Type dotNetType)
        {
            if (typeof(String) == dotNetType)
                return "string";
            if (typeof(Int16) == dotNetType || typeof(Int32) == dotNetType || typeof(Int64) == dotNetType)
                return "number";
            return null;
        }

        public static string GetFieldType(Type dotNetType)
        {
            if (typeof(String) == dotNetType)
                return "textfield";
            if (typeof(Int16) == dotNetType || typeof(Int32) == dotNetType || typeof(Int64) == dotNetType)
                return "textfield";
            return null;
        }
    }
}