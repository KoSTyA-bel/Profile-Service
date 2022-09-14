using ProfilerService.BLL.Entities;

namespace ProfilerService.Grpc;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Service.Grpc.CreateProfileRequest, Profile>()
            .ForMember(dest => dest.DiscrodId, opt => opt.MapFrom(src => src.DiscordId));

        CreateMap<string, Guid>()
            .ConvertUsing((x, res) => res = Guid.TryParse(x, out var id) ? id : Guid.Empty);
        CreateMap<Guid?, string>()
            .ConvertUsing((x, res) => res = x?.ToString() ?? string.Empty);

        CreateMap<string, DateTime>()
            .ConvertUsing((x, res) => res = DateTime.TryParse(x, out var dateTime) ? dateTime : DateTime.MinValue);
        CreateMap<DateTime, string>()
            .ConvertUsing((x, res) => res = x.ToString());

        CreateMap<Profile, Service.Grpc.Profile>()
            .ForMember(dest => dest.DiscordId, opt => opt.MapFrom(src => src.DiscrodId))
            .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points))
            .ForMember(dest => dest.WaxWallet, opt => opt.MapFrom(src => src.WaxWallet))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.LoseCount, opt => opt.MapFrom(src => src.LoseCount))
            .ForMember(dest => dest.WinCount, opt => opt.MapFrom(src => src.WinCount))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();
    }
}
