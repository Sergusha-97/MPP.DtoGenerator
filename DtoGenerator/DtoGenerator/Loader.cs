using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITypeMap;

namespace DtoGenerator
{
    abstract class Loader
    {
        abstract public List<ITypeMap.ITypeMap> LoadPlugins(string path);
    }
}
