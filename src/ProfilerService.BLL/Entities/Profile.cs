namespace ProfilerService.BLL.Entities;

public class Profile
{
    public Guid Id { get; set; }

    public ulong DiscrodId { get; set; }

    public string WaxWallet { get; set; }

    public double Points { get; set; }
}
