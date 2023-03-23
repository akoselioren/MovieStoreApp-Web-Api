using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
        public int? DirectorId { get; set; }
        public Director Director { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; } = true;
        public List<Actor> MovieActors { get; set; }

    }
}
