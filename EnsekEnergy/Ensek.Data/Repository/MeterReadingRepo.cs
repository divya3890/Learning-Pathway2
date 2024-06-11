using Ensek.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensek.Data.Repository
{
    public class MeterReadingRepo : IMeterReadingRepo
    {
        private readonly EnsekDbContext _dbContext;
        public MeterReadingRepo(EnsekDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<MeterReading> GetAll()
        {
            return _dbContext.MeterReadings;

        }

        public void Insert(List<MeterReading> meterReadings)
        {
            _dbContext.MeterReadings.AddRange(meterReadings);
            _dbContext.SaveChanges();
        }
    }
}
