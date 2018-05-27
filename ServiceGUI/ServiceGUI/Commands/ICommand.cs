using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Commands
{
    /// <summary>
    /// an interface for GUI commands.
    /// </summary>
    public interface ICommand
    {
        bool Execute(string[] args, string results);          // The Function That will Execute The 
    }
}
