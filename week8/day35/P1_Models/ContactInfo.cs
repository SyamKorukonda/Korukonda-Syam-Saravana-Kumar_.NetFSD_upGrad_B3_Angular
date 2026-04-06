using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication10.Models
{
    public class ContactInfo
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public long MobileNo { get; set; }
        public string Designation { get; set; }
        public int CompanyId { get; set; } //foreign key
        public int DepartmentId { get; set; } //foreign key

        // For JOIN display
        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
        public string DepartmentName { get; set; }
    }
}