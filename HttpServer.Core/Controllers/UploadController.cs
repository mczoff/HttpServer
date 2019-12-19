using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using HTTPServer.Core.Abstractions;
using HTTPServer.Core.Attributes;
using HTTPServer.Core.Exceptions;
using HTTPServer.Core.Model;
using HTTPServer.Core.Repositories.Models;
using HTTPServer.Core.Services;
using Newtonsoft.Json;

namespace HTTPServer.Core.Controllers
{
    [Controller("upload")]
    public class UploadController
    {
        private string _path = "./files";

        private ExtensionService _extensionService;
        
        public UploadController()
        {
            _extensionService = new ExtensionService();
        }
        
        
        [Page(@"index")]
        public IHttpJsonResponse Post([PostData]PostModel data)
        {
            string extension = _extensionService.GetExtension(data.AdditionalParts["ContentType"]);
            
            if(extension == null)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest);

            string newFileName = $"{Guid.NewGuid().ToString()}{extension}";
            string newFilePath = Path.Combine(_path, newFileName);
            
            File.WriteAllBytes(newFilePath, data.Data);
            
            return new HttpJsonResponse();
        }
    }
}