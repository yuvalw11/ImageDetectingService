using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.Models;
namespace ServiceGUI.Commands
{
    class CloseCommand : ICommand
    {
        private ISettingsModel model;
        public CloseCommand(ISettingsModel model)
        {
            this.model = model;
        }
        public bool Execute(string[] args, string results)
        {
            try
            {
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
