using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Resources.Authentication
{
    public class MinimumAgeRequirementsHandler : AuthorizationHandler<MinimumAgeRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirements requirement)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);
            if (dateOfBirth.AddYears(requirement.MinAge) > DateTime.Today)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
