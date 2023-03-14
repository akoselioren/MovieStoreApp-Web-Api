using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public TimeSpan RunningTime { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
