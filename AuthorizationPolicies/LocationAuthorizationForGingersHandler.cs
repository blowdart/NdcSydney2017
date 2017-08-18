using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationPolicies
{
    public class LocationAuthorizationForGingersHandler : AuthorizationHandler<LocationRequirement>
    {
        IWeatherProvider _weatherProvider;

        public LocationAuthorizationForGingersHandler(IWeatherProvider weatherProvider)
        {
            _weatherProvider = weatherProvider;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            LocationRequirement requirement)
        {
            var hairColour =
                context.User.Claims.Where(
                    c => c.Type == "biometrics:Hair" &&
                    c.Issuer == "urn:PassportControl").FirstOrDefault().Value;

            if (string.Compare(hairColour, "GINGER", StringComparison.OrdinalIgnoreCase) == 0  &&
                requirement.Location == Location.Outside)
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
