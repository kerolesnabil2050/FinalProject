using AutoMapper;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceViewModel.Midetor
{
    public class MeditorDtoProfile : Profile
    {
        public MeditorDtoProfile()
        {
            CreateMap<Mediator, MeditorDto>();
            CreateMap<MeditorDto, Applicationuser>();
                //.ForMember(dst => dst.password
                //, opt => opt.MapFrom(src => src.Applicationuser.PasswordHash))
                //.ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Applicationuser.Email))
                //.ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.Applicationuser.UserName));


                


        }
    }
}
