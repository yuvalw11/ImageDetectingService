using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServiceGuiComunication;

namespace ServiceGuiComunication
{
    public class ComunicationClient
    {
        private IPEndPoint ep;
        private TcpClient client;

        public EventHandler<CommandReceivedEventArgs> CommandReceived;

        public ComunicationClient(int port)
        {
            this.ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            this.client = new TcpClient();
        }

        public void ConnectToServer()
        {
            client.Connect(ep);

            Task task = new Task(() =>
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    BinaryReader reader = new BinaryReader(stream);
                    BinaryWriter writer = new BinaryWriter(stream);
                    while (true)
                    {
                        string output = reader.ReadString();
                        this.CommandReceived?.Invoke(this, new CommandReceivedEventArgs(JsonConvertor.GenerateJsonCommandObject(output)));
                    }
                }
                catch(SocketException)
                {

                }
            });
            task.Start();
        }

        public JsonCommand sendCommand(int commandID, string[] args)
        {
            JsonCommand command = new JsonCommand(commandID, args, false, "");
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(JsonConvertor.GenerateJsonCommandString(command));
            try
            {
                string result = reader.ReadString();
                command = JsonConvertor.GenerateJsonCommandObject(result);
                return command;
            }
            catch (SocketException)
            {
                return null;
            }
        }
    }
}
