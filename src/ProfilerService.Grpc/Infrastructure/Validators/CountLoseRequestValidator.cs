using FluentValidation;
using Service.Grpc;

namespace ProfileService.Grpc.Infrastructure.Validators;

public class CountLoseRequestValidator : AbstractValidator<CountLoseRequest>
{
    public CountLoseRequestValidator()
    {
        RuleFor(x => x.PointsAmount)
            .GreaterThanOrEqualTo(0);
    }
}
