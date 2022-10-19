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
using System;
using GHC.DataPortal.Model.Retail;

namespace GHC.DataPortal.Service.IntegrationTests
{
    [TestClass]
    public class RetailServiceIntegrationTests
    {
        private UnityContainer unityContainer;
        private IRetailService service;

        [TestInitialize]
        public void Setup()
        {
            this.unityContainer = new UnityContainer();
            unityContainer.RegisterType<IRetailService, RetailService>()
                     .RegisterType<IRetailManager, RetailManager>()
                     .RegisterType<IDataPortalUnitOfWork, DataPortalUnitOfWork>()
                     .RegisterType<IDataPortalDbContext, DataPortalDbContext>()
                     .RegisterType<IRetailRepository, RetailRepository>();

            this.service = new RetailService(unityContainer.Resolve<IRetailManager>());
        }

        [TestMethod]
        public void CanSaveUpdateDeleteInputs()
        {
            var date = DateTime.Parse("2011-11-11");
            var inputs = new List<InputItem>
            {
                new InputItem { Id = 1,
                             FilePeriod = date,
                             GroupId = 1,
                             DataYear = 2015,
                             City = "Peris",
                             Area = 100m,
                             WeightedArea = 100m,
                             CreatedById = 1,
                             CreatedDate = date,
                             UpdatedById = 1,
                             UpdatedDate = date
                },
                new InputItem { Id = 2,
                             FilePeriod = date,
                             GroupId = 1,
                             DataYear = 2015,
                             City = "Peris",
                             Area = 200m,
                             WeightedArea = 200m,
                             CreatedById = 1,
                             CreatedDate = date,
                             UpdatedById = 1,
                             UpdatedDate = date
                },
            };
            //test insert
            service.SaveInputs(inputs);

            //test update
            inputs[0].Area = 400m;

            service.UpdateInput(inputs[0]);

            //clean up
            service.DeleteInputs(new int[] { 1, 2 });
        }

        [TestMethod]
        public void CanDeleteInputsByIds()
        {
            var date = DateTime.Parse("2009-09-09");
            var inputs = new List<InputItem>
            {
                new InputItem { Id = 1,
                             FilePeriod = date,
                             GroupId = 1,
                             DataYear = 2015,
                             Country = "Peris",
                             Area = 100m,
                             WeightedArea = 100m,
                             CreatedById = 1,
                             CreatedDate = date,
                             UpdatedById = 1,
                             UpdatedDate = date
                },
                new InputItem { Id = 2,
                             FilePeriod = date,
                             GroupId = 1,
                             DataYear = 2015,
                             Country = "Peris",
                             Area = 200m,
                             WeightedArea = 200m,
                             CreatedById = 1,
                             CreatedDate = date,
                             UpdatedById = 1,
                             UpdatedDate = date
                },
            };
            //insert temp data
            service.SaveInputs(inputs);
            service.DeleteInputs(new int[] { 1, 2 });
        }
    }
}
