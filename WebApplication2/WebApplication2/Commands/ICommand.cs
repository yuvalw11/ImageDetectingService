using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Commands
{
    public interface ICommand
    {
        bool Execute(string[] args, string results);          // The Function That will Execute The 
    }
}