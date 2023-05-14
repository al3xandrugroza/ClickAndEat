using AutoMapper;
using ClickAndEatApi.Db.Models;
using ClickAndEatApi.Dtos;

namespace ClickAndEatApi.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDto, OrderEntity>();
        CreateMap<OrderEntity, OrderDto>();
    }
}