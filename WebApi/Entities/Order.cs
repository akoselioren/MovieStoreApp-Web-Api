using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public int MovieId { get; set;}
        public int Price { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsActive { get; set; } = true;
        public Movie Movie { get; set; }
        public Customer Customer { get; set; }

    }
}
