using FluentValidation;
using Service.Grpc;

namespace ProfilerService.Grpc.Infrastructure.Validators;

public class GetRangeOfProfilesRequestValidator : AbstractValidator<GetRangeOfProfilesRequest>
{
	public GetRangeOfProfilesRequestValidator()
	{
		RuleFor(x => x.Page)
			.GreaterThanOrEqualTo(1);

		RuleFor(x => x.PageSize)
			.GreaterThanOrEqualTo(1);
	}
}
