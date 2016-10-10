using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITypeMap;

namespace DtoGenerator
{
    internal abstract class Loader
    {
        abstract internal List<ITypeMap.ITypeMap> LoadPlugins(string path);
    }
}
