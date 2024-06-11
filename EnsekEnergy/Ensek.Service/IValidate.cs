using Ensek.Data.Models;
using Ensek.DomainModels.Model.DTOs;
using Ensek.DomainModels.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensek.Service
{
    public interface IValidate
    {
        ValidationResultModel ValidateUpload(List<MeterReadingDTO> file,IEnumerable<MeterReading> dbMeterReading, IEnumerable<Account> dbAccountId, ref List<MeterReading> meterReadings);
    }
}
