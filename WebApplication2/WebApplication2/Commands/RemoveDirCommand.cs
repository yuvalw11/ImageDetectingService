using Infrustructure;
using ServiceGuiComunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Commands
{
    public class RemoveDirCommand : ICommand
    {
        private ImageWebModel model;

        public RemoveDirCommand(ImageWebModel model)
        {
            this.model = model;
        }

        public bool Execute(string[] args, string results)
        {
            //removes from the gui list the handler that was removed.
            for (int i = 0; i < model.InputDirs.Length; i++)
            {
                if (model.InputDirs[i].Equals(args[0].ToString()))
                {
                    List<string> list = model.InputDirs.ToList();
                    list.Remove(model.InputDirs[i]);
                    model.InputDirs = list.ToArray();
                }
            }
            return true;
        }
    }
}