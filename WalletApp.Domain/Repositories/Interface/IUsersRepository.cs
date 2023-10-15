using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.Domain.Repositories.Interface;
using WalletApp.Domain.DataBase;
using WalletApp.Domain.DbModels;
using WalletApp.Domain.Repositories.Implementation;

namespace WalletApp.Domain.Repositories.Interface
{
    public interface IUsersRepository : IDbRepository<User>
    {
    }
}
