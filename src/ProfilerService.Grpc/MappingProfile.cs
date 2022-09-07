using ProfilerService.BLL.Entities;

namespace ProfilerService.Grpc;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Service.Grpc.CreateProfileRequest, Profile>()
            .ForMember(dest => dest.DiscrodId, opt => opt.MapFrom(src => src.DiscordId));
        CreateMap<Profile, Service.Grpc.Profile>()
            .ForMember(dest => dest.DiscordId, opt => opt.MapFrom(src => src.DiscrodId))
            .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points));
    }
}
