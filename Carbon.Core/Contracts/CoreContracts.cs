using Microsoft.AspNetCore.Mvc;

namespace Carbon.Core.Contracts
{
    internal class CarbonProblemDetails : ProblemDetails
    {
        public Guid OperationId { get; set; }
    }
        
    internal enum RateLimiterPolicies
    {
        SessionPolicy
    }
}