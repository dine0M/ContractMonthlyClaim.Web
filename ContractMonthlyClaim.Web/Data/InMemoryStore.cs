using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ContractMonthlyClaim.Web.Models;

namespace ContractMonthlyClaim.Web.Data
{
    public class InMemoryStore
    {
        public ConcurrentDictionary<Guid, Lecturer> Lecturers { get; } = new();
        public ConcurrentDictionary<Guid, Claim> Claims { get; } = new();
        public ConcurrentDictionary<Guid, List<ClaimUpload>> Uploads { get; } = new();

        public InMemoryStore()
        {
            // no seed data
        }
    }
}