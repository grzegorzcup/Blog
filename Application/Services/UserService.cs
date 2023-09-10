using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using Application.Resources.Authentication;

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
            if (newuser == null) throw new Exception("Brak danych usera");

            if (newuser.RoleId == 0)
                throw new Exception("brak ID roli uzytkownika");

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

            /*if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(
                        new Claim("Email" , user.Email)
                    );
            }*/

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

        public bool Remove(int id)
        {
            if (id == 0 || id == null)
                return false;
            _userRepository.delete(id);
            return true;

        }

        public User Update(int id, UpdateUserDto user)
        {
            var toUpdate = _mapper.Map<User>(user);
            toUpdate.Id = id;
            if (toUpdate == null)
                throw new Exception("nie ma takiego Użutkownika");
            if (id == 0 || id == null)
                throw new Exception("nie ma użytkownika z takim id");
            _userRepository.update(toUpdate);
            return toUpdate;
        }

        public IEnumerable<User> GetUsers()
        {
            var list = _userRepository.GetAllUsers();
            return list;
        }

        public User GetById(int id)
        {
            var user = _userRepository.GetById(id);
            return user;
        }
    }
}
