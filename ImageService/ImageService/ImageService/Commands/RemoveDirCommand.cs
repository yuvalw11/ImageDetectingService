using ImageService.Commands;
using ImageService.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService.Commands
{
    //request from the client to close a directory handler.
    public class RemoveDirCommand : ICommand
    {
        private ImageServer imse;

        public RemoveDirCommand(ImageServer imse)
        {
            this.imse = imse;
        }

        //argrs[0] is the dircetory to close.
        public string Execute(string[] args, out bool result)
        {
            this.imse.closeHandler(args[0]);
            result = true;
            return args[0];
        }
    }
}
