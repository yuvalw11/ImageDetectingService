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
        private BinaryReader reader;
        private BinaryWriter writer;

        public event EventHandler<CommandReceivedEventArgs> CommandReceived;

        public ComunicationClient(int port)
        {
            this.ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            this.client = new TcpClient();
           
        }

        public void ConnectToServer()
        {
            client.Connect(ep);
            NetworkStream stream = client.GetStream();
            this.reader = new BinaryReader(stream);
            this.writer = new BinaryWriter(stream);

            Task task = new Task(() =>
            {
                try
                {
                    while (true)
                    {
                        string output = this.reader.ReadString();
                        this.CommandReceived?.Invoke(this, new CommandReceivedEventArgs(JsonConvertor.GenerateJsonCommandObject(output)));
                    }
                }
                catch(Exception e)
                {

                }
            });
            task.Start();
        }

        public void sendCommand(int commandID, string[] args)
        {
            JsonCommand command = new JsonCommand(commandID, args, false, "");
            this.writer.Write(JsonConvertor.GenerateJsonCommandString(command));
        }
    }
}
