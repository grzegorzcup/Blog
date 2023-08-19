using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Resources.Authentication
{
    public class MinimumAgeRequirements : IAuthorizationRequirement
    {
        public int MinAge { get; }

        public MinimumAgeRequirements(int minimumAge)
        {
            MinAge = minimumAge;
        }
    }
}
