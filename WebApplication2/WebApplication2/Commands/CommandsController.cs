using Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Commands
{
    class CommandsController
    {
        private Dictionary<int, ICommand> commands;

        public CommandsController(ImageWebModel model)
        {
            this.commands = new Dictionary<int, ICommand>();
            this.commands.Add((int)CommandsEnum.GetConfigCommand, new GetAppConfigCommand(model));
        }

        public bool ExecuteCommand(int commandID, string[] args, string results)
        {
            //trying to execute a command
            if (this.commands.ContainsKey(commandID))
            {
                return this.commands[commandID].Execute(args, results);
            }
            else
            {
                return false;
            }

        }
    }
}