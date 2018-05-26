using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGuiComunication
{
    public struct ConfigFileData
    {
        public string OutputDir { get; set; }
        public string SourceDir { get; set; }
        public string LogName { get; set; }
        public int ThumnailSize { get; set; }
        public string[] InputDirs { get; set; }
    }
}
