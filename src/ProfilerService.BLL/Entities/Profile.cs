namespace ProfileService.BLL.Entities;

public class Profile
{
    public Guid Id { get; set; }

    public ulong DiscrodId { get; set; }

    public string WaxWallet { get; set; }

    public DateTime CreationDate { get; set; }

    public int PointsAmount { get; set; }

    public uint WinCount { get; set; }

    public uint LoseCount { get; set; }
}
