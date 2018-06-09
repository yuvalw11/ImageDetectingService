using ServiceGuiComunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Commands
{
    public class GetLogsCommand : ICommand
    {
        private ImageWebModel model;

        public GetLogsCommand(ImageWebModel model)
        {
            this.model = model;
        }

        public bool Execute(string[] args, string results)
        {
            List<LogData> data = JsonConvertor.ConvertToLogDataList(results);
            this.model.logs = data;

            return true;
        }
    }
}
