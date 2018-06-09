using Infrustructure;
using ServiceGuiComunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Commands
{
    public class LogCommand : ICommand
    {
        private ImageWebModel model;

        public LogCommand(ImageWebModel model)
        {
            this.model = model;
        }

        public bool Execute(string[] args, string results)
        {
            LogData ld = new LogData();
            ld.Message = args[1];
            ld.Type = (int)Enum.Parse(typeof(MessageTypeEnum), args[0]);
            this.model.logs.Add(ld);

            return true;
        }
    }
}