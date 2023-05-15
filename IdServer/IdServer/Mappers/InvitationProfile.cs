using AutoMapper;
using IdServer.Db.Models;
using IdServer.Dtos;

namespace IdServer.Mappers;

public class InvitationProfile : Profile
{
    public InvitationProfile()
    {
        
        CreateMap<InvitationEntity, InvitationDto>();
    }
}