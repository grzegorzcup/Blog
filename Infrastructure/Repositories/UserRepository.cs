using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _blogContext;

        public UserRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public User add(User user)
        {
            _blogContext.Users.Add(user);
            _blogContext.SaveChanges();
            return user;
        }

        public void delete(int id)
        {
            var user = _blogContext.Users.SingleOrDefault(u => u.Id == id);
            _blogContext.Users.Remove(user);
            _blogContext.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _blogContext.Users
                        .Include(u=> u.Role);
        }

        public User GetByName(string name)
        {
            return _blogContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Name == name);
        }

        public User GetById(int id)
        {
            return _blogContext.Users
                .Include(u => u.Role)
                .SingleOrDefault(user => user.Id == id);
        }

        public void UpdateData(User user)
        {
            using(var context = _blogContext)
            {
                context.Users.Attach(user);
                context.Entry(user).Property(p => p.Name).IsModified = true;
                context.Entry(user).Property(p => p.Email).IsModified=true;
                context.Entry(user).Property(p => p.RoleId).IsModified = true;
                context.SaveChanges();
            }
        }

    }
}
