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
       internal int MaxTaskNumber { get; set; }
       internal string Namespace { get; set; }
       internal string DestinationFolder { get; set; }
       internal string SourceFile { get; set; }
       internal string PluginsSourceFolder { get; set; }

    }
}
