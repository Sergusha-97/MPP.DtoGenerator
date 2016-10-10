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
       internal int Max_task_number { get; set; }
       internal string Namespace { get; set; }
       internal string Destination_folder { get; set; }
       internal string Source_file { get; set; }
       internal string Plugins_source_folder { get; set; }

    }
}
