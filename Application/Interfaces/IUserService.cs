using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public User RegisterUser(RegisterUserDto newuser);

        public string GenerateJWT(User user);

        public string Login(LoginDto login);
    }
}
