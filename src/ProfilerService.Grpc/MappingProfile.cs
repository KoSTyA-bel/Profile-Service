using ProfilerService.BLL.Entities;

namespace ProfilerService.Grpc;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Service.Grpc.GetByDiscordIdRequest, ulong>()
            .ForMember(dest => dest, opt => opt.MapFrom(src => ulong.Parse(src.DiscordId)));
        CreateMap<Service.Grpc.CreateProfileRequest, Profile>()
            .ForMember(dest => dest.DiscrodId, opt => opt.MapFrom(src => ulong.Parse(src.DiscordId)));
        CreateMap<Service.Grpc.DeleteUserRequest, ulong>()
            .ForMember(dest => dest, opt => opt.MapFrom(src => ulong.Parse(src.DiscordId)));
        CreateMap<Service.Grpc.Profile, Profile>().ReverseMap();
    }
}
