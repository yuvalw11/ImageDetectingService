using ImageService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServiceGuiComunication;
using ImageService.ImageService.Infrastructure;

namespace ImageService.ImageService.Commands
{
    class GetConfigCommand : ICommand
    {

        public string Execute(string[] args, out bool result)
        {
            ConfigFileData cfd = new ConfigFileData();
            try
            {
                cfd.OutputDir = ReadAppConfig.ReadSetting("OutPutDir");
                cfd.SourceDir = ReadAppConfig.readAppSettings("ImageServiceSource");
                cfd.LogName = ReadAppConfig.readAppSettings("ImageServiceLog");
                cfd.ThumnailSize = ReadAppConfig.ReadThumbnailSize("ThumbnailSize");
                cfd.InputDirs = ReadAppConfig.ReadSetting("Handlers").Split(';');
                result = true;
                return JsonConvertor.ConvertToJson(cfd);
            }
            catch
            {
                result = false;
                return "problem occured";
            }
        }
    }
}
