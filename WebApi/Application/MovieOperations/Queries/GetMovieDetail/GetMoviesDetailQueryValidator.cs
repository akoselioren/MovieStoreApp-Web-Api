using FluentValidation;

namespace WebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMoviesDetailQueryValidator : AbstractValidator<GetMoviesDetailQuery>
    {
        public GetMoviesDetailQueryValidator()
        {
            RuleFor(query => query.MovieId).GreaterThan(0);
        }
    }
}
