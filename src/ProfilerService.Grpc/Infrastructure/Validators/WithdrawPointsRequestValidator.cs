using FluentValidation;
using Service.Grpc;

namespace ProfilerService.Grpc.Infrastructure.Validators;

public class WithdrawPointsRequestValidator : AbstractValidator<WithdrawPointsRequest>
{
	public WithdrawPointsRequestValidator()
	{
		RuleFor(x => x.PointsAmount)
			.GreaterThanOrEqualTo(1);
	}
}
