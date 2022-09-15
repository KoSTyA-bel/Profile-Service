﻿using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface IProfileService
{
    Task<Profile> GetByDiscordId(ulong discordId, CancellationToken token);

    Task<StatusType> Create(Profile profile, CancellationToken token);

    Task<StatusType> Update(Profile profile, CancellationToken token);

    Task<IEnumerable<Profile>> GetProfiles(int startPosition, int count, CancellationToken token);

    Task<IEnumerable<Profile>> GetLeaderBoard(int count, CancellationToken token);

    Task<StatusType> Delete(ulong discordId, CancellationToken token);

    Task<StatusType> DepositPoints(ulong discordId, int pointsAmount, CancellationToken token);

    Task<StatusType> WithdrawPoints(ulong discordId, int pointsAmount, CancellationToken token);

    Task<StatusType> ResetPoints(int pointsAmount, CancellationToken token);
}
