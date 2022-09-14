﻿using AutoMapper;
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

        await _service.Create(profile, context.CancellationToken);

        return new CreateProfileResponse();
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var discordId = _mapper.Map<ulong>(request.DiscordId);

        await _service.Delete(discordId, context.CancellationToken);

        return new DeleteUserResponse();
    }

    public override async Task<DepositPointsResponse> DepositPoints(DepositPointsRequest request, ServerCallContext context)
    {
        ulong discordId = request.DiscordId;
        int points = request.Points;
        var res = await _service.DepositPoints(discordId, points, context.CancellationToken);

        return new DepositPointsResponse()
        {
            Status = (StatusType)res,
        };
    }

    public override async Task<GetByDiscordIdResponse> GetByDiscordId(GetByDiscordIdRequest request, ServerCallContext context)
    {
        var discordId = request.DiscordId;

        var profile = await _service.GetByDiscordId(discordId, context.CancellationToken);

        var grpcProfile = _mapper.Map<Service.Grpc.Profile>(profile);

        return new GetByDiscordIdResponse()
        {
            Profile = grpcProfile,
        };
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
        int points  = request.Points;

        var res = await _service.WithdrawPoints(discordId, points, context.CancellationToken);

        return new WithdrawPointsResponse()
        {
            Status = (StatusType)res,
        };
    }

    public override async Task<VerifyWaxWalletResponse> VerifyWaxWallet(VerifyWaxWalletRequest request, ServerCallContext context)
    {
        var profile = _mapper.Map<BusinessModels.Profile>(request.Profile);
        var res = await _waxWalletVerify.VerifyWaxWallet(profile.WaxWallet, context.CancellationToken);
        var response = new VerifyWaxWalletResponse();

        switch (res)
        {
            case BusinessModels.StatusType.Success:
                await _service.Update(profile, context.CancellationToken);
                response.Status = StatusType.Success;
                break;
            case BusinessModels.StatusType.Failed:
                response.Status = StatusType.Failed;
                break;
        }

        return response;
    }
}
