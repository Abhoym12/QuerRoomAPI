using AutoMapper;
using QueryRoom.DTOs;
using QueryRoomAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryRoom.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<TBL_QUESTIONS, Questions>();
            Mapper.CreateMap<Questions, TBL_QUESTIONS>();
            Mapper.CreateMap<TBL_ANSWERS, Answers>();
            Mapper.CreateMap<Answers, TBL_ANSWERS>();
            Mapper.CreateMap<TBL_USERROLE,UserRoleDTO>();
            Mapper.CreateMap<UserRoleDTO , TBL_USERROLE>();
            Mapper.CreateMap<TBL_SIGNUP,UserDetails>();
            Mapper.CreateMap<UserDetails,TBL_SIGNUP>();
        }
    }
}