using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class RegisterUserDto :IMap
    {
        public string Name { get; set;}
        public string Password { get; set;}
        public string Email { get; set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterUserDto, User>();
        }
    }
}
