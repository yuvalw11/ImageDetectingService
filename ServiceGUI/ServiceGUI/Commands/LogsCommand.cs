using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.DataStructures;
using ServiceGuiComunication;

namespace ServiceGUI.Commands
{
     class LogsCommand : ICommand
    {
        private ILogModel logModel;
        public LogsCommand(ILogModel logModel)
        {
            this.logModel = logModel;
        }
        public bool Execute(string[] args, string results)
        {
            try
            {
                List<LogData> logs = JsonConvertor.ConvertToLogDataList(results);
                foreach (LogData log in logs)
                {

                    this.logModel.Logs.Add(new LogLine((MessageTypeEnum)log.Type, log.Message));
                }
                return true;
            } 
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
