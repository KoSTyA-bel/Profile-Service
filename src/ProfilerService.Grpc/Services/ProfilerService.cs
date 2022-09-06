using AutoMapper;
using Grpc.Core;
using ProfilerService.BLL.Interfaces;
using Service.Grpc;
using BusinessModels = ProfilerService.BLL.Entities;

namespace ProfilerService.Grpc.Services;

public class ProfilerService : Service.Grpc.ProfilerService.ProfilerServiceBase
{
    private readonly IProfileService _service;
    private readonly IMapper _mapper;

    public ProfilerService(IProfileService service, IMapper mapper)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public override async Task<CreateProfileResponse> CreateProfile(CreateProfileRequest request, ServerCallContext context)
    {
        var profile = _mapper.Map<BusinessModels.Profile>(request);

        await _service.Create(profile);

        return new CreateProfileResponse();
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var discordId = _mapper.Map<ulong>(request.DiscordId);

        await _service.Delete(discordId);

        return new DeleteUserResponse();
    }

    public override async Task<DepositPointsResponse> DepositPoints(DepositPointsRequest request, ServerCallContext context)
    {
        // TODO: magic parse
        var res = await _service.DepositPoints(ulong.Parse(request.DiscordId), request.Points);

        return new DepositPointsResponse()
        {
            Status = res ? StatusType.Success : StatusType.Failed,
        };
    }

    public override async Task<GetByDiscordIdResponse> GetByDiscordId(GetByDiscordIdRequest request, ServerCallContext context)
    {
        var discordId = _mapper.Map<ulong>(request);

        var profile = await _service.GetByDiscordId(discordId);

        var grpcProfile = _mapper.Map<Service.Grpc.Profile>(profile);

        return new GetByDiscordIdResponse()
        {
            Profile = grpcProfile,
        };
    }

    public override async Task<GetRangeOfProfilesResponse> GetRangeOfProfiles(GetRangeOfProfilesRequest request, ServerCallContext context)
    {
        var profiles = await _service.GetProfiles(request.StartPosition, request.Count);

        var grpcProfiles = new List<Service.Grpc.Profile>();
        grpcProfiles.AddRange(_mapper.Map<List<Service.Grpc.Profile>>(profiles));

        var response = new GetRangeOfProfilesResponse();
        response.Profiles.AddRange(grpcProfiles);

        return response;
    }

    public override async Task<WithdrawPointsResponse> WithdrawPoints(WithdrawPointsRequest request, ServerCallContext context)
    {
        // todo magic
        var res = await _service.WithdrawPoints(ulong.Parse(request.DiscordId), request.Points);

        return new WithdrawPointsResponse()
        {
            Status = res ? StatusType.Success : StatusType.Failed,
        };
    }
}
