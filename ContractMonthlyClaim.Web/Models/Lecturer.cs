using System;

namespace ContractMonthlyClaim.Web.Models
{
    public class Lecturer
    {
        public Guid Id { get; set; } = Guid.NewGuid();   // primary key

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }

        // Optional extra contact fields if you want them later
        public string? PhoneNumber { get; set; }
    }
}