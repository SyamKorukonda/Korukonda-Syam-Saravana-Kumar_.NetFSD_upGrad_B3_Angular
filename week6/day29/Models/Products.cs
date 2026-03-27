using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Products
    {
        [Required]
        public int Pid { get; set; }
        [Required]
        [StringLength(15,MinimumLength =5)]
        public string Pname { get; set; }
        [Required]
        public decimal Pprice { get; set; }
        [StringLength(15,MinimumLength =5)]
        public string Pcategory { get; set; }

    }
}
