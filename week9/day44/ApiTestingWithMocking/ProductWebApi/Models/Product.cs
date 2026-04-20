using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication19.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public double Price { get; set; }
        public string Category { get; set; }
    }
}
