﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ServiceGuiComunication
{
    //interface for classes that handle clients
    public interface IClientHandler
    {
        //function to handle the client
        void HandleClient(TcpClient client);
    }
}
