using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.CSharp;
using System.IO;

namespace DtoGenerator
{
    internal  class CSharpCodeSaver
    {
        internal void Save(IDictionary<String,CodeCompileUnit> compileUnits, string path)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            foreach (string classname in compileUnits.Keys)
            {
                string filename = Path.Combine(path, classname + ".cs");
                using (StreamWriter sw = new StreamWriter(filename, false))
                {
                    using (IndentedTextWriter tw = new IndentedTextWriter(sw, "    "))
                    {
                        provider.GenerateCodeFromCompileUnit(compileUnits[classname], tw,
                         new CodeGeneratorOptions());
                    }
                }
            }

        }
    }
}
