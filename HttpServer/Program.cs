using HTTPServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpServerContext httpServerContext = new HttpServerContext("http://localhost:8888/");
            await httpServerContext.StartAsync();
        }
    }
}
