using HTTPServer.Core.Abstractions;
using HTTPServer.Core.Attributes;
using HTTPServer.Core.Exceptions;
using HTTPServer.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer.Core.Controllers
{
    [Controller("values")]
    public class ValuesController
    {
        [Page(@"[0-9]+")]
        public IHttpJsonResponse Get(int i)
        {
            return new HttpJsonResponse { Data = $"<html><head><meta charset='utf8'></head><body>Привет {i} мир!</body></html>" };
        }

        [Page(@"index")]
        public IHttpJsonResponse GetIndex()
        {
            //var methodsInfo = this.GetType().GetMethods();
            //return new HttpJsonResponse { Data =  JsonConvert.SerializeObject(methodsInfo.Select(t => new { Name = t.Name, ReturnType = t.ReturnType, Params = t.GetParameters().Select(k => $"{k.ParameterType.Name} {k.Name}") }), Formatting.Indented) };

            throw new HttpStatusCodeException(HttpStatusCode.NotFound);
        }

        [Error(HttpStatusCode.NotFound, "Page with current index not found")]
        public IHttpJsonResponse NotFound()
        {
            var myAttribute = GetType().GetMethod("NotFound").GetCustomAttributes(true).OfType<ErrorAttribute>().FirstOrDefault();
            return new HttpJsonResponse { Data = $"<html><head><meta charset='utf8'></head><body>{myAttribute.Description}</body></html>" };
        }
    }
}
