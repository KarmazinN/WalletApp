using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.Domain.Repositories.Interface;

namespace WalletApp.Domain
{
    public interface IUow : IDisposable
    {
        IUsersRepository User { get; }

        ITransactionRepository Transaction { get; }

        Task<int> SaveAsync();
    }
}
