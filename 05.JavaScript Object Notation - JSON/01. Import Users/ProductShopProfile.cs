using AutoMapper;
using ProductShop.IO.Input;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {

        public ProductShopProfile()
        {
            CreateMap<UserInputModel, User>();
        }
    }
}
