using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGuiComunication
{
    //the client and the server pass to each other JsonCommand.
    public class JsonCommand
    {
        public int CommandID { get; set; } //this is the id of the command
        public string[] Args { get; set; } //this is the arguments that the command will need
        public bool Result { get; set; }  //result indicates if the command ended successfully
        public string JsonData { get; set; }//this is the result coming from the command if the command has already been executed

        //c'tor for JsonCommand
        public JsonCommand(int commandID, string[] args, bool result, string jsonData)
        {
            this.CommandID = commandID;
            this.Args = args;
            this.Result = result;
            this.JsonData = jsonData;
        }
    }
}
