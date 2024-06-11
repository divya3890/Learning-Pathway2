using Ensek.Data.Models;
using Ensek.DomainModels.Model.DTOs;
using Ensek.DomainModels.Model.Response;
using Ensek.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ensek.UnitTests
{
    public class ValidateUploadTests
    {
        [Fact]
        public void ValidateUpload_ValidData_ReturnsValidationResult()
        {
            // Arrange
            var uploadFile = new List<MeterReadingDTO>
            {
                new MeterReadingDTO { AccountId = "1", MeterReadingDateTime = "2023-01-01", MeterReadValue = "100" }
            };
            var dbMeterReadings = new List<MeterReading> { new MeterReading { AccountId = 1, MeterReadingDateTime = DateTime.Parse("2023-01-01"), MeterReadValue = 100 } }.AsQueryable();
            var dbAccounts = new List<Account> { new Account { AccountId = 1, FirstName = "Test", LastName = "Test" } }.AsQueryable();
            var meterReadingList = new List<MeterReading>();

            var validateService = new Validate();
            var validationResult = new ValidationResultModel();

            // Act
            validationResult = validateService.ValidateUpload(uploadFile, dbMeterReadings, dbAccounts, ref meterReadingList);

            // Assert
            Assert.NotNull(validationResult);

        }

        [Fact]
        public void ValidateFormat_InvalidMeterReadValue_AddError()
        {
            // Arrange
            var uploadFile = new List<MeterReadingDTO>
            {
                new MeterReadingDTO { AccountId = "1", MeterReadingDateTime = "2023-01-01", MeterReadValue = "10" }
            };
            var validationResultModel = new ValidationResultModel();
            var removedIds = new List<string>();
            var validateService = new Validate();

            // Act
            validateService.ValidateFormat(uploadFile, ref validationResultModel, ref removedIds);

            // Assert
            Assert.Contains(validationResultModel.Errors, e => e.Contains("Invalid Meter Reading"));
        }
        [Fact]
        public void ValidateFormat_InvalidDateFormat_AddError()
        {
            // Arrange
            var uploadFile = new List<MeterReadingDTO>
            {
                new MeterReadingDTO { AccountId = "1", MeterReadingDateTime = "invalid-date", MeterReadValue = "10000" }
            };
            var validationResultModel = new ValidationResultModel();
            var removedIds = new List<string>();
            var validateService = new Validate();

            // Act
            validateService.ValidateFormat(uploadFile, ref validationResultModel, ref removedIds);

            // Assert
            Assert.Contains(validationResultModel.Errors, e => e.Contains("Invalid Date Format"));
        }

        [Fact]
        public void CheckDuplicateValues_AddsError()
        {
            // Arrange
            var uploadFile = new List<MeterReadingDTO>
            {
                new MeterReadingDTO { AccountId = "1", MeterReadingDateTime = "2023-01-01", MeterReadValue = "10000" },
                new MeterReadingDTO { AccountId = "1", MeterReadingDateTime = "2023-01-01", MeterReadValue = "10000" }
            };
            var validationResultModel = new ValidationResultModel();
            var removedIds = new List<string>();
            var validateService = new Validate();

            // Act
            validateService.CheckDuplicateValues(uploadFile, ref validationResultModel, ref removedIds);

            // Assert
            Assert.Contains(validationResultModel.Errors, e => e.Contains("Duplicates Record Found"));
        }
        [Fact]
        public void ValidateMeterReadings_NullAccountId_AddError()
        {
            // Arrange
            var uploadFile = new List<MeterReadingDTO>
            {
                new MeterReadingDTO { AccountId = null, MeterReadingDateTime = "2023-01-01", MeterReadValue = "10000" }
            };
            var validationResultModel = new ValidationResultModel();
            var removedIds = new List<string>();
            var validateService = new Validate();

            // Act
            validateService.ValidateMeterReadings(uploadFile, ref validationResultModel, ref removedIds);

            // Assert
            Assert.Contains(validationResultModel.Errors, e => e.Contains("Null Record Found"));
        }
        [Fact]
        public void ValidateAccountIdAgainstDatabase_AddError()
        {
            // Arrange
            var uploadFile = new List<MeterReadingDTO>
            {
                new MeterReadingDTO { AccountId = "2", MeterReadingDateTime = "2023-01-01", MeterReadValue = "10000" }
            };
            var dbAccounts = new List<Account> { new Account { AccountId = 1, FirstName = "Test", LastName = "Test" } }.AsQueryable();
            var validationResultModel = new ValidationResultModel();
            var removedIds = new List<string>();
            var validateService = new Validate();

            // Act
            validateService.ValidateAccountIdAgainstDatabase(uploadFile, dbAccounts, ref validationResultModel, ref removedIds);

            // Assert
            Assert.Contains(validationResultModel.Errors, e => e.Contains("Account is not existing in the system"));
        }
        [Fact]
        public void ValidateMeterReadingsDate_OldMeterReadingDate_AddError()
        {
            // Arrange
            var uploadFile = new List<MeterReadingDTO>
            {
                new MeterReadingDTO { AccountId = "1", MeterReadingDateTime = "2022-01-01", MeterReadValue = "10000" }
            };
            var dbMeterReadings = new List<MeterReading> { new MeterReading { AccountId = 1, MeterReadingDateTime = DateTime.Parse("2023-01-01"), MeterReadValue = 100 } }.AsQueryable();
            var validationResultModel = new ValidationResultModel();
            var removedIds = new List<string>();
            var validateService = new Validate();

            // Act
            validateService.ValidateMeterReadingsDate(dbMeterReadings, uploadFile, ref validationResultModel, ref removedIds);

            // Assert
            Assert.Contains(validationResultModel.Errors, e => e.Contains("Meter Reading Date should not be old value"));
        }
    }

    

}
