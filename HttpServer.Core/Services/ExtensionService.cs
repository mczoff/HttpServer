using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HTTPServer.Core.Services
{
    public class ExtensionService
    {
        private IDictionary<string, string> _extensionDictionary = new Dictionary<string, string>
        {
            { "application/json", ".json" }
        };
        
        public string GetExtension(string contentType)
        {
            _extensionDictionary.TryGetValue(contentType, out string extension);

            return !string.IsNullOrWhiteSpace(extension) ? extension : null;
        }
    }
}