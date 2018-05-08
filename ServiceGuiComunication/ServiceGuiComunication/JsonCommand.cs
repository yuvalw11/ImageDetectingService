using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGuiComunication
{
    public class JsonCommand
    {
        public int CommandID { get; set; }
        public string[] Args { get; set; }
        public bool Result { get; set; }
        public string JsonData { get; set; }

        public JsonCommand(int commandID, string[] args, bool result, string jsonData)
        {
            this.CommandID = commandID;
            this.Args = args;
            this.Result = result;
            this.JsonData = jsonData;
        }
    }
}
