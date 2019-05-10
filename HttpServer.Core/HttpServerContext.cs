using HTTPServer.Core.Abstractions;
using HTTPServer.Core.Attributes;
using HTTPServer.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using HttpClient = HTTPServer.Core.Model.HttpClient;

namespace HTTPServer.Core
{
    public class HttpServerContext
        : IHttpServerContext
    {
        readonly HttpListener _httpListener;
        readonly string _prefix;

        public HttpServerContext(string prefix)
        {
            _httpListener = new HttpListener();
            _prefix = prefix;
            _httpListener.Prefixes.Add(_prefix);
        }

        public int MaxThreadsCount { get; private set; }
        public int MinThreadsCount { get; private set; }

        public async Task StartAsync()
            => await this.StartAsync(CancellationToken.None);

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _httpListener.Start();

            while (true)
            {
                HttpListenerContext httpListenerContext = _httpListener.GetContext();

                Thread Thread = new Thread(new ParameterizedThreadStart(ClientThread));

                Thread.Start(httpListenerContext);
            }
        }

        private void ClientThread(object contextObj)
        {
            HttpListenerContext context = contextObj as HttpListenerContext;

            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string controllerName = request.Url.Segments[1].Replace("/", "");

            string[] urlParams = request.Url.Segments.Skip(2).Select(s => s.Replace("/", "")).ToArray();

            var controllerType = this.GetTypeController(controllerName);

            //TODO: Exception
            if (controllerType == null)
                return;

            var controller = Activator.CreateInstance(controllerType);

            var method = controllerType.GetMethods().FirstOrDefault(t => t.GetCustomAttribute<PageAttribute>()?.ValidateUrlParams(urlParams) ?? false);

            object[] @params = method.GetParameters().Select((p, i) => Convert.ChangeType(urlParams[i], p.ParameterType)).ToArray();
            object ret = default;

            try
            {
                ret = method.Invoke(controller, @params);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is HttpStatusCodeException)
                {
                    var errormethod = controllerType.GetMethods().FirstOrDefault(t => t.GetCustomAttribute<ErrorAttribute>()?.HttpCode == (ex.InnerException as HttpStatusCodeException).StatusCode);
                    ret = errormethod.Invoke(controller, null);
                }
            }

            byte[] buffer = Encoding.UTF8.GetBytes((ret as IHttpResponse).Data);

            // получаем поток ответа и пишем в него ответ
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // закрываем поток
            output.Close();
        }

        private Type GetTypeController(string controllerName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var controllerType = assembly.GetTypes().FirstOrDefault(t => string.Equals(t.GetCustomAttribute<ControllerAttribute>()?.Name, controllerName, StringComparison.OrdinalIgnoreCase));

            //404
            if (controllerType == null)
                return null;

            return controllerType;
        }
    }
}
