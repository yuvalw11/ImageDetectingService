using ServiceGUI.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Commands
{
    public class LogCommand : ICommand
    {
        private ILogModel model;

        public LogCommand(ILogModel model)
        {
            this.model = model;
        }
        public bool Execute(string[] args, string results)
        {
            try
            {
                MessageTypeEnum result;
                Enum.TryParse(args[0], out result);
                this.model.Logs.Add(new LogLine(result, args[1]));
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
