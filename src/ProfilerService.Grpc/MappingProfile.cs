using ProfilerService.BLL.Entities;

namespace ProfilerService.Grpc;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Service.Grpc.GetByDiscordIdRequest, ulong>()
            .ConvertUsing(src => ulong.Parse(src.DiscordId));
        CreateMap<Service.Grpc.CreateProfileRequest, Profile>()
            .ForMember(dest => dest.DiscrodId, opt => opt.MapFrom(src => ulong.Parse(src.DiscordId)));
        CreateMap<Service.Grpc.DeleteUserRequest, ulong>()
            .ConvertUsing(src => ulong.Parse(src.DiscordId));
        CreateMap<Profile, Service.Grpc.Profile>()
            .ForMember(dest => dest.DiscordId, opt => opt.MapFrom(src => src.DiscrodId.ToString()));
    }
}
