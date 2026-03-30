using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class ContactInfo
    {
        [Required]
        public int ContactId {  get; set; }
        [Required(ErrorMessage ="First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")] 
        public string LastName { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required(ErrorMessage ="Email Id is required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public  string EmailId { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [Range(1000000000, 9999999999, ErrorMessage = "Enter valid 10-digit number")]
        public long MobileNo { get; set; }

        public string Designation { get; set; }




    }
}
