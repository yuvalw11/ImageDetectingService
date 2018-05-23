using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure
{
    public enum CommandsEnum : int
    {
        NewFileCommand,
        GetConfigCommand,
        LogCommand,
        LogsCommand,
        RemoveDirCommand,
        CloseCommand
    }
}
