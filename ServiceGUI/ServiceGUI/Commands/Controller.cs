using ServiceGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrustructure;

namespace ServiceGUI.Commands
{
    class Controller
    {
        private Dictionary<int, ICommand> commands;

        public Controller(ILogModel logModel, ISettingsModel settingModel)
        {
            this.commands = new Dictionary<int, ICommand>();
            this.commands.Add((int)CommandsEnum.GetConfigCommand, new GetConfigCommand(settingModel));
            this.commands.Add((int)CommandsEnum.LogsCommand, new LogsCommand(logModel));
            this.commands.Add((int)CommandsEnum.LogCommand, new LogCommand(logModel));
            this.commands.Add((int)CommandsEnum.RemoveDirCommand, new RemoveDirCommand(settingModel));
            this.commands.Add((int)CommandsEnum.CloseCommand, new CloseCommand(settingModel));
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
