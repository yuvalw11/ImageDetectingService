
using ImageService.ImageService.Logging;
using ImageService.Logging.Modal;
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

        //the function sends the log the message to present
        public void Log(string message, MessageTypeEnum type)
        {
            this.logs = new List<LogInfo>();
            MessageRecievedEventArgs e = new MessageRecievedEventArgs();
            e.Message = message;
            e.Status = type;
            this.MessageRecieved?.Invoke(this, e);
        }
        public List<LogInfo> GetMessages()
        {
            return this.logs;
        }
        public void AddMessage(LogInfo li)
        {
            this.logs.Add(li);
        }
    }
}
