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
    class JsonParser
    {
        private string sourceName;
        public JsonParser (string filename)
        {
            sourceName = filename;
        }
        public ClassType[] GetClassesFromJson()
        {
            ClassType[] classTypes;
            using (FileStream fstream = new FileStream(sourceName, FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ClassType[]));
                classTypes = (ClassType[])serializer.ReadObject(fstream);
            }
            return classTypes;
        }

    }
}
