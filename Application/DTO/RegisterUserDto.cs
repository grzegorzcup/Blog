﻿using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class RegisterUserDto :IMap
    {
        [Required]
        public string Name { get; set;}
        [Required]
        public string Password { get; set;}
        [Required]
        public string Email { get; set;}
        public int RoleId { get; set;} = 1;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterUserDto, User>();
        }
    }
}
