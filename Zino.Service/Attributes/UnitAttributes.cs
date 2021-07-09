using System;
using System.Collections.Generic;
using System.Text;

namespace Zino.Service.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class UnitAttributes : Attribute
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public double Unit { get; set; }
    }
}
