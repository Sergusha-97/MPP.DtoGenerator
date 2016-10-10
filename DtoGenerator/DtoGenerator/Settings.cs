using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
namespace DtoGenerator
{
   internal class Settings
    {
        public readonly int Max_task_number;
        public readonly string Namespace;
        public readonly string Destination_folder;
        public readonly string Source_file;
        public readonly string Plugins_source_folder;
        public Settings(NameValueCollection appSettings)
        {
            Max_task_number = Convert.ToInt32(appSettings["Max_task_number"]);
            Namespace = appSettings["Namespace"];
            Destination_folder = appSettings["Destination_folder"];
            Source_file = appSettings["Source_file"];
            Plugins_source_folder = appSettings["Plugins_source_folder"];
        }


    }
}
