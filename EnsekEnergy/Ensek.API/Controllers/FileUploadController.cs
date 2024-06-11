using Ensek.DomainModels.Model.Response;
using Ensek.Service;
using Ensek.Service.Model;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace Ensek.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : Controller
    {
        private readonly IUploadFileService _uploadFile;

        public FileUploadController(IUploadFileService uploadFile)
        {
            _uploadFile = uploadFile;
        }
        [ProducesResponseType(typeof(ValidationResultModel), (int)HttpStatusCode.OK)]
        [HttpPost("[action]")]
       public async Task<IActionResult> MeterReadingUploads([FromForm] UploadFileRequest file)
        {
            var response = await _uploadFile.UploadData(file);
            return Ok(response);
        }

    }
    
}
