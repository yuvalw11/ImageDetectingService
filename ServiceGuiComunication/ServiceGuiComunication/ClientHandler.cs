using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using Infrustructure;

namespace ServiceGuiComunication
{
    class ClientHandler : IClientHandler
    {
        IImageController controller;
        List<TcpClient> clients;

        public ClientHandler(IImageController controller, List<TcpClient> clients)
        {
            this.controller = controller;
            this.clients = clients;
        }

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                string commandLine = reader.ReadLine();
                bool result;
                JsonCommand command = JsonConvertor.GenerateJsonCommandObject(commandLine);
                if (command.CommandID == (int)CommandsEnum.CloseCommand)
                {
                    this.clients.Remove(client);
                }
                else
                {
                    string message = this.controller.ExecuteCommand(command.CommandID, command.Args, out result);
                    command.JsonData = message;
                    command.Result = result;
                    writer.Write(JsonConvertor.GenerateJsonCommandString(command));
                }
                client.Close();
            }).Start();
        }

    }
   
}
