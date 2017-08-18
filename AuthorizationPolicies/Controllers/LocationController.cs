using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationPolicies.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        [Authorize(Policy = AuthorizationPolicies.Inside)]
        public IActionResult Inside()
        {
            return View();
        }

        [Authorize(Policy = AuthorizationPolicies.Outside)]
        public IActionResult Outside()
        {
            return View();
        }
    }
}