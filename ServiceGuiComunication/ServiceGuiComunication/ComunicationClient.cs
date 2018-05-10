using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGuiComunication
{
    class ComunicationClient
    {
        private IPEndPoint ep;
        private TcpClient client;

        public ComunicationClient(int port)
        {
            this.ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            this.client = new TcpClient();
        }

        public void startClient()
        {

        }

        public void sendCommand(int commandID, string[] args)
        {
            JsonCommand command = new JsonCommand(commandID, args, false, "");
            client.Connect(ep);
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(JsonConvertor.GenerateJsonCommandString(command));
            string result = reader.ReadString();
            command = JsonConvertor.GenerateJsonCommandObject(result);
            Console.WriteLine(command.JsonData);
        }
    }
}
