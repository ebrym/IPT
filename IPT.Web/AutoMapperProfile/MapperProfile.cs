using AutoMapper;
using IPT.Data.Entity;
using IPT.Web.Models.AccountModel;
using System.Collections.Generic;

namespace IPT.Web.AutoMapperProfile
{
    public class MapperProfile:Profile
    {

        public MapperProfile()
        {
           

            CreateMap<UserViewModel, User>().ReverseMap();

            CreateMap<RegisterViewModel, User>().ReverseMap();

            CreateMap<RoleViewModel, Role>().ReverseMap();

           // CreateMap<PermissionViewModel, Permission>().ReverseMap();

          


        }
    }
}
