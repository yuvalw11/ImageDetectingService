using ImageService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging;
using ImageService.ImageService.Logging;
using ServiceGuiComunication;

namespace ImageService.ImageService.Commands
{
    //request from the client to get a list of all the logs until now.
    class LogsCommand : ICommand
    {
        private ILoggingService ils;

        public LogsCommand(ILoggingService ils)
        {
            this.ils = ils;
        }

        //returns a json object of all logs
        public string Execute(string[] args, out bool result)
        {
            List<LogInfo> list = this.ils.GetMessages();
            List<LogData> lds = new List<LogData>(); //for comunication
            foreach(LogInfo info in list)
            {
                LogData data = new LogData();
                data.Message = info.Message;
                data.Type = info.Type;
                lds.Add(data);
            }
            result = true;
            return JsonConvertor.ConvertToJson(lds);
        }
    }
}
