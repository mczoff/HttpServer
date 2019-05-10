using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer.Core.Abstractions
{
    public interface IHttpResponse
    {
        string Data { get; set; }
    }
}
