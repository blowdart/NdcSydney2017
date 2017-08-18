using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationPolicies
{
    public enum Location
    {
        Inside,
        Outside,
    }

    public class LocationRequirement : IAuthorizationRequirement
    {
        public Location Location { get; set; }
    }
}
