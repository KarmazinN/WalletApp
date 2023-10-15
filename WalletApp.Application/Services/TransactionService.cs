using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WalletApp.Application.Interfaces;
using WalletApp.Application.Models;
using WalletApp.Domain;
using WalletApp.Domain.DbModels;

namespace WalletApp.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public TransactionService(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task CreateTransactionAsync(CreateTransactionModel model)
        {
            var newTransaction = _mapper.Map<Transaction>(model);

            await _uow.Transaction.InsertAsync(newTransaction);

            await _uow.SaveAsync();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _uow.Transaction.GetFirstAsync(t => t.Id == id);

            if (transaction is null)
                throw new Exception();

            _uow.Transaction.Delete(transaction);

            await _uow.SaveAsync();
        }

        public async Task<TransactionsListModel> GetTransactionsAsync(string userId)
        {
            var userTransactions = await _uow.Transaction.GetListAsync(x => x.UserId.ToString() == userId);

            return new TransactionsListModel
            {
                LatestTransactions = _mapper.Map<List<TransactionViewModel>>(userTransactions
                                            .OrderByDescending(x => x.Id)
                                            .Take(10))
            };
        }

        public BloсsModel GetPaymentValues()
        {
            var (cardBalance, available) = GenerateRandBalence();

            return new BloсsModel
            {
                CardBalance = cardBalance,
                Available = available,
                NoPaymentDue = $"You’ve paid your {DateTime.UtcNow:MMM} balance",
                DailyPoints = CalculateDailyPoints(),
            };
        }

        public async Task<TransactionViewModel> GetTransactionByIdAsync(int id)
        {
            return await _uow.Transaction.CustomQuery().Include(x => x.Icon).Where(x => x.Id == id)
                .ProjectTo<TransactionViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                ?? throw new Exception();
        }

        public static int CalculateDailyPoints()
        {
            DateTime currentDate = DateTime.Now;

            DateTime[] startOfSeasons = {
            new DateTime(currentDate.Year, 3, 1),  // Spring
            new DateTime(currentDate.Year, 6, 1),  // Summer
            new DateTime(currentDate.Year, 9, 1),  // Autumn
            new DateTime(currentDate.Year, 12, 1)  // Winter
        };

            int dayOfYear = currentDate.DayOfYear;

            int currentSeasonIndex = 0;
            for (int i = 0; i < startOfSeasons.Length; i++)
            {
                if (currentDate >= startOfSeasons[i])
                {
                    currentSeasonIndex = i;
                }
            }

            int points = 0;

            if (dayOfYear == startOfSeasons[currentSeasonIndex].DayOfYear)
            {
                points = 2;
            }
            else if (dayOfYear == startOfSeasons[currentSeasonIndex].DayOfYear + 1)
            {
                points = 3;
            }
            else
            {
                points = (int)Math.Round(2 * Math.Pow(1.6, dayOfYear - startOfSeasons[currentSeasonIndex].DayOfYear - 2));
                if (points >= 1000)
                {
                    points = points / 1000;
                }
            }

            return points;
        }

        private (double, double) GenerateRandBalence()
        {
            Random rnd = new Random();
            double cardBalance = rnd.NextDouble() * 1500.0;
            double cardLimit = 1500.0;
            double available = cardLimit - cardBalance;
            return (Math.Round(cardBalance, 2), Math.Round(available, 2));
        }
    }
}
