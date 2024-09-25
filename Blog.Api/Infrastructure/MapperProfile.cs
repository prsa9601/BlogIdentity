﻿using AutoMapper;
using Blog.Api.ViewModels.Users;
using Blog.Application.User.ChangePassword;

namespace Blog.Api.Infrastructure;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //CreateMap<AddUserAddressCommand, AddUserAddressViewModel>()
        //    .ForMember(f=>f.PhoneNumber,r=>r.MapFrom(w=>w.PhoneNumber)).ReverseMap();

        CreateMap<ChangePasswordViewModel, ChangeUserPasswordCommand>().ReverseMap();
    }
}