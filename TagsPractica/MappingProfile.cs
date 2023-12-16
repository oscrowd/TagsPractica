using AutoMapper;
using TagsPractica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagsPractica.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TagsPractica
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //CreateMap<RegisterViewModel, User>()
            //    .ForMember(x => x.BirthDate, opt => opt.MapFrom(c => new DateTime((int)c.Year, (int)c.Month, (int)c.Date)))
            //    .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
            //    .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
            CreateMap<RegisterViewModel, User>();
            CreateMap<User, RegisterViewModel > ();
            CreateMap<AuthViewModel, User>();
            CreateMap<User, AuthViewModel>();
            //CreateMap<User, Model>();
        }
    }
}
