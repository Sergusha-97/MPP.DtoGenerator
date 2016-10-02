using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITypeMap
{
    public interface ITypeMap
    {
         Dictionary<string, Type> TypeMap {  get;  }
    }
}
