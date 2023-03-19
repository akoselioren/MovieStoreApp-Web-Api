using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Director
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Movie))]
        public int DirectedMovieId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Movie Movie { get; set; }
    }
}
