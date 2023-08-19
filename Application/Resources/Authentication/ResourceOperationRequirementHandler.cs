using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Resources.Authentication
{
    //dodać zasób potem
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement)
        {
            if (requirement.Operation == ResourceOperation.Read ||
                requirement.Operation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;


            return Task.CompletedTask;

        }
    }
}
