
using System;
using System.Linq;
using ContractMonthlyClaim.Web.Data;
using ContractMonthlyClaim.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaim.Web.Controllers
{
    public class HRController : Controller
    {
        private readonly InMemoryStore _store;

        public HRController(InMemoryStore store)
        {
            _store = store;
        }

        // GET: HR/ManageLecturers
        [HttpGet]
        public IActionResult ManageLecturers()
        {
            var vm = BuildDashboardViewModel();
            return View(vm);
        }

        // POST: HR/AddLecturer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLecturer(Lecturer lecturer)
        {
            if (string.IsNullOrWhiteSpace(lecturer.Name))
            {
                ModelState.AddModelError("NewLecturer.Name", "Name is required.");
            }

            if (!ModelState.IsValid)
            {
                var vm = BuildDashboardViewModel();
                vm.NewLecturer = lecturer;
                return View("ManageLecturers", vm);
            }

            lecturer.Id = Guid.NewGuid();
            _store.Lecturers[lecturer.Id] = lecturer;

            return RedirectToAction(nameof(ManageLecturers));
        }

        // POST: HR/UpdateLecturer  (inline edit of lecturer details)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateLecturer(Guid id, string name, string email, string department, string? phoneNumber)
        {
            if (_store.Lecturers.TryGetValue(id, out var lecturer))
            {
                lecturer.Name = name;
                lecturer.Email = email;
                lecturer.Department = department;
                lecturer.PhoneNumber = phoneNumber;
            }

            return RedirectToAction(nameof(ManageLecturers));
        }

        // GET: HR/InvoiceReport  – simple “automation” report for approved claims
        [HttpGet]
        public IActionResult InvoiceReport()
        {
            var approvedClaims = _store.Claims.Values
                .Where(c => c.Status == ClaimStatus.Approved)
                .OrderBy(c => c.DateSubmitted)
                .ToList();

            return View(approvedClaims);
        }

        // Helper to build dashboard data
        private HrDashboardViewModel BuildDashboardViewModel()
        {
            var claims = _store.Claims.Values;

            return new HrDashboardViewModel
            {
                Lecturers = _store.Lecturers.Values.ToList(),
                TotalClaims = claims.Count(),
                PendingCount = claims.Count(c => c.Status == ClaimStatus.Pending),
                UnderReviewCount = claims.Count(c => c.Status == ClaimStatus.UnderReview),
                ApprovedCount = claims.Count(c => c.Status == ClaimStatus.Approved),
                RejectedCount = claims.Count(c => c.Status == ClaimStatus.Rejected),
                ApprovedClaims = claims.Where(c => c.Status == ClaimStatus.Approved).ToList()
            };
        }
    }
}