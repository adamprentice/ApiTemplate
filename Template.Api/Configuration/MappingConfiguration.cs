using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Api.Models.RequestModel;
using Template.Api.Models.ResponseModel;
using Template.Domain.Entities;

namespace Template.Api.Configuration
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<UserRequestModel, User>()
                .ForMember(x => x.PasswordHash, opt => opt.Ignore())
                .ForMember(x => x.PasswordSalt, opt => opt.Ignore());
            CreateMap<User, UserResponseModel>()
                .ForMember(x => x.Token, opt => opt.Ignore());
        }
    }
}
