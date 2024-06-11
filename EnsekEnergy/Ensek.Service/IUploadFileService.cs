using Ensek.DomainModels.Model.Response;
using Ensek.Service.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensek.Service
{
    public interface IUploadFileService
    {
        Task<ValidationResultModel> UploadData(UploadFileRequest file);
    }
}
