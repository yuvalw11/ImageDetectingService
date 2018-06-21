using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ImageService.Modal;
using Infrustructure;

namespace ImageService.ImageService
{
    public class TCPServer: ITCPServer
    {
        int Port { get; set; }
        ILoggingService Ils { get; set; }
        ITCPHandler TCPHandler { get; set; }
        TcpListener Listener { get; set; }
        private List<TcpClient> clients = new List<TcpClient>();

        public TCPServer(int port, ILoggingService ils, ITCPHandler tcpHandler)
        {
            this.Port = port;
            this.Ils = ils;
            this.TCPHandler = tcpHandler;
            
        }
        //starts running the tcpServer
        public void Start()
        {
            try
            {
                IPEndPoint endPoint = new
                IPEndPoint(IPAddress.Parse("10.0.0.2"), Port);
                Listener = new TcpListener(endPoint);

                Listener.Start();
                Ils.Log("Waiting for connection from client...", MessageTypeEnum.INFO);
                Task task = new Task(() =>
                {
                    while (true)
                    {
                        try
                        {
                            //tries to accept a tcp client
                            TcpClient tcpClient = Listener.AcceptTcpClient();
                            Ils.Log("client connection found", MessageTypeEnum.INFO);
                            clients.Add(tcpClient);
                            TCPHandler.HandleTcpClient(tcpClient, clients);
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                    Ils.Log("TCP server has stopped", MessageTypeEnum.INFO);
                });
                task.Start();
            }
            catch (Exception e)
            {
                //fail message upon failure
                Ils.Log(e.ToString(), MessageTypeEnum.FAIL);
            }
        }
    }
}
