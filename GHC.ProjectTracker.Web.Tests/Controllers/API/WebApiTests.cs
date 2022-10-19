using GHC.ProjectTracker.IService;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Web.Common;
using GHC.ProjectTracker.Web.Controllers.API;
using GHC.ProjectTracker.Web.Infrastructure.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GHC.DataPortal.Model;
using GHC.ProjectTracker.Web.Models;

namespace GHC.ProjectTracker.Web.Tests.Controllers.API
{
    [TestClass]
    public class WebApiTests
    {
        private Mock<IProjectTrackerService> mockProjectTrackerService;
        private Mock<IITPTrackerService> mockITPTrackerService;
        private Mock<ISessionManager> mockSessionManager;
        private Mock<ITPTrackerMapper> mockTPTrackerMapper;
        private Mock<IITPTrackerMapper> mockITPTrackerMapper;
        private ITProjectTrackerApiController mockTProjectTrackerApiController;
        private ITPTrackerApiController mockITPTrackerApiController;

        [TestInitialize]
        public void Setup()
        {
            mockProjectTrackerService = new Mock<IProjectTrackerService>();
            mockITPTrackerService = new Mock<IITPTrackerService>();
            mockSessionManager = new Mock<ISessionManager>();
            mockTPTrackerMapper = new Mock<ITPTrackerMapper>();
            mockITPTrackerMapper = new Mock<IITPTrackerMapper>();

            ITPTrackerMapper.Initialise();

            mockTProjectTrackerApiController = new ITProjectTrackerApiController(mockProjectTrackerService.Object, new SessionManager());
            mockITPTrackerApiController = new ITPTrackerApiController(mockITPTrackerService.Object, new ITPTrackerMapper());
        }

        [TestMethod]
        public void CanGetAllActiveUsers()
        {
            List<User> users = new List<User> {
                new User { Id = 1, FirstName = "Zia", LastName = "khan", IsActive = true },
                new User { Id = 2, FirstName = "Stewart", LastName = "Cope", IsActive = true },
                new User { Id = 3, FirstName = "Abdullah", LastName = "kamran", IsActive = false },
                new User { Id = 4, FirstName = "Shravan", LastName = "Gone", IsActive = true },
            };

            mockITPTrackerService.Setup(u => u.GetAllActiveUsers(true)).Returns(users);

            mockITPTrackerApiController.Request = new HttpRequestMessage();
            mockITPTrackerApiController.Configuration = new System.Web.Http.HttpConfiguration();

            var response = mockITPTrackerApiController.GetAllActiveUsers(true);
            Assert.IsNotNull(response);

            UserModel[] activeUsers;

            Assert.IsTrue(response.TryGetContentValue<UserModel[]>(out activeUsers));
            Assert.IsTrue(activeUsers.Count() > 0);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

        }


    }
}
