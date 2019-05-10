using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPServer.Core
{
    public interface IHttpServerContext
    {
        Task StartAsync();
        Task StartAsync(CancellationToken cancellationToken);
    }
}
