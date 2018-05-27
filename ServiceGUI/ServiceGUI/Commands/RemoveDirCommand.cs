using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.Models;
namespace ServiceGUI.Commands
{
    /// <summary>
    /// a command class that removes from the GUI listbox chosen handler to remove.
    /// </summary>
    public class RemoveDirCommand: ICommand
    {
        private ISettingsModel model;
        /// <summary>
        /// the constructor
        /// </summary>
        public RemoveDirCommand(ISettingsModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// executes the command
        /// <param name= args> command's args </param>
        /// <param name= results> the handler to remove </param>
        /// <return> true if successful, false for exception
        /// </summary>
        public bool Execute(string[] args, string results)
        {
            try
            {
                if (args.Length != 1)
                {
                    return false;
                }
                //removes from the gui list the handler that was removed.
                model.DirectoriesCollection.Remove(args[0]);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
