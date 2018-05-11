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
        List<BinaryWriter> writers;

        public ClientHandler(IImageController controller, List<BinaryWriter> writers)
        {
            this.controller = controller;
            this.writers = writers;
        }

        public void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            new Task(() =>
            {
                this.writers.Add(writer);
                string commandLine = reader.ReadString();
                bool result;
                JsonCommand command = JsonConvertor.GenerateJsonCommandObject(commandLine);
 
                try
                {
                    while (command.CommandID != (int)CommandsEnum.CloseCommand)
                    {
                        string message = this.controller.ExecuteCommand(command.CommandID, command.Args, out result);
                        command.JsonData = message;
                        command.Result = result;
                        writer.Write(JsonConvertor.GenerateJsonCommandString(command));

                        commandLine = reader.ReadString();
                        command = JsonConvertor.GenerateJsonCommandObject(commandLine);
                    }
                }
                catch(Exception e)
                {
                    this.writers.Remove(writer);
                    return;
                }
                this.writers.Remove(writer);
                client.Close();
                
            }).Start();
        }

    }
   
}
