using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationPolicies
{
    public class LocationAuthorizationHandler : AuthorizationHandler<LocationRequirement>
    {
        IWeatherProvider _weatherProvider;

        public LocationAuthorizationHandler(IWeatherProvider weatherProvider)
        {
            _weatherProvider = weatherProvider;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            LocationRequirement requirement)
        {
            var country =
                context.User.FindFirst(
                    c => c.Type == ClaimTypes.Country &&
                    c.Issuer == "urn:PassportControl").Value.ToUpperInvariant();

            switch (requirement.Location)
            {
                case Location.Outside:

                    if ((_weatherProvider.GetSeason() == Season.Summer &&
                        country == "GBR") ||
                        (_weatherProvider.GetSeason() == Season.Winter &&
                        country == "AUS"))
                    {
                        // Do nothing, this is not when these people should be outside.
                        ;
                    }
                    else
                    {
                        context.Succeed(requirement);
                    }

                    break;

                case Location.Inside:
                    context.Succeed(requirement);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
