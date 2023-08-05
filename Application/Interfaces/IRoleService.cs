using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        public Role GetRoleById(int id);
        public Role GetRoleByName(string name);
        public Role AddRole(Role role);
        public Role UpdateRole(Role role);
        public Role DeleteRole(int id);
        public IEnumerable<Role> GetAllRoles();
    }
}
