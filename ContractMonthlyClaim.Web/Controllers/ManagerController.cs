using System;
using System.Linq;
using ContractMonthlyClaim.Web.Data;
using ContractMonthlyClaim.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaim.Web.Controllers
{
    public class ManagerController : Controller
    {
        private readonly InMemoryStore _store;

        public ManagerController(InMemoryStore store)
        {
            _store = store;
        }

        // GET: Manager/Review
        [HttpGet]
        public IActionResult Review()
        {
            var toProcess = _store.Claims.Values
                .Where(c => c.Status == ClaimStatus.UnderReview ||
                            c.Status == ClaimStatus.Approved ||
                            c.Status == ClaimStatus.Rejected)
                .OrderByDescending(c => c.DateSubmitted);

            return View(toProcess);
        }

        // POST: Manager/Review
        [HttpPost]
        public IActionResult Review(Guid claimId, ClaimStatus status)
        {
            if (_store.Claims.TryGetValue(claimId, out var claim))
            {
                if (status == ClaimStatus.Approved || status == ClaimStatus.Rejected)
                {
                    claim.Status = status;
                }
            }

            return RedirectToAction("Review");
        }
    }
}