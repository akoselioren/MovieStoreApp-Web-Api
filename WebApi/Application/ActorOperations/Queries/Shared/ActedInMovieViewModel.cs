using System;

namespace WebApi.Application.ActorOperations.Queries.Shared
{
    public class ActedInMovieViewModel
    {
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
    }
}
