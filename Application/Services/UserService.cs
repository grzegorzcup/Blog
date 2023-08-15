﻿using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using Application.Resources;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper,IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _logger = logger;
        }

        public User RegisterUser(RegisterUserDto newuser)
        {
            _logger.LogError($"test");
            if (newuser == null) throw new Exception("Brak danych usera");

            var user = _mapper.Map<User>(newuser);
            var hashedpassword = _passwordHasher.HashPassword(user, newuser.Password);
            user.PasswordHash = hashedpassword;
            _userRepository.add(user);
            return user;

        }

        public string GenerateJWT(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.Name.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                //new Claim("CreatedDate", user.Created.ToString("yyyy-MM-dd"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.Secret));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(double.Parse(_authenticationSettings.JWTExpiredDays));

            var token = new JwtSecurityToken(_authenticationSettings.JWTIssuer,
                _authenticationSettings.JWTIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenhandler = new JwtSecurityTokenHandler();
            return tokenhandler.WriteToken(token);
        }
        public string Login(LoginDto login)
        {
            var user = _userRepository.GetByName(login.Name);
            if (user == null) throw new Exception("nieprawidłowa nazwa użytkownika");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new Exception("nieprawidłowe hasło!");
            }

            var token = GenerateJWT(user);

            return token;
        }
    }
}
