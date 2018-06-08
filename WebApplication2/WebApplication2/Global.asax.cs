using Infrustructure;
using ServiceGuiComunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication2.Commands;
using WebApplication2.Models;

namespace WebApplication2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ImageWebModel model = ImageWebModel.GetModel();
            CommandsController cc = new CommandsController(model);
            ComunicationClient client = ComunicationClient.GetClient(8000);
            client.CommandReceived += delegate (object senderObj, CommandReceivedEventArgs args)
            {
                JsonCommand jCommand = args.JsonCommand;
                cc.ExecuteCommand(jCommand.CommandID, jCommand.Args, jCommand.JsonData);
            };
            try
            {
                client.ConnectToServer();
                client.sendCommand((int)CommandsEnum.GetConfigCommand, null);
                client.sendCommand((int)CommandsEnum.LogsCommand, null);
            }
            catch
            {
                Console.WriteLine("failed to connect to server");
            }
        }
    }
}
