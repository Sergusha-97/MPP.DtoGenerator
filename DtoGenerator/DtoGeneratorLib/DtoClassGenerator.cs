using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoGeneratorLib
{
    class DtoClassGenerator
    {
        private static volatile DtoClassGenerator instance = null;
        private static readonly object syncRoot = new Object();
        private string className;
        private string nameSpace;
        private DtoClassGenerator(string className, string nameSpace)
        {
            this.className = className;
            this.nameSpace = nameSpace;
        }
        public static DtoClassGenerator Instance(string className, string nameSpace)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new DtoClassGenerator(className,  nameSpace);
                    }
                }
            }
            return instance;
        }
        
    }
}
