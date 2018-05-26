using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.Models;
using ServiceGuiComunication;
namespace ServiceGUI.Commands
{
    public class GetConfigCommand : ICommand
    {
        private ISettingsModel model;

        public GetConfigCommand(ISettingsModel model)
        {
            this.model = model;
        }
        public bool Execute(string[] args, string results)
        {
            try
            {
                ConfigFileData settings = JsonConvertor.ConvertToConfigFileData(results); // <- שורה בעייתית, עבדה קודם וכעת זורקת אקספשיין 
                this.model.OutputDirectory = settings.OutputDir;
                this.model.ServiceLogName = settings.LogName;
                this.model.ServiceSourceName = settings.SourceDir;
                this.model.ThumbSize = settings.ThumnailSize.ToString();
                foreach(string handler in settings.InputDirs)
                {
                    this.model.DirectoriesCollection.Add(handler);
                    int a = settings.InputDirs.Length;
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
