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
            try
            {
                SettingsAssigner assigner = new SettingsAssigner();
                Settings settings = assigner.Set(new SettingsBuilder(), ConfigurationManager.AppSettings);
                JsonParser parser = new JsonParser(settings.SourceFile);
                ClassType[] classTypes = parser.GetClassesFromJson();
                PluginLoader ploader = new PluginLoader();
                List<ITypeMap.ITypeMap> pluginList = ploader.LoadPlugins(settings.PluginsSourceFolder);
                Dictionary<string, Type> typeMap = new Dictionary<string, Type>();
                AddElemsToTypeMap(typeMap, pluginList);
                using (DtoClassGenerator generator = new DtoClassGenerator(settings.Namespace, typeMap, settings.MaxTaskNumber))
                {
                    IDictionary<String, CodeCompileUnit> compileUnits = generator.GenerateCSharpCode(classTypes);
                    CSharpCodeSaver codeSaver = new CSharpCodeSaver();
                    codeSaver.Save(compileUnits, settings.DestinationFolder);
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return;
            }
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
