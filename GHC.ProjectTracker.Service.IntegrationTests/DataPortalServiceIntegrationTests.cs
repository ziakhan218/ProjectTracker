using GHC.DataPortal.Service;
using GHC.DataPortal.IService;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GHC.DataPortal.Business;
using GHC.DataPortal.Data.Infrastructure;
using GHC.DataPortal.Data;
using GHC.DataPortal.Model;
using System.Collections.Generic;
using System.Linq;

namespace GHC.DataPortal.Service.IntegrationTests
{
    [TestClass]
    public class DataPortalServiceIntegrationTests
    {
        private UnityContainer unityContainer;
        private IDataPortalService service;

        [TestInitialize]
        public void Setup()
        {
            this.unityContainer = new UnityContainer();
            unityContainer.RegisterType<IDataPortalService, DataPortalService>()
                     .RegisterType<IDataPortalManager, DataPortalManager>()
                     .RegisterType<IDataPortalUnitOfWork, DataPortalUnitOfWork>()
                     .RegisterType<IDataPortalDbContext, DataPortalDbContext>()
                     .RegisterType<IDataPortalRepository, DataPortalRepository>();

            this.service = new DataPortalService(unityContainer.Resolve<IDataPortalManager>());
        }

        [TestMethod]
        public void CanGetAllUsers()
        {
            IEnumerable<User> users = service.GetAllActiveUsers();

            Assert.IsTrue(users.Count() > 0);
        }

        [TestMethod]
        public void CanGetUserByUserName()
        {
            User user = service.GetUserByUserName(@"INTERNATIONAL\TFN.TLN");

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void CanGetOrCreateUser()
        {
            User user = new User { UserName = @"INTERNATIONAL\TFN.TLN", FirstName = "TFN", LastName = "TLN", EmailAddress = "T.TLN@grosvenor.com" };
            
            user = service.GetOrCreateUser(user);

            Assert.IsTrue(user.Id > 0);
        }
        [TestMethod]
        public void CanGetCountries()
        {
            var countries = service.GetCountries();

            Assert.IsNotNull(countries);
            Assert.IsTrue(countries.Count() > 0);
        }

        [TestMethod]
        public void CanGetCountryById()
        {
            var france = service.GetCountryById(1);
            Assert.IsNotNull(france);
            Assert.IsTrue(france.Id == 1);
        }
    }
}
