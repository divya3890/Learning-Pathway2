using Ensek.Data.Models;
using Ensek.Data.Repository;
using Ensek.DomainModels.Model.DTOs;
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
    public class UploadFileService : IUploadFileService
    {
        private readonly IValidate _validate;
        private readonly IMeterReadingRepo _meterReadingRepo;
        private readonly IAccountRepo _accountRepo;
        public UploadFileService(IValidate validate, IMeterReadingRepo meterReadingRepo, IAccountRepo accountRepo)
        {
            _validate = validate;
            _meterReadingRepo = meterReadingRepo;
            _accountRepo = accountRepo;
        }

        public async Task<ValidationResultModel> UploadData(UploadFileRequest file)
        {
            var uploadFile = ReadFile(file);
            var meterReadingList = new List<MeterReading>();
            var dbMeterReading = _meterReadingRepo.GetAll();
            var dbAccountId = _accountRepo.GetAll();
            var validationResult = _validate.ValidateUpload(uploadFile, dbMeterReading, dbAccountId, ref meterReadingList);
            _meterReadingRepo.Insert(meterReadingList);
            return validationResult;
        }

        private List<MeterReadingDTO> ReadFile(UploadFileRequest file)
        {
            using (var sr = new StreamReader(file.FileContent.OpenReadStream(), Encoding.UTF8))
            {
                string headerLine = sr.ReadLine();
                string line = String.Empty;
                var meterReadingList = new List<MeterReadingDTO>();

                while ((line = sr.ReadLine()) != null)
                {
                    var lineItems = line.Split(',');

                    meterReadingList.Add(new MeterReadingDTO
                    {
                        AccountId = lineItems[0],
                        MeterReadingDateTime = lineItems[1],
                        MeterReadValue = lineItems[2]
                    });

                }
                return meterReadingList;
            }
        }
    }
}
