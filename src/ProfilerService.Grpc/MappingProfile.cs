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
            .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points))
            .ForMember(dest => dest.WaxWallet, opt => opt.MapFrom(src => src.WaxWallet ?? string.Empty))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString()))
            .ForMember(dest => dest.LoseCount, opt => opt.MapFrom(src => src.LoseCount))
            .ForMember(dest => dest.WinCount, opt => opt.MapFrom(src => src.WinCount))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        CreateMap<Service.Grpc.Profile, Profile>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(dest => dest.DiscrodId, opt => opt.MapFrom(src => src.DiscordId))
            .ForMember(dest => dest.WaxWallet, opt => opt.MapFrom(src => src.WaxWallet))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Parse(src.CreationDate)))
            .ForMember(dest => dest.LoseCount, opt => opt.MapFrom(src => src.LoseCount))
            .ForMember(dest => dest.WinCount, opt => opt.MapFrom(src => src.WinCount))
            .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points));
    }
}
