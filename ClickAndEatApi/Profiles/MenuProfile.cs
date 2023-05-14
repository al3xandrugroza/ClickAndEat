using AutoMapper;
using ClickAndEatApi.Db.Models;
using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Profiles;

public class MenuProfile : Profile
{
    public MenuProfile()
    {
        CreateMap<MenuDto, MenuEntity>();
        CreateMap<MenuEntity, MenuDto>();
    }
}