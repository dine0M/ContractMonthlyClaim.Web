using System;
using System.Collections.Generic;

namespace ContractMonthlyClaim.Web.Models
{
    public class Claim
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid LecturerId { get; set; }

        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount => HoursWorked * HourlyRate;

        public string? Notes { get; set; }

        public List<ClaimUpload> Uploads { get; set; } = new();

        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        // relative path for the file in wwwroot (if any)
        public string? UploadPath { get; set; }
    }
}