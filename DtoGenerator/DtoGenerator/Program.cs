using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoGeneratorLib;
using System.Configuration;
using System.Collections.Specialized;
namespace DtoGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings(ConfigurationManager.AppSettings);
            JsonParser parser = new JsonParser(settings.Source_file);
            ClassType[] classTypes = parser.GetClassesFromJson();
            PluginLoader ploader = new PluginLoader();
            var pluginList = ploader.LoadPlugins(settings.Plugins_source_folder);
            Dictionary<string, Type> typeMap = new Dictionary<string, Type>();
            foreach (var elem in pluginList)
            {
                foreach (var point in elem.TypeMap)
                {
                    typeMap.Add(point.Key, point.Value);
                }
            }
            DtoClassGenerator generator =  new DtoClassGenerator(settings.Namespace,typeMap,settings.Max_task_number);
            generator.GenerateCSharpCode(settings.Destination_folder,classTypes);
            Console.ReadLine();
        }
    }
}
