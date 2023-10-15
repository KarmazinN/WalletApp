using AutoMapper;
using WalletApp.Application.Models;
using WalletApp.Domain.DbModels;
using WalletApp.Domain.Enum;


namespace WalletApp.Application.Mapping
{
    public class MappingConfigurator : Profile
    {
        public MappingConfigurator()
        {
            CreateMap<Transaction, TransactionViewModel>()
                .ForMember(x => x.DateTransaction,
                    opt => opt.MapFrom(x => x.DateTransaction >= DateTime.UtcNow.AddDays(-7)
                            ? DateTime.UtcNow.ToString("dddd")
                            : x.DateTransaction.ToString("dd/MM/yy")))
                .ForMember(x => x.Sum,
                    opt => opt.MapFrom(x => x.Type == TransactionType.Payment
                            ? $"+{x.Sum}"
                            : $"-{x.Sum}"))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(x => x.IsPending == true
                            ? $"Pending - {x.Description}"
                            : x.Description))
                .ForMember(x => x.Icon,
                    opt => opt.MapFrom(x => x.Icon.IconUrl ?? null))
                    .ReverseMap();

            CreateMap<CreateTransactionModel, Transaction>()
                .ForMember(x => x.DateTransaction,
                    opt => opt.MapFrom(x => DateTime.UtcNow));

            CreateMap<CreateUserModel, User>();

            CreateMap<User, BaseModel>();

        }
    }
}
