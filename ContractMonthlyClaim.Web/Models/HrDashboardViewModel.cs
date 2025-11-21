using System.Collections.Generic;

namespace ContractMonthlyClaim.Web.Models
{
    public class HrDashboardViewModel
    {
        public Lecturer NewLecturer { get; set; } = new Lecturer();

        public List<Lecturer> Lecturers { get; set; } = new();

        // Claim statistics
        public int TotalClaims { get; set; }
        public int PendingCount { get; set; }
        public int UnderReviewCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }

        // Used for the “approved claims” report shortcut on the HR page
        public List<Claim> ApprovedClaims { get; set; } = new();
    }
}
