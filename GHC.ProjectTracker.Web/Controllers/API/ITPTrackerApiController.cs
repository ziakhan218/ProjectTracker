using GHC.ProjectTracker.IService;
using GHC.ProjectTracker.Web.Common;
using GHC.ProjectTracker.Web.Filters;
using GHC.ProjectTracker.Web.Filters.API;
using GHC.ProjectTracker.Web.Infrastructure.Mappers;
using GHC.ProjectTracker.Web.Models;
using GHC.ProjectTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GHC.ProjectTracker.Web.Controllers.API
{
    [RoutePrefix("api")]

    //[Authorize]
    public class ITPTrackerApiController : ApiController
    {
        private readonly IITPTrackerService iTProjectTrackerService;
        private IITPTrackerMapper mapper;

        public ITPTrackerApiController(IITPTrackerService iTProjectTrackerService, IITPTrackerMapper mapper)
        {
            this.iTProjectTrackerService = iTProjectTrackerService;
            this.mapper = mapper;
        }


        [HttpGet]
        [Route("allActiveUsers")]
        
        public HttpResponseMessage GetAllActiveUsers(bool isActive)
        {
            var users = iTProjectTrackerService.GetAllActiveUsers(isActive);
            var userModelList = mapper.Map<List<UserModel>>(users);

            var response = Request.CreateResponse<UserModel[]>(HttpStatusCode.OK, userModelList.ToArray());
            return response;
        }

        [HttpGet]
        [Route("userInfo")]

        public HttpResponseMessage GetUserInfo(int userId)
        {
            var user = iTProjectTrackerService.GetUser(userId);
            var userModel = mapper.Map<UserModel>(user);

            var response = Request.CreateResponse<UserModel>(HttpStatusCode.OK, userModel);
            return response;
        }

        [HttpGet]
        [Route("IsUserNameAlreadyTaken")]

        public HttpResponseMessage ValidateUserName(string userName)
        {
            var user = iTProjectTrackerService.GetUserByUserName(userName);
            bool isUserExist = true;
            if(user == null)
            {
                isUserExist = false;
            }

            var response = Request.CreateResponse<Boolean>(HttpStatusCode.OK, isUserExist);
            return response;
        }

        [HttpPost]
        [Route("saveUser")]

        public HttpResponseMessage SaveUser(UserModel userModel, bool isUpdate = false)
        {
            var user = mapper.Map<User>(userModel);
            var createdUser = iTProjectTrackerService.GetUpdateCreateUser(user, isUpdate);
            var response = Request.CreateResponse<UserModel>(HttpStatusCode.OK, userModel);
            return response;
        }

        [HttpPost]
        [Route("setUserState")]
        public HttpResponseMessage SetUserState(UserModel userModel)
        {
            var result = iTProjectTrackerService.SetUserState(userModel.UserId, userModel.IsActive);
            var response = Request.CreateResponse<Boolean>(HttpStatusCode.OK, result);
            return response;
        }

        [HttpGet]
        [Route("allCountries")]
        //[AuthorizeCustom(Roles.ITPTrackerAdmin, Roles.RetailUser, Roles.RetailAdmin)]
        public HttpResponseMessage GetAllCountries()
        {
            var countries = iTProjectTrackerService.GetCountries();
            var countryList = mapper.Map<List<CountryModel>>(countries);

            var response = Request.CreateResponse<CountryModel[]>(HttpStatusCode.OK, countryList.ToArray());
            return response;
        }
    }
}
