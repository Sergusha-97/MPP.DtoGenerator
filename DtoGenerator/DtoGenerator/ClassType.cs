using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DtoGenerator
{
    [DataContract]
    class ClassType
    {
        [DataMember]
        public string ClassName { get; set; }
        [DataMember]
        public ClassField[] Fields { get; set; }
    }
}
