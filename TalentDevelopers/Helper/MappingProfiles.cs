using AutoMapper;
using TalentDevelopers.Models;
using TalentDevelopers.ViewModels;

namespace TalentDevelopers.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Sales, SalesViewModelGet>();
            CreateMap<Sales, SalesViewModelPost>();
            CreateMap<Store, StoreViewModel>();

            CreateMap<CustomerViewModel, Customer>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<SalesViewModelGet, Sales>();
            CreateMap<SalesViewModelPost, Sales>();
            CreateMap<StoreViewModel, Store>();
        }
    }
}
