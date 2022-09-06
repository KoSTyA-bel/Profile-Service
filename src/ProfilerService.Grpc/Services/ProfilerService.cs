using Grpc.Core;
using Service.Grpc;

namespace ProfilerService.Grpc.Services;

public class ProfilerService : Service.Grpc.ProfilerService.ProfilerServiceBase
{
    public override Task<CreateProfileResponse> CreateProfile(CreateProfileRequest request, ServerCallContext context)
    {
        return base.CreateProfile(request, context);
    }

    public override Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        return base.DeleteUser(request, context);
    }

    public override Task<DepositPointsResponse> DepositPoints(DepositPointsRequest request, ServerCallContext context)
    {
        return base.DepositPoints(request, context);
    }

    public override Task<GetByDiscordIdResponse> GetByDiscordId(GetByDiscordIdRequest request, ServerCallContext context)
    {
        return base.GetByDiscordId(request, context);
    }

    public override Task<GetRangeOfProfilesResponse> GetRangeOfProfiles(GetRangeOfProfilesRequest request, ServerCallContext context)
    {
        return base.GetRangeOfProfiles(request, context);
    }

    public override Task<WithdrawPointsResponse> WithdrawPoints(WithdrawPointsRequest request, ServerCallContext context)
    {
        return base.WithdrawPoints(request, context);
    }
}
