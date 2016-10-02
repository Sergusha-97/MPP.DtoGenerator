using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.CodeDom;
using DtoGenerator;
using Microsoft.CSharp;
using System.IO;


namespace DtoGeneratorLib
{
    class DtoClassGenerator
    {
        private static volatile DtoClassGenerator instance = null;
        private static readonly object syncRoot = new Object();
        private string className;
        private string nameSpace;
        private ClassType[] classes;
        private Dictionary<string, Type> typeMap;
        private DtoClassGenerator(string nameSpace, ClassType[] classes, Dictionary<string, Type> typeMap)
        {
            this.nameSpace = nameSpace;
            this.classes = classes;
            this.typeMap = typeMap;
        }
        public static DtoClassGenerator Instance(string nameSpace, ClassType[] classes, Dictionary<string, Type> typeMap)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new DtoClassGenerator(nameSpace, classes, typeMap);
                    }
                }
            }
            return instance;
        }
        private Dictionary<string,CodeCompileUnit> GetCodeCompileUnits()
        {
            Dictionary<string, CodeCompileUnit> result = new Dictionary<string, CodeCompileUnit>();
            foreach (ClassType classElem in classes)
            {
                CodeCompileUnit compileUnit = new CodeCompileUnit();
                CodeNamespace ns = new CodeNamespace(nameSpace);
                compileUnit.Namespaces.Add(ns);
                ns.Imports.Add(new CodeNamespaceImport("System"));
                CodeTypeDeclaration classType = new CodeTypeDeclaration(classElem.className);
                ns.Types.Add(classType);
                foreach (ClassField field in classElem.Fields)
                {
                    var property = new CodeMemberProperty();
                    property.Attributes = MemberAttributes.Public;
                    property.Type = new CodeTypeReference(typeMap[field.format]);
                    property.Name = field.name;
                    //property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName)));
                    //property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), new CodePropertySetValueReferenceExpression()));
                    classType.Members.Add(property);

                }
                result[classElem.className] = compileUnit;
            }
            return result;
        }
        public  void  GenerateCSharpCode(string path)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            Dictionary<string, CodeCompileUnit> compileUnits = GetCodeCompileUnits();
            foreach (String className in compileUnits.Keys)
            {
                string filename = Path.Combine(path, className + ".cs");
                using (StreamWriter sw = new StreamWriter(filename, false)) //???
                {
                    IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");
                    provider.GenerateCodeFromCompileUnit(compileunit, tw,
                        new CodeGeneratorOptions());
                    tw.Close();
                }

            }

        }
        
    }
}
