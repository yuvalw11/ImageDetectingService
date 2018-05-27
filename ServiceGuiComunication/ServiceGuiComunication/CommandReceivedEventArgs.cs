using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGuiComunication
{

    public class CommandReceivedEventArgs : EventArgs
    {
        public JsonCommand JsonCommand { get; set; }

        public CommandReceivedEventArgs(JsonCommand command)
        {
            this.JsonCommand = command;
        }
    }
}
