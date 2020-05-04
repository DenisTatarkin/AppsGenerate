using System;
using AppsGenerate.Attributes;

namespace AppsGenerate.Services
{
    public class DisplayService
    {
        public string GetDisplay(Type t)
        {
            object[] attrs = t.GetCustomAttributes(false);
            foreach (DisplayAttribute attr in attrs)
                return attr.GetDisplay();

            return null;
        }
    }
}