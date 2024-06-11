using Ensek.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensek.Data.Repository
{
    public interface IAccountRepo
    {
        IQueryable<Account> GetAll();
    }
}
