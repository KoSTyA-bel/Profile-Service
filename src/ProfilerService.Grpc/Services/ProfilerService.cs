using AutoMapper;
using Grpc.Core;
using ProfilerService.BLL.Interfaces;
using Service.Grpc;
using BusinessModels = ProfilerService.BLL.Entities;

namespace ProfilerService.Grpc.Services;

public class ProfilerService : Service.Grpc.ProfilerService.ProfilerServiceBase
{
    private readonly IProfileService _service;
    private readonly IWaxWalletVerifyService _waxWalletVerify;
    private readonly IMapper _mapper;

    public ProfilerService(IProfileService service, IWaxWalletVerifyService waxWalletVerify, IMapper mapper)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _waxWalletVerify = waxWalletVerify ?? throw new ArgumentNullException(nameof(waxWalletVerify));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public override async Task<CreateProfileResponse> CreateProfile(CreateProfileRequest request, ServerCallContext context)
    {
        var profile = _mapper.Map<BusinessModels.Profile>(request);
        var result = await _service.Create(profile, context.CancellationToken);
        var response = new CreateProfileResponse();

        response.Status = _mapper.Map<StatusType>(result);

        return response;
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var discordId = _mapper.Map<ulong>(request.DiscordId);
        var result = await _service.Delete(discordId, context.CancellationToken);
        var response = new DeleteUserResponse();

        response.Status = _mapper.Map<StatusType>(result);

        return response;
    }

    public override async Task<DepositPointsResponse> DepositPoints(DepositPointsRequest request, ServerCallContext context)
    {
        ulong discordId = request.DiscordId;
        int pointsAmount = request.PointsAmount;
        var result = await _service.DepositPoints(discordId, pointsAmount, context.CancellationToken);
        var response = new DepositPointsResponse();

        response.Status = _mapper.Map<StatusType>(result);

        return response;
    }

    public override async Task<GetByDiscordIdResponse> GetByDiscordId(GetByDiscordIdRequest request, ServerCallContext context)
    {
        var discordId = request.DiscordId;
        var profile = await _service.GetByDiscordId(discordId, context.CancellationToken);
        var grpcProfile = _mapper.Map<Service.Grpc.Profile>(profile);
        var response = new GetByDiscordIdResponse()
        {
            Profile = grpcProfile,
        };

        return response;
    }

    public override async Task<GetRangeOfProfilesResponse> GetRangeOfProfiles(GetRangeOfProfilesRequest request, ServerCallContext context)
    {
        int page = request.Page;
        int pageSize = request.PageSize;

        var profiles = await _service.GetProfiles(page, pageSize, context.CancellationToken);

        var grpcProfiles = new List<Service.Grpc.Profile>();
        grpcProfiles.AddRange(_mapper.Map<List<Service.Grpc.Profile>>(profiles));

        var response = new GetRangeOfProfilesResponse();
        response.Profiles.AddRange(grpcProfiles);

        return response;
    }

    public override async Task<WithdrawPointsResponse> WithdrawPoints(WithdrawPointsRequest request, ServerCallContext context)
    {
        ulong discordId = request.DiscordId;
        int pointsAmount = request.PointsAmount;
        var result = await _service.WithdrawPoints(discordId, pointsAmount, context.CancellationToken);
        var response = new WithdrawPointsResponse();

        response.Status = _mapper.Map<StatusType>(result);

        return response;
    }

    public override async Task<VerifyWaxWalletResponse> VerifyWaxWallet(VerifyWaxWalletRequest request, ServerCallContext context)
    {
        var profile = _mapper.Map<BusinessModels.Profile>(request.Profile);
        var result = await _waxWalletVerify.VerifyWaxWallet(profile.WaxWallet, context.CancellationToken);
        var response = new VerifyWaxWalletResponse();

        if (result == BusinessModels.StatusType.Success)
        {
            await _service.Update(profile, context.CancellationToken);
        }

        response.Status = _mapper.Map<StatusType>(result);

        return response;
    }
}
