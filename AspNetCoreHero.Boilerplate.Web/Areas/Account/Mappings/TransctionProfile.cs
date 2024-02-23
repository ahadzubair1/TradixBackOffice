using AspNetCoreHero.Boilerplate.Application.Features.Transactions.Queries.GetAll;
using AspNetCoreHero.Boilerplate.Web.Areas.Account.Models;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Account.Mappings
{
    internal class TransctionProfile : Profile
    {
        public TransctionProfile()
        {
            CreateMap<GetAllTransactionsResponse, TransactionViewModel>().ReverseMap();
            //CreateMap<GetBrandByIdResponse, TransactionViewModel>().ReverseMap();
            //CreateMap<CreateBrandCommand, TransactionViewModel>().ReverseMap();
            //CreateMap<UpdateBrandCommand, TransactionViewModel>().ReverseMap();
        }
    }
}