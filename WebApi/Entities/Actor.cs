using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Actor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Movie))]
        public int CastMovieId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Movie Movie { get; set; }
    }
}
