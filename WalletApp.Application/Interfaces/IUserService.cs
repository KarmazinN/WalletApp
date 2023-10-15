using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.Application.Models;
using WalletApp.Domain.DbModels;

namespace WalletApp.Application.Interfaces
{
    public interface IUserService
    {
        public Task CreateUserAsync(CreateUserModel model);
        public Task<List<UserBaseModels>> GetListOfUserAsync();

    }
}
