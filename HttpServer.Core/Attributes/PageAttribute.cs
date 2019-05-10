using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HTTPServer.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class PageAttribute : Attribute
    {
        public PageAttribute(string urlPattern)
        {
            if (string.IsNullOrWhiteSpace(urlPattern))
                throw new ArgumentException();

            this.UrlRegex = urlPattern;
        }

        public string UrlRegex { get; }

        public bool ValidateUrlParams(string[] urlParams)
        {
            string url = urlParams.Aggregate(string.Empty, (t, k) => t + k);
            return new Regex(this.UrlRegex, RegexOptions.IgnoreCase).IsMatch(url);
        }
            
    }
}
