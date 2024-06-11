using Ensek.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensek.Data.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly EnsekDbContext _dbContext;
        public AccountRepo(EnsekDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Account> GetAll()
        {
            return _dbContext.Accounts;

        }
    }
}
