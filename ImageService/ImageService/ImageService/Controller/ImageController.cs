using ImageService.Commands;
using ImageService.ImageService.Commands;
using Infrustructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging;
using ImageService.Server;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        private Dictionary<int, ICommand> commands; //contains all the possible commands

        //c'tor for ImageController
        public ImageController(IImageServiceModal modal, ILoggingService ils)
        {
            commands = new Dictionary<int, ICommand>();
            commands.Add((int)CommandsEnum.NewFileCommand, new NewFileCommand(modal));
            commands.Add((int)CommandsEnum.GetConfigCommand, new GetConfigCommand());
            commands.Add((int)CommandsEnum.LogsCommand, new LogsCommand(ils));
            
        }

        public void addServer(ImageServer imse)
        {
            commands.Add((int)CommandsEnum.RemoveDirCommand, new RemoveDirCommand(imse));
        }

        //executes a command and set resultSuccesful if the command was successful or not
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            //trying to execute a command
            if (this.commands.ContainsKey(commandID))
            {
                return this.commands[commandID].Execute(args, out resultSuccesful);
            }
            else
            {
                resultSuccesful = false;
                return "command not available";
            }
           
        }
    }
}
