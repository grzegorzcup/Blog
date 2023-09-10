using Application.Mappings;
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
    public class UpdateUserDto :IMap
    {
        public string Name { get; set; }
        
        public string Email { get; set; }

        public int RoleId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, User>();
        }
    }
}
