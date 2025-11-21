using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaim.Web.Models
{
    public class LecturerClaimViewModel
    {
        [Required(ErrorMessage = "Lecturer ID is required.")]
        [Display(Name = "Lecturer ID")]
        public string? LecturerId { get; set; }

        [Required(ErrorMessage = "Hours Worked is required.")]
        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }

        [Required(ErrorMessage = "Hourly Rate is required.")]
        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }

        [Display(Name = "Additional Notes")]
        public string? Notes { get; set; }

        [Display(Name = "Upload Document")]
        public IFormFile? UploadFile { get; set; }   // nullable to remove compiler warning

        public string? FileName { get; set; }

        public decimal FinalPayment => HoursWorked * HourlyRate;

        public string Status { get; set; } = "Pending";

        // List of previously submitted claims (the “Your Claims” table)
        public List<Claim> PreviousClaims { get; set; } = new();
    }
}