using System.Collections.Generic;

namespace HTTPServer.Core.Model
{
    public class PostModel
    {
        public byte[] Data { get; set; }
        public IDictionary<string, string> AdditionalParts { get; set; }
    }
}