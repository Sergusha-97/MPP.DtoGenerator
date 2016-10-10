using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace DtoGenerator
{
    internal class SettingsAssigner
    {
        internal Settings Set (Builder builder,NameValueCollection appSettings)
        {
            builder.CreateSettings(appSettings);
            builder.SetDestinationFolder();
            builder.SetMaxTaskNumber();
            builder.SetNamespace();
            builder.SetPluginsSourceFolder();
            builder.SetSourceFile();
            return builder.settings;
        }
    }
}
