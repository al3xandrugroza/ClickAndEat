using AutoMapper;
using ClickAndEatApi.Db.Models;
using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Profiles;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<ShoppingCartDto, ShoppingCartEntity>();
        CreateMap<ShoppingCartEntity, ShoppingCartDto>();
    }
}