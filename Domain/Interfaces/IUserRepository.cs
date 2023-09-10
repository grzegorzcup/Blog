using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetById(int id);
        User add(User user);
        void UpdateData(User user);
        void delete(int id);
        User GetByName(string name);
    }
}
