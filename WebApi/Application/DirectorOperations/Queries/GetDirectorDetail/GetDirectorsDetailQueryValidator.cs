using FluentValidation;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorsDetailQueryValidator : AbstractValidator<GetDirectorsDetailQuery>
    {
        public GetDirectorsDetailQueryValidator()
        {
            RuleFor(query => query.DirectorId).GreaterThan(0);
        }
    }
}
