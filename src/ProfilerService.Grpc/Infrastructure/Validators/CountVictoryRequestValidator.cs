using FluentValidation;
using Service.Grpc;

namespace ProfileService.Grpc.Infrastructure.Validators;

public class CountVictoryRequestValidator : AbstractValidator<CountVictoryRequest>
{
    public CountVictoryRequestValidator()
    {
        RuleFor(x => x.PointsAmount)
            .GreaterThanOrEqualTo(0);
    }
}
