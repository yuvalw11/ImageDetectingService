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
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);
                
                string commandLine = reader.ReadString();
                bool result;
                JsonCommand command = JsonConvertor.GenerateJsonCommandObject(commandLine);

                while (command.CommandID != (int)CommandsEnum.CloseCommand)
                {
                    string message = this.controller.ExecuteCommand(command.CommandID, command.Args, out result);
                    command.JsonData = message;
                    command.Result = result;
                    writer.Write(JsonConvertor.GenerateJsonCommandString(command));

                    commandLine = reader.ReadString();
                    command = JsonConvertor.GenerateJsonCommandObject(commandLine);
                }
                this.clients.Remove(client);
                client.Close();
                
            }).Start();
        }

    }
   
}
