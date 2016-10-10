using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace DtoGenerator
{
    internal class Builder
    {
        internal Settings settings { get; private set; }
        protected  NameValueCollection appSettings;
        internal void CreateSettings(NameValueCollection appSettings)
        {
            this.settings = new Settings();
            this.appSettings = appSettings;
        }
        internal abstract void SetMaxTaskNumber();
        internal abstract void SetNamespace();
        internal abstract void SetDestinationFolder();
        internal abstract void SetSourceFile();
        internal abstract void SetPluginsSourceFolder();
        
    }
}
