﻿using ImageService.Commands;
using ImageService.ImageService.Infrastructure;
using ImageService.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService.Commands
{
    //request from the client to close a directory handler.
    public class RemoveDirCommand : ICommand
    {
        private ImageServer imse;

        public RemoveDirCommand(ImageServer imse)
        {
            this.imse = imse;
        }

        //argrs[0] is the dircetory to close.
        public string Execute(string[] args, out bool result)
        {
            this.imse.closeHandler(args[0]);
            string[] paths = ReadAppConfig.ReadSetting("Handlers").Split(';');
            paths = paths.Where(val => val != args[0]).ToArray();
            string newPaths = String.Join(";", paths);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["Handlers"].ConnectionString = newPaths;
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");

            result = true;
            return args[0];
        }
    }
}
