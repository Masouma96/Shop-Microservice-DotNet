using AutoMapper;
using IDP.Application.Commands.Auth;
using IDP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDP.Application.Helper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<AuthCommand, User>().ReverseMap();
        }
    }
}
