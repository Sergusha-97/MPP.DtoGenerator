using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITypeMap;

namespace BasicTypesLib
{
    public class BasicTypes: ITypeMap.ITypeMap
    {
        private Dictionary<string,Type> typeMap;
        public Dictionary<string, Type> TypeMap
        {
            public get 
            {
                return typeMap;
            }
        }
        public BasicTypes ()
        {
            typeMap = new Dictionary<string, Type>();
            typeMap["int32"] = typeof(System.Int32);
            typeMap["int64"] = typeof(System.Int64);
            typeMap["float"] = typeof(System.Single);
            typeMap["double"] = typeof(System.Int32);
            typeMap["byte"] = typeof(System.Byte);
            typeMap[""] = typeof(System.Boolean);
            typeMap["date"] = typeof(System.DateTime);
            typeMap["string"] = typeof(System.String);
        }
    }
}
