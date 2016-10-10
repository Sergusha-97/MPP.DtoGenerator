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
            settings.Destination_folder = appSettings["Destination_folder"]; ;
        }
        internal override void SetMaxTaskNumber()
        {
            settings.Max_task_number = Convert.ToInt32(appSettings["Max_task_number"]);
        }
        internal override void SetNamespace()
        {
            settings.Namespace  = appSettings["Namespace"];
        }
        internal override void SetPluginsSourceFolder()
        {
            settings.Plugins_source_folder = appSettings["Plugins_source_folder"]; 
        }
        internal override void SetSourceFile()
        {
            settings.Source_file = appSettings["Source_file"];
        }
    }
}
