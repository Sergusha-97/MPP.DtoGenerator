using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DtoGenerator
{
    [DataContract]
    public class ClassType
    {
        [DataMember]
        public string className { get; set; }
        [DataMember]
        public ClassField[] properties { get; set; }
    }
}
