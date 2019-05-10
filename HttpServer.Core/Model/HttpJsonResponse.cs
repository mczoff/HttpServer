using HTTPServer.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer.Core.Model
{
    public class HttpJsonResponse
        : IHttpJsonResponse
    {
        public string Data { get; set; }
    }
}
