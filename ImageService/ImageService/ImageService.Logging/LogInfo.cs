using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService.Logging
{
    public class LogInfo
    {
        public LogInfo(int type, string message)
        {
            this.Message = message;
            this.Type = type;
        }

        public int Type { get; set; }
        public string Message { get; set; }
    }
}
