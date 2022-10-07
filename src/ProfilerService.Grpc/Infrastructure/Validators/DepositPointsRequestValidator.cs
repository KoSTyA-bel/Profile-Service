using FluentValidation;
using Service.Grpc;

namespace ProfileService.Grpc.Infrastructure.Validators;

public class DepositPointsRequestValidator : AbstractValidator<DepositPointsRequest>
{
    public DepositPointsRequestValidator()
    {
        RuleFor(x => x.PointsAmount)
            .GreaterThanOrEqualTo(1);
    }
}
