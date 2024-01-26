using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.MISAAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty:Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyName : Attribute
    {
        public string Name = string.Empty;
        public PropertyName(string name) { 
            Name= name;

        }

    }
}
