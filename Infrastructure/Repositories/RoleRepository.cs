using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BlogContext _blogContext;

        public RoleRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public Role add(Role role)
        {
            _blogContext.Roles.Add(role);
            _blogContext.SaveChanges();
            return role;
        }

        public void delete(int id)
        {
            _blogContext.Roles.Remove(GetById(id));
            _blogContext.SaveChanges();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _blogContext.Roles;
        }

        public Role GetById(int id)
        {
            return _blogContext.Roles.SingleOrDefault(GetById(id));
        }

        public void update(Role role)
        {
            _blogContext.Roles.Update(role);
            _blogContext.SaveChanges();
        }
    }
}


