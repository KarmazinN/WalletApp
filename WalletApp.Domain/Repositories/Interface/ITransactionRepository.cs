using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.Domain.Repositories.Implementation;
using WalletApp.Domain.DataBase;
using WalletApp.Domain.DbModels;

namespace WalletApp.Domain.Repositories.Interface
{
    public interface ITransactionRepository : IDbRepository<Transaction>
    {
    }
}
