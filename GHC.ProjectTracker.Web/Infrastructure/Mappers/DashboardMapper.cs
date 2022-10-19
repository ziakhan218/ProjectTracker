using AutoMapper;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace GHC.DataPortal.Web.Infrastructure.Mappers
{
    [ExcludeFromCodeCoverageAttribute]
    public class DashboardMapper : IDashboardMapper
    {
        public static void Initialise()
        {
            Mapper.CreateMap<User, UserModel>()
                   .ForMember(dest => dest.UserId, expr => expr.MapFrom(item => item.Id))
                   .ForMember(dest => dest.FirstNameLastName, expr => expr.MapFrom(item => string.Format("{0} {1}", item.FirstName, item.LastName)))
                   .ForMember(dest => dest.DisplayName, expr => expr.MapFrom(item => string.Format("{0} {1} ({2})", item.FirstName, item.LastName, item.UserName)));
        }

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}