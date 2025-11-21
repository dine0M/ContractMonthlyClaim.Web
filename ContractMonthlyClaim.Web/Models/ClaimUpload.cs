using System;

namespace ContractMonthlyClaim.Web.Models
{
    public class ClaimUpload
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long Length { get; set; }
        public string StoredPath { get; set; } = string.Empty;
    }
}