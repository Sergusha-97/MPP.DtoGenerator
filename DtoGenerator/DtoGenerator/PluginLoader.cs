using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
namespace DtoGenerator
{
    internal class PluginLoader : Loader
    {
        internal override List<ITypeMap.ITypeMap> LoadPlugins(string path)
        {
            try
            {


                string[] dllFileNames = null;
                if (Directory.Exists(path))
                {
                    dllFileNames = Directory.GetFiles(path, "*.dll");
                }
                else
                {
                    return new List<ITypeMap.ITypeMap>();
                }
                List<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
                foreach (string dllFile in dllFileNames)
                {
                    AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);

                }
                Type pluginType = typeof(ITypeMap.ITypeMap);
                List<Type> pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        Type[] types = assembly.GetTypes();
                        foreach (Type type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }

                    }
                }
                List<ITypeMap.ITypeMap> plugins = new List<ITypeMap.ITypeMap>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    ITypeMap.ITypeMap plugin = (ITypeMap.ITypeMap)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }
                return plugins;

            }
            catch
            {
                throw;
            }
        }
    }
}
