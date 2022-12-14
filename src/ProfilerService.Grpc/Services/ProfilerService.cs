using AutoMapper;
using Grpc.Core;
using ProfileService.BLL.Interfaces;
using Service.Grpc;
using System.Diagnostics.CodeAnalysis;
using BusinessModels = ProfileService.BLL.Entities;

namespace ProfileService.Grpc.Services;

public class ProfilerService : Service.Grpc.ProfilerService.ProfilerServiceBase
{
    private readonly IProfileService _service;
    private readonly INFTVerifyService _waxWalletVerify;
    private readonly IMapper _mapper;

    public ProfilerService(IProfileService service, INFTVerifyService waxWalletVerify, IMapper mapper)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _waxWalletVerify = waxWalletVerify ?? throw new ArgumentNullException(nameof(waxWalletVerify));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public override async Task<CreateProfileResponse> CreateProfile(CreateProfileRequest request, ServerCallContext context)
    {
        var profile = _mapper.Map<BLL.Entities.Profile>(request);
        var cancellationToken = context.CancellationToken;

        var result = await _service.Create(profile, cancellationToken);
        var status = _mapper.Map<StatusType>(result);

        var response = new CreateProfileResponse()
        {
            Status = status,
        };

        return response;
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var discordId = _mapper.Map<ulong>(request.DiscordId);
        var cancellationToken = context.CancellationToken;

        var result = await _service.Delete(discordId, cancellationToken);
        var status = _mapper.Map<StatusType>(result);

        var response = new DeleteUserResponse()
        {
            Status = status,
        };

        return response;
    }

    public override async Task<DepositPointsResponse> DepositPoints(DepositPointsRequest request, ServerCallContext context)
    {
        var discordId = request.DiscordId;
        var pointsAmount = request.PointsAmount;
        var cancellationToken = context.CancellationToken;

        var result = await _service.DepositPoints(discordId, pointsAmount, cancellationToken);
        var status = _mapper.Map<StatusType>(result);

        var response = new DepositPointsResponse()
        {
            Status = status,
        };

        return response;
    }

    public override async Task<GetByDiscordIdResponse> GetByDiscordId(GetByDiscordIdRequest request, ServerCallContext context)
    {
        var discordId = request.DiscordId;
        var cancellationToken = context.CancellationToken;

        var profile = await _service.GetByDiscordId(discordId, cancellationToken);
        var grpcProfile = _mapper.Map<Service.Grpc.Profile>(profile);

        var response = new GetByDiscordIdResponse()
        {
            Profile = grpcProfile,
        };

        return response;
    }

    public override async Task<GetRangeOfProfilesResponse> GetRangeOfProfiles(GetRangeOfProfilesRequest request, ServerCallContext context)
    {
        var page = request.Page;
        var pageSize = request.PageSize;

        var profiles = await _service.GetProfiles(page, pageSize, context.CancellationToken);

        var grpcProfiles = new List<Service.Grpc.Profile>();
        grpcProfiles.AddRange(_mapper.Map<List<Service.Grpc.Profile>>(profiles));

        var response = new GetRangeOfProfilesResponse();
        response.Profiles.AddRange(grpcProfiles);

        return response;
    }

    public override async Task<WithdrawPointsResponse> WithdrawPoints(WithdrawPointsRequest request, ServerCallContext context)
    {
        var discordId = request.DiscordId;
        var pointsAmount = request.PointsAmount;

        var result = await _service.WithdrawPoints(discordId, pointsAmount, context.CancellationToken);
        var status = _mapper.Map<StatusType>(result);

        var response = new WithdrawPointsResponse()
        {
            Status = status,
        };

        return response;
    }

    public override async Task<LinkWaxWalletResponse> LinkWaxWallet(LinkWaxWalletRequest request, ServerCallContext context)
    {
        var discordId = request.DiscordId;
        var waxWallet = request.WaxWallet;
        var cancellationToken = context.CancellationToken;

        var profile = await _service.GetByDiscordId(discordId, cancellationToken);

        profile.WaxWallet = waxWallet;

        var result = _service.Update(profile, cancellationToken);
        var status = _mapper.Map<StatusType>(await result);

        var response = new LinkWaxWalletResponse()
        {
            Status = status,
        };

        return response;
    }

    public override async Task<VerifyNFTResponse> VerifyNFT(VerifyNFTRequest request, ServerCallContext context)
    {
        var discordId = request.DiscordId;
        var cancellationToken = context.CancellationToken;

        var profile = await _service.GetByDiscordId(discordId, cancellationToken);

        var result = _waxWalletVerify.VerifyWaxWallet(profile.WaxWallet, cancellationToken);
        var type = _mapper.Map<NFTType>(await result);

        var response = new VerifyNFTResponse()
        {
            Type = type,
        };

        return response;
    }

    public override async Task<ResetPointsResponse> ResetPoints(ResetPointsRequest request, ServerCallContext context)
    {
        var pointsAmount = request.PointsAmount;
        var cancellationToken = context.CancellationToken;

        var result = _service.ResetPoints(pointsAmount, cancellationToken);
        var status = _mapper.Map<StatusType>(await result);

        var response = new ResetPointsResponse()
        {
            Status = status,
        };

        return response;
    }

    public override async Task<GetLeaderBoardByPointsAmountResponse> GetLeaderBoardByPointsAmount(GetLeaderBoardByPointsAmountRequest request, ServerCallContext context)
    {
        var count = request.Count;
        var cancellationToken = context.CancellationToken;
        var profiles = _service.GetLeaderBoard(count, cancellationToken);
        var response = new GetLeaderBoardByPointsAmountResponse();

        response.Profiles.AddRange(_mapper.Map<IEnumerable<Service.Grpc.Profile>>(await profiles));

        return response;
    }

    public override async Task<VerifyNFTInProfilesResponse> VerifyNFTInProfiles(VerifyNFTInProfilesRequest request, ServerCallContext context)
    {
        var cancellationToken = context.CancellationToken;
        var profilesIds = request.DiscrodIds;
        var profilesWaxWallet = new List<string>();

        foreach (var discordId in profilesIds)
        {
            var profile = await _service.GetByDiscordId(discordId, cancellationToken);
            profilesWaxWallet.Add(profile.WaxWallet);
        }

        var result = await _waxWalletVerify.VerifyWaxWallets(profilesWaxWallet, cancellationToken);

        var response = new VerifyNFTInProfilesResponse();
        var mappedResult = _mapper.Map<IEnumerable<NFTType>>(result);

        response.NftTypes.AddRange(mappedResult);

        return response;
    }

    public override async Task<ResetBattleResultsResponse> ResetBattleResults(ResetBattleResultsRequest request, ServerCallContext context)
    {
        var winCount = request.WinCount;
        var loseCount = request.LoseCount;
        var cantellationToken = context.CancellationToken;

        var result = _service.ResetBattleResults(winCount, loseCount, cantellationToken);
        var status = _mapper.Map<StatusType>(await result);

        var response = new ResetBattleResultsResponse()
        {
            Status = status,
        };

        return response;
    }

    public override async Task<CountBattleResultResponse> CountBattleResult(CountBattleResultRequest request, ServerCallContext context)
    {
        var discrodId = request.DiscordId;
        var pointsAmount = request.PointsAmount;
        var battleExodus = _mapper.Map<BusinessModels.BattleExodus>(request.BattleExodus);
        var cancellationToken = context.CancellationToken;

        var result = await _service.CountBattleResult(discrodId, pointsAmount, battleExodus, cancellationToken);
        var status = _mapper.Map<StatusType>(result);

        var response = new CountBattleResultResponse()
        {
            Status = status,
        };

        return response;
    }
}
