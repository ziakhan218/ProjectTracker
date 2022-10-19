using AutoMapper;
using GHC.ProjectTracker.Common;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Web.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace GHC.ProjectTracker.Web.Infrastructure.Mappers
{
    [ExcludeFromCodeCoverageAttribute]
    public class ITPTrackerMapper : IITPTrackerMapper
    {
        public static void Initialise()
        {
            Mapper.CreateMap<User, UserModel>()
                   .ForMember(dest => dest.UserId, expr => expr.MapFrom(item => item.Id))
                   .ForMember(dest => dest.FirstNameLastName, expr => expr.MapFrom(item => string.Format("{0} {1}", item.FirstName, item.LastName)))
                   .ForMember(dest => dest.DisplayName, expr => expr.MapFrom(item => string.Format("{0} {1} ({2})", item.FirstName, item.LastName, item.UserName)));

            Mapper.CreateMap<UserModel, User>()
                   .ForMember(dest => dest.Id, expr => expr.MapFrom(item => item.UserId))
                   .ForMember(dest => dest.FirstName,expr => expr.MapFrom(item => item.FirstName))
                   .ForMember(dest => dest.LastName, expr => expr.MapFrom(item => item.LastName))
                   .ForMember(dest => dest.EmailAddress, expr => expr.MapFrom(item => item.EmailAddress))
                   .ForMember(dest => dest.UserName, expr => expr.MapFrom(item => item.UserName));

            Mapper.CreateMap<Country, CountryModel>();
        }

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}