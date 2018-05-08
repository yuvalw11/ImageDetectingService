using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace ServiceGuiComunication
{
    class ClientHandler : IClientHandler
    {
        public void HandleClient(TcpClient client)
        {
            new Task(() => {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);
                
                string commandLine = reader.ReadLine()
                JsonCommand command = JsonConvertor.GenerateJsonCommandObject(commandLine);
                string result = "";//ExecuteCommand(commandLine, client);
                writer.Write(result);
                
                client.Close();
            }).Start();
        }
    }
   
}
