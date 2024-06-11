using Ensek.Data.Models;
using Ensek.DomainModels.Model.DTOs;
using Ensek.DomainModels.Model.Response;
using Ensek.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ensek.Service
{
    public class Validate : IValidate
    {
        public ValidationResultModel ValidateUpload(List<MeterReadingDTO> uploadFile, IEnumerable<MeterReading> dbMeterReading, IEnumerable<Account> dbAccountId, ref List<MeterReading> meterReadings)
        {
            ValidationResultModel validationResultModel = new ValidationResultModel();

            var toRemovedIds = new List<string>();
            ValidateFormat(uploadFile, ref validationResultModel, ref toRemovedIds);
            ValidateAccountIdAgainstDatabase(uploadFile, dbAccountId, ref validationResultModel, ref toRemovedIds);
            CheckDuplicateValues(uploadFile, ref validationResultModel, ref toRemovedIds);
            ValidateMeterReadings(uploadFile, ref validationResultModel, ref toRemovedIds);
            ValidateMeterReadingsDate(dbMeterReading, uploadFile, ref validationResultModel, ref toRemovedIds);
            uploadFile.RemoveAll(x => toRemovedIds.Distinct().Contains(x.AccountId.ToString()));

            meterReadings = uploadFile.Select(x => new MeterReading
            {
                AccountId = Convert.ToInt32(x.AccountId),
                MeterReadingDateTime = Convert.ToDateTime(x.MeterReadingDateTime),
                MeterReadValue = Convert.ToInt32(x.MeterReadValue),
            }).ToList();
            return validationResultModel;
        }


        public void CheckDuplicateValues(List<MeterReadingDTO> uploadFile, ref ValidationResultModel validationResultModel, ref List<string> removedIds)
        {
            var duplicates = uploadFile
           .GroupBy(m => new { m.AccountId, m.MeterReadingDateTime, m.MeterReadValue })
           .SelectMany(g => g.Skip(1))
           .ToList();

            if (duplicates.Any())
            {
                foreach (var duplicate in duplicates)
                {
                    validationResultModel.Errors.Add("Duplicates Record Found- " + duplicate.AccountId);
                    removedIds.Add(duplicate.AccountId);
                }

            }
        }

        public void ValidateMeterReadings(List<MeterReadingDTO> uploadFile, ref ValidationResultModel validationResultModel, ref List<string> removedIds)
        {
            foreach (var reading in uploadFile)
            {
                if (string.IsNullOrEmpty(reading.AccountId))
                {
                    validationResultModel.Errors.Add("Null Record Found- " + reading.AccountId);
                    removedIds.Add(reading.AccountId);
                }
            }
        }

        public void ValidateAccountIdAgainstDatabase(List<MeterReadingDTO> uploadFile, IEnumerable<Account> dbAccount, ref ValidationResultModel validationResultModel, ref List<string> removedIds)
        {
            var uploadAccountIds = uploadFile.Select(a => a.AccountId).Distinct();

            var dbAccountIds = dbAccount.Select(a => a.AccountId.ToString()).Distinct();

            var differences = uploadAccountIds.Except(dbAccountIds).ToList();
            if (differences.Any())
            {

                foreach (var difference in differences)
                {
                    validationResultModel.Errors.Add("Account is not existing in the system - " + difference);
                    removedIds.Add(difference);
                }

            }
        }
        public void ValidateFormat(List<MeterReadingDTO> uploadFile, ref ValidationResultModel validationResultModel, ref List<string> removedIds)
        {
            foreach (var value in uploadFile)
            {
                string pattern = @"^\d{5}$";

                if (!Regex.IsMatch(value.MeterReadValue, pattern))
                {
                    validationResultModel.Errors.Add("Invalid Meter Reading - " + value.MeterReadValue);
                    removedIds.Add(value.AccountId);
                }

                DateTime parsedDateTime;
                if (!DateTime.TryParse(value.MeterReadingDateTime, out parsedDateTime))
                {
                    validationResultModel.Errors.Add("Invalid Date Format - " + value.MeterReadValue);
                    removedIds.Add(value.AccountId);
                }

            }
        }
        public void ValidateMeterReadingsDate(IEnumerable<MeterReading> dbMeterReading, List<MeterReadingDTO> uploadFile, ref ValidationResultModel validationResultModel, ref List<string> removedIds)
        {
            foreach (var value in uploadFile)
            {
                var lastDate = dbMeterReading.OrderByDescending(o => o.MeterReadingDateTime).FirstOrDefault(x => x.AccountId == Convert.ToInt32(value.AccountId))?.MeterReadingDateTime;
                if (lastDate >= Convert.ToDateTime(value.MeterReadingDateTime))
                {
                    validationResultModel.Errors.Add("Meter Reading Date should not be old value - " + value.MeterReadingDateTime);
                    removedIds.Add(value.AccountId);
                }

            }
        }

    }
}
