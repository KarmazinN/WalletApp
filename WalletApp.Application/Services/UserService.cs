using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WalletApp.Application.Interfaces;
using WalletApp.Application.Models;
using WalletApp.Domain;
using WalletApp.Domain.DbModels;
using WalletApp.Domain.Repositories.Interface;

namespace WalletApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public UserService(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(CreateUserModel model)
        {
            var newUser = _mapper.Map<User>(model);

            await _uow.User.InsertAsync(newUser);

            await _uow.SaveAsync();
        }

        public async Task<List<User>> GetListOfUserAsync()
        {
            return await _uow.User.CustomQuery().ToListAsync();
        }
    }
}
