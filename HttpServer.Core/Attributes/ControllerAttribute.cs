using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]

    sealed class ControllerAttribute 
        : Attribute
    {
        public string Name { get; private set; }
        public ControllerAttribute(string name)
        {
            this.Name = name;
        }
    }
}
