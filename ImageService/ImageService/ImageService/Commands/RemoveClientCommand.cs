using ImageService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Server;

namespace ImageService.ImageService.Commands
{
    class RemoveClientCommand : ICommand
    {
        private ImageServer server;

        public RemoveClientCommand(ImageServer server)
        {
            this.server = server;
        }

        //assume args[0] is the url of the handler to remove
        string Execute(string[] args, out bool result)
        {

        }
    }
}
