using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.Application.Models;

namespace WalletApp.Application.Interfaces
{
    public interface ITransactionService
    {
        public BloсsModel GetPaymentValues();
        public Task CreateTransactionAsync(CreateTransactionModel model);
        public Task DeleteTransactionAsync(int id);
        public Task<TransactionsListModel> GetTransactionsAsync(string userId);
        public Task<TransactionViewModel> GetTransactionByIdAsync(int id);
    }
}
