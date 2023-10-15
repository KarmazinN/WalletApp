using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WalletApp.Domain.Enum;

namespace WalletApp.Application.Models
{
    public class TransactionsListModel
    {
        public List<TransactionViewModel> LatestTransactions { get; set; } = new List<TransactionViewModel>();
    }

    public class BloсsModel
    {
        public double CardBalance { get; set; }
        public double Available { get; set; }
        public string? NoPaymentDue { get; set; }
        public int DailyPoints { get; set; }
    }

    public class TransactionBaseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? SenderName { get; set; }
    }

    public class TransactionViewModel : TransactionBaseModel
    {
        public string? DateTransaction { get; set; }
        public string? Icon { get; set; }
        public string? Sum { set; get; }

    }

    public class CreateTransactionModel : TransactionBaseModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public double Sum { set; get; }
        public bool IsPending { set; get; }
        public TransactionType Type { get; set; }
        public int? IconId { get; set; }
    }

}
