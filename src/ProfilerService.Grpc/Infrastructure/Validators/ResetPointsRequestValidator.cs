using FluentValidation;
using Service.Grpc;

namespace ProfilerService.Grpc.Infrastructure.Validators;

public class ResetPointsRequestValidator : AbstractValidator<ResetPointsRequest>
{
	public ResetPointsRequestValidator()
	{
		RuleFor(x => x.PointsAmount)
			.GreaterThanOrEqualTo(1);
	}
}
