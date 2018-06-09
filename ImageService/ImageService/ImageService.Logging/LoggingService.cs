
using ImageService.ImageService.Logging;
using ImageService.Logging.Modal;
using Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        public List<LogInfo> logs;

        public LoggingService()
        {
            this.logs = new List<LogInfo>();
        }

        //the function sends the log the message to present
        public void Log(string message, MessageTypeEnum type)
        {
            LogInfo li = new LogInfo((int)type, message);
            this.logs.Add(li);
            MessageRecievedEventArgs e = new MessageRecievedEventArgs();
            e.Message = message;
            e.Status = type;
            this.MessageRecieved?.Invoke(this, e);

        }
        public List<LogInfo> GetMessages()
        {
            return this.logs;
        }
    }
}
