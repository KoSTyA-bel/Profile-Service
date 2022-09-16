using FluentValidation;
using Service.Grpc;

namespace ProfilerService.Grpc.Infrastructure.Validators;

public class GetLeaderBoardByPointsAmountRequestValidator : AbstractValidator<GetLeaderBoardByPointsAmountRequest>
{
	public GetLeaderBoardByPointsAmountRequestValidator()
	{
		RuleFor(x => x.Count)
			.GreaterThanOrEqualTo(1);
	}
}
