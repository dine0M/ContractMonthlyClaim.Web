using System;
using System.Collections.Generic;
using ContractMonthlyClaim.Web.Models;

namespace ContractMonthlyClaim.Web.Data
{
    public interface IClaimStore
    {
        IReadOnlyCollection<Claim> GetAll();
        void Add(Claim claim);
        void UpdateStatus(Guid claimId, string status);
        Claim Get(Guid claimId);
    }
}