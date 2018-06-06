using ServiceGuiComunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Commands
{
    public class GetAppConfigCommand : ICommand
    {
        private ImageWebModel model;

        public GetAppConfigCommand( ImageWebModel model)
        {
            this.model = model;
        }

        public bool Execute(string[] args, string results)
        {
            ConfigFileData data = JsonConvertor.ConvertToConfigFileData(results);
            model.InputDirs = data.InputDirs;
            model.LogName = data.LogName;
            model.OutputDir = data.OutputDir;
            model.SourceDir = data.SourceDir;
            model.ThumnailSize = data.ThumnailSize;

            return true;
        }
    }
}