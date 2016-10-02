using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITypeMap
{
    interface ITypeMap
    {
        public Dictionary<string, Type> TypeMap { public get; private set; }
    }
}
