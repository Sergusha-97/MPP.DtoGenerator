using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoGeneratorLib;
using System.Configuration;
using System.Collections.Specialized;
using System.CodeDom.Compiler;
using System.CodeDom;
namespace DtoGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            SettingsAssigner assigner = new SettingsAssigner();
            Settings settings = assigner.Set(new SettingsBuilder(), ConfigurationManager.AppSettings);
            JsonParser parser = new JsonParser(settings.Source_file);
            ClassType[] classTypes = parser.GetClassesFromJson();
            PluginLoader ploader = new PluginLoader();
            var pluginList = ploader.LoadPlugins(settings.Plugins_source_folder);
            Dictionary<string, Type> typeMap = new Dictionary<string, Type>();
            AddElemsToTypeMap(typeMap, pluginList);
            DtoClassGenerator generator =  new DtoClassGenerator(settings.Namespace,typeMap,settings.Max_task_number);
            var compileUnits = generator.GenerateCSharpCode(classTypes);
            CSharpCodeSaver codeSaver = new CSharpCodeSaver();
            codeSaver.Save(compileUnits, settings.Destination_folder);
            Console.ReadLine();
        }
        static void AddElemsToTypeMap(IDictionary<string, Type> typeMap,IList<ITypeMap.ITypeMap> pluginList)
        {
            foreach (var elem in pluginList)
            {
                foreach (var point in elem.TypeMap)
                {
                    typeMap.Add(point.Key, point.Value);
                }
            }
        }
    }
}
