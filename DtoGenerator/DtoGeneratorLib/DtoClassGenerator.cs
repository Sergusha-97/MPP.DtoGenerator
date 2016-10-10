using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.CodeDom;
using DtoGenerator;
using Microsoft.CSharp;
using System.IO;
using System.Collections.Concurrent;


namespace DtoGeneratorLib
{
    public class DtoClassGenerator
    {
        private static readonly object syncRoot = new Object();
        private readonly string nameSpace;
        private  readonly Dictionary<string, Type> typeMap;
        private readonly List<string> usingStatements;
        private int max_task_number;
        private ConcurrentDictionary<string,CodeCompileUnit> compileUnits;
        private Semaphore semaphore;

        public DtoClassGenerator(string nameSpace,  Dictionary<string, Type> typeMap, int max_task_number)
        {
            this.nameSpace = nameSpace;
            this.typeMap = typeMap;
            this.max_task_number = max_task_number;
            usingStatements = new List<string>();
            usingStatements.Add("System");
            usingStatements.Add("System.Collections.Generic");
            usingStatements.Add("System.Linq");
            usingStatements.Add("System.Text");
            usingStatements.Add("System.Threading.Tasks");

        }
        private void ImportNamespaces(CodeNamespace ns)
        {
            foreach (string statement in usingStatements)
            {
                ns.Imports.Add(new CodeNamespaceImport(statement));
            }
        }
        private void CreateProperties(ClassField elem, CodeCompileUnit compileUnit, CodeTypeDeclaration classType)
        {
            var fieldName = "_" + elem.name;
            var field = new CodeMemberField(typeMap[elem.format], fieldName);
            classType.Members.Add(field);
            var property = new CodeMemberProperty();
            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            property.Type = new CodeTypeReference(typeMap[elem.format]);
            property.Name = elem.name;
            property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName)));
            property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), new CodePropertySetValueReferenceExpression()));
            classType.Members.Add(property);
        }
        private CodeCompileUnit GetCodeCompileUnits(ClassType classElem)
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            CodeNamespace ns = new CodeNamespace(nameSpace);
            compileUnit.Namespaces.Add(ns);
            ImportNamespaces(ns);
            CodeTypeDeclaration classType = new CodeTypeDeclaration(classElem.className);
            ns.Types.Add(classType);
            foreach (ClassField elem in classElem.properties)
            {
                CreateProperties(elem, compileUnit, classType);
            }
            return compileUnit;
            
        }
        private void CreateClass(ClassType classelem)
        {
                compileUnits[classelem.className] = GetCodeCompileUnits(classelem);
        }
        private void onTaskFinish (CountdownEvent countdownEvent)
        {         
            semaphore.Release();
            countdownEvent.Signal();
        }
        private void WaitAllTasksFinalization(CountdownEvent countdownEvent)
        {
            countdownEvent.Wait();
        }

        public ConcurrentDictionary<string, CodeCompileUnit> GenerateCSharpCode(ClassType[] classes)
        {
            compileUnits = new ConcurrentDictionary<string, CodeCompileUnit>();
            semaphore = new Semaphore(max_task_number, max_task_number);
            using (var countDownEvent = new CountdownEvent(classes.Length))
            {
                for (int i = 0; i < classes.Length; i++ )
                {
                    ClassType classelem = classes[i];
                    semaphore.WaitOne();
                    ThreadPool.QueueUserWorkItem(
                        delegate
                        {
                            CreateClass(classelem);
                            onTaskFinish(countDownEvent);
                            
                        });

                }
                WaitAllTasksFinalization(countDownEvent);
                return compileUnits;
            }
        }     
    }
}
