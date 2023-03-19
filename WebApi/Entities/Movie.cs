using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public string RunningTime { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; } = true;
        public Genre Genre { get; set; }
        public Director Director { get; set; }
    }
}
