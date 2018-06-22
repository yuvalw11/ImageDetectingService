using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ImageService.ImageService
{
    public interface ITCPHandler
    {
        void HandleTcpClient(TcpClient client, List<TcpClient> clients);
    }
}
