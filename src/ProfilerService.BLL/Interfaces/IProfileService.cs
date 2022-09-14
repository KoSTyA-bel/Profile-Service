﻿using ProfilerService.BLL.Entities;

namespace ProfilerService.BLL.Interfaces;

public interface IProfileService
{
    Task<Profile> GetByDiscordId(ulong discordId, CancellationToken token);

    Task<Profile> Create(Profile profile, CancellationToken token);

    Task<Profile> Update(Profile profile, CancellationToken token);

    Task<IEnumerable<Profile>> GetProfiles(int startPosition, int count, CancellationToken token);

    Task<bool> Delete(ulong discordId, CancellationToken token);

    // todo return type
    public Task<StatusType> DepositPoints(ulong discordId, int pointsAmount, CancellationToken token);

    // todo return type
    public Task<StatusType> WithdrawPoints(ulong discordId, int pointsAmount, CancellationToken token);
}
