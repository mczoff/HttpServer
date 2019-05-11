using HTTPServer.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer.Core.Model
{
    public class HttpClient
        : IHttpClient
    {
        readonly TcpClient _tcpClient;

        public HttpClient(TcpClient client)
        {
            _tcpClient = client;
        }
    }
}
