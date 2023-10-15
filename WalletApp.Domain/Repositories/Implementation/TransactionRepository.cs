using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.Domain.DataBase;
using WalletApp.Domain.DbModels;
using WalletApp.Domain.Repositories.Interface;

namespace WalletApp.Domain.Repositories.Implementation
{
    public class TransactionRepository : DbRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationsContext context) : base(context)
        {
        }
    }
}
