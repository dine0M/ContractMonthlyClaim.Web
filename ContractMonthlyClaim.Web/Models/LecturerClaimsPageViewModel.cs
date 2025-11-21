using System.Collections.Generic;

namespace ContractMonthlyClaim.Web.Models
{
    public class LecturerClaimsPageViewModel
    {
        public Claim NewClaim { get; set; } = new Claim();
        public List<Claim> Claims { get; set; } = new List<Claim>();
    }
}