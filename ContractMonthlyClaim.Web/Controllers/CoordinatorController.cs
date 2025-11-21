using System;
using System.Linq;
using ContractMonthlyClaim.Web.Data;
using ContractMonthlyClaim.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaim.Web.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly InMemoryStore _store;

        public CoordinatorController(InMemoryStore store)
        {
            _store = store;
        }

        // GET: Coordinator/Review
        [HttpGet]
        public IActionResult Review()
        {
            var pending = _store.Claims.Values
                .Where(c => c.Status == ClaimStatus.Pending || c.Status == ClaimStatus.UnderReview)
                .OrderByDescending(c => c.DateSubmitted);

            return View(pending);
        }

        // POST: Coordinator/Review
        [HttpPost]
        public IActionResult Review(Guid claimId)
        {
            if (_store.Claims.TryGetValue(claimId, out var claim))
            {
                // Coordinator only moves to UnderReview
                claim.Status = ClaimStatus.UnderReview;
            }

            return RedirectToAction("Review");
        }
    }
}