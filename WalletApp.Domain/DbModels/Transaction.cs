using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.Domain.Enum;

namespace WalletApp.Domain.DbModels
{
    public class Transaction
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public TransactionType Type { get; set; }
        public DateTime DateTransaction { get; set; }
        public bool IsPending { get; set; }
        public string? RecipientName { get; set; }
        public int? IconId { get; set; }
        public double Sum { set; get; }
        public virtual Icon? Icon { get; set; }
        public virtual User? Owner { get; set; }
    }
}
