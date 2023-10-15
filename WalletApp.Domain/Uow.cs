using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.Domain.DataBase;
using WalletApp.Domain.Repositories.Interface;
using WalletApp.Domain.Repositories.Implementation;

namespace WalletApp.Domain
{
    public class Uou : IUow
    {
        private readonly ApplicationsContext _context;

        public Uou(ApplicationsContext context) => _context = context;

        private IUsersRepository _usersRepository;
        private ITransactionRepository _transactionRepository;


        public IUsersRepository User
        {
            get
            {
                _usersRepository ??= new UsersRepository(_context);

                return _usersRepository;
            }
        }

        public ITransactionRepository Transaction
        {
            get
            {
                _transactionRepository ??= new TransactionRepository(_context);

                return _transactionRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}


