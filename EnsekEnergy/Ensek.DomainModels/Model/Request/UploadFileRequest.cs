using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensek.Service.Model
{
    public class UploadFileRequest
    {
        public string FileType { get; set; }
        public IFormFile FileContent { get; set; }
        public string FileName { get; set; }


    }
}
