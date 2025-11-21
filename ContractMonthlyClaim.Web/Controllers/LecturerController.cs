using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ContractMonthlyClaim.Web.Data;
using ContractMonthlyClaim.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaim.Web.Controllers
{
    public class LecturerController : Controller
    {
        private readonly InMemoryStore _store;
        private readonly IWebHostEnvironment _env;

        public LecturerController(InMemoryStore store, IWebHostEnvironment env)
        {
            _store = store;
            _env = env;
        }

        // GET: Lecturer/SubmitClaim
        [HttpGet]
        public IActionResult SubmitClaim()
        {
            var vm = new LecturerClaimViewModel
            {
                PreviousClaims = _store.Claims.Values
                    .OrderByDescending(c => c.DateSubmitted)
                    .ToList()
            };

            return View(vm);
        }

        // POST: Lecturer/SubmitClaim
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitClaim(LecturerClaimViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.PreviousClaims = _store.Claims.Values
                    .OrderByDescending(c => c.DateSubmitted)
                    .ToList();
                return View(model);
            }

            // Try to link to an existing lecturer (if ID matches), otherwise just store a new Guid
            Guid lecturerIdGuid;
            if (!Guid.TryParse(model.LecturerId, out lecturerIdGuid))
            {
                lecturerIdGuid = Guid.NewGuid();
            }

            var claim = new Claim
            {
                LecturerId = lecturerIdGuid,
                HoursWorked = model.HoursWorked,
                HourlyRate = model.HourlyRate,
                Notes = model.Notes,
                DateSubmitted = DateTime.UtcNow,
                Status = ClaimStatus.Pending
            };

            // Handle file upload (optional)
            if (model.UploadFile != null && model.UploadFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "Uploads");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.UploadFile.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.UploadFile.CopyToAsync(stream);
                }

                claim.UploadPath = $"/Uploads/{uniqueFileName}";
                claim.Uploads.Add(new ClaimUpload
                {
                    FileName = model.UploadFile.FileName,
                    ContentType = model.UploadFile.ContentType,
                    Length = model.UploadFile.Length,
                    StoredPath = claim.UploadPath
                });
            }

            _store.Claims[claim.Id] = claim;

            TempData["Message"] = "Claim submitted successfully.";

            // Redirect to avoid resubmitting on refresh
            return RedirectToAction(nameof(SubmitClaim));
        }
    }
}