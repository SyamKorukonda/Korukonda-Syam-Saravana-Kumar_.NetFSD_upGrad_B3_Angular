using System.ComponentModel.DataAnnotations; //This is the missing namespace!
using System.Text.Json.Serialization;

namespace ContactManagement.Models
{
    public class ContactInfo
    {
        [Key] //
        public int ContactId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public long MobileNo { get; set; }
        public string Designation { get; set; }

        public int CompanyId { get; set; }
        [JsonIgnore]
        public Company ?Company { get; set; }

        [JsonIgnore]

        public int DepartmentId { get; set; }
        public Department ?Department { get; set; }
    }
}