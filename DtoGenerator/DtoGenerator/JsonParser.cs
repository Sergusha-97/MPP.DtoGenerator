using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
 

namespace DtoGenerator
{
    internal class JsonParser
    {
        private string sourceName;
        public JsonParser (string filename)
        {
            sourceName = filename;
        }
        internal ClassType[] GetClassesFromJson()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ClassType[]));
            using (FileStream fs = new FileStream(sourceName, FileMode.OpenOrCreate))
            {
                ClassType[] newpeople = (ClassType[])jsonFormatter.ReadObject(fs);
                return newpeople;

            }

         
        }

    }
}
