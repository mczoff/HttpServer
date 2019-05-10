using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class ErrorAttribute : Attribute
    {
        public string Description { get; set; }
        public HttpStatusCode HttpCode { get; set; }

        public ErrorAttribute(HttpStatusCode statusCode, string description)
        {
            this.Description = description;
            this.HttpCode = statusCode;
        }
    }
}
