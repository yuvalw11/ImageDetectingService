using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGuiComunication
{
    //struct that contains data about the app config data
    public struct ConfigFileData
    {
        public string OutputDir { get; set; }
        public string SourceDir { get; set; }
        public string LogName { get; set; }
        public int ThumnailSize { get; set; }
        public string[] InputDirs { get; set; }
    }
}
