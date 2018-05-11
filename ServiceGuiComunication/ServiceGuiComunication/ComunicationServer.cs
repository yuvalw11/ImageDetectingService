using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Infrustructure;
using System.IO;

namespace ServiceGuiComunication
{
    public class ComunicationServer
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        private List<BinaryWriter> writers;

        public ComunicationServer(int port, IImageController controller)
        {
            this.port = port;
            this.writers = new List<BinaryWriter>();
            this.ch = new ClientHandler(controller, this.writers);
        }

        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();

            Task task = new Task(() => 
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }
        
        public void SendCommandToAllClients(int commandID, string[] args)
        {
            JsonCommand command = new JsonCommand(commandID, args, false, ""); //result and jsonData are irrelevant now
            foreach(BinaryWriter writer in this.writers)
            {
                writer.Write(JsonConvertor.GenerateJsonCommandString(command));
            }
        }

        public void Stop()
        {
            this.listener.Stop();
        }
    }
    
}
