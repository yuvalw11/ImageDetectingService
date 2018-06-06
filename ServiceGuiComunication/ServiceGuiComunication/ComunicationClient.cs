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
    //the class defines a client to connect to a remote server
    public class ComunicationClient
    {
        private IPEndPoint ep;
        private TcpClient client;
        private BinaryReader reader;
        private BinaryWriter writer;
        private bool connected;

        public event EventHandler<CommandReceivedEventArgs> CommandReceived; // any time the client receives a command the event is invoked.
        private static ComunicationClient comunicationClient = null; //the only client, ComunicationClient is a songelton

        //get the singelton client
        public static ComunicationClient GetClient(int port)
        {
            if (ComunicationClient.comunicationClient == null)
            {
                ComunicationClient.comunicationClient = new ComunicationClient(port);
            }
            return ComunicationClient.comunicationClient;
        }

        //private constructor for client
        private ComunicationClient(int port)
        {
            this.ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            this.client = new TcpClient();
           
        }

        //the function connects the client to the server
        //it creates a new thread to receieve commands constently and invoke commandReceived
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
                    this.connected = true;
                    while (true)
                    {
                        string output = this.reader.ReadString();
                        this.CommandReceived?.Invoke(this, new CommandReceivedEventArgs(JsonConvertor.GenerateJsonCommandObject(output)));
                    }
                    
                }
                catch(Exception e)
                {
                    this.connected = false;
                }
            });
            task.Start();
        }

        public bool isConnected()
        {
            return this.connected;
        }

        //this func sends a command to the server, result will come later in CommandReceived 
        public void sendCommand(int commandID, string[] args)
        {
            JsonCommand command = new JsonCommand(commandID, args, false, "");
            this.writer.Write(JsonConvertor.GenerateJsonCommandString(command));
        }
    }
}
