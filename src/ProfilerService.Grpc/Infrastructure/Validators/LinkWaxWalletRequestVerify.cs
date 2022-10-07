using FluentValidation;
using Service.Grpc;

namespace ProfileService.Grpc.Infrastructure.Validators;

public class LinkWaxWalletRequestVerify : AbstractValidator<LinkWaxWalletRequest>
{
    public LinkWaxWalletRequestVerify()
    {
        RuleFor(x => x.WaxWallet)
            .NotNull()
            .NotEmpty()
            .MinimumLength(1);
    }
}
