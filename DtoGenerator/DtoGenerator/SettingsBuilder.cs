using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoGenerator
{
    internal class SettingsBuilder:Builder
    {
        internal override void SetDestinationFolder()
        {
            settings.DestinationFolder = appSettings["Destination_folder"]; ;
        }
        internal override void SetMaxTaskNumber()
        {
            settings.MaxTaskNumber = Convert.ToInt32(appSettings["Max_task_number"]);
        }
        internal override void SetNamespace()
        {
            settings.Namespace  = appSettings["Namespace"];
        }
        internal override void SetPluginsSourceFolder()
        {
            settings.PluginsSourceFolder = appSettings["Plugins_source_folder"]; 
        }
        internal override void SetSourceFile()
        {
            settings.SourceFile = appSettings["Source_file"];
        }
    }
}
