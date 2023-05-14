using AutoMapper;
using ClickAndEatApi.Db.Models;
using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Profiles;

public class FoodTypeProfile : Profile
{
    public FoodTypeProfile()
    {
        CreateMap<FoodTypeEntity, FoodTypeDto>();
        CreateMap<FoodTypeDto, FoodTypeEntity>();

        CreateMap<FoodTypeCreateRequestDto, FoodTypeEntity>();
    }
}