using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper,IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public User RegisterUser(RegisterUserDto newuser)
        {
            if (newuser == null) throw new Exception("Brak danych usera");

            var user = _mapper.Map<User>(newuser);
            var hashedpassword = _passwordHasher.HashPassword(user, newuser.Password);
            user.PasswordHash = hashedpassword;
            _userRepository.add(user);
            return user;

        }
    }
}
