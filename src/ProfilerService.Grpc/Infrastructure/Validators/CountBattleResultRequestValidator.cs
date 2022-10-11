using FluentValidation;
using Service.Grpc;

namespace ProfileService.Grpc.Infrastructure.Validators
{
    public class CountBattleResultRequestValidator : AbstractValidator<CountBattleResultRequest>
    {
        public CountBattleResultRequestValidator()
        {
            RuleFor(x => x.BattleExodus).NotEqual(BattleExodus.Undefiend);
        }
    }
}
