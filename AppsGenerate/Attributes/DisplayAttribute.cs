using System;

namespace AppsGenerate.Attributes
{
    public class DisplayAttribute : Attribute
    {
        private string _display;

        public DisplayAttribute(string display)
        {
            _display = display;
        }

        public string GetDisplay()
        {
            return _display;
        }
    }
}