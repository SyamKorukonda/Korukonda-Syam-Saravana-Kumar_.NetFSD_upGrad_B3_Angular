using System.ComponentModel.DataAnnotations;

namespace ContactService.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        
        public int CategoryId { get; set; }
    }
}
