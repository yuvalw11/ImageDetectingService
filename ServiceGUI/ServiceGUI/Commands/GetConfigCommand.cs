using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.Models;
using ServiceGuiComunication;
namespace ServiceGUI.Commands
{
    /// <summary>
    /// a command class that gets from the server the appconfig
    /// </summary>
    public class GetConfigCommand : ICommand
    {
        private ISettingsModel model;

        /// <summary>
        ///the constructor
        /// </summary>
        public GetConfigCommand(ISettingsModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// executes the command
        /// <param name= args> command's args </param>
        /// <param name= results> a string containing the necessary info
        /// from the appconfig</param>
        /// <return> true if successful, false for exception
        /// </summary>
        public bool Execute(string[] args, string results)
        {
            try
            {
                //gets the appsettings in order to update the values of the gui view
                ConfigFileData settings = JsonConvertor.ConvertToConfigFileData(results); 
                this.model.OutputDirectory = settings.OutputDir;
                this.model.ServiceLogName = settings.LogName;
                this.model.ServiceSourceName = settings.SourceDir;
                this.model.ThumbSize = settings.ThumnailSize.ToString();
                foreach(string handler in settings.InputDirs)
                {
                    this.model.DirectoriesCollection.Add(handler);
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
