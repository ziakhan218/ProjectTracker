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
using GHC.DataPortal.Model.OECD;

namespace GHC.DataPortal.Service.IntegrationTests
{
    [TestClass]
    public class OECDServiceIntegrationTests
    {
        private UnityContainer unityContainer;
        private IOECDService service;
        private IOECDRepository repository;

        [TestInitialize]
        public void Setup()
        {
            this.unityContainer = new UnityContainer();
            unityContainer.RegisterType<IOECDService, OECDService>()
                     .RegisterType<IOECDManager, OECDManager>()
                     .RegisterType<IDataPortalUnitOfWork, DataPortalUnitOfWork>()
                     .RegisterType<IDataPortalDbContext, DataPortalDbContext>()
                     .RegisterType<IOECDRepository, OECDRepository>();

            this.service = new OECDService(unityContainer.Resolve<IOECDManager>());
            this.repository = new OECDRepository(unityContainer.Resolve<IDataPortalUnitOfWork>());
        }

        [TestMethod]
        public void CanGetAllOECDMapping()
        {
            var OECDMappingList = service.GetAllOECDMapping();
            Assert.IsNotNull(OECDMappingList);
            Assert.IsTrue(OECDMappingList.Count() > 0);
        }

        [TestMethod]
        public void CanGetUnmappedBlueprintCompanies()
        {
            var bluePrintCompanies = service.GetUnmappedBlueprintCompanies();
            Assert.IsNotNull(bluePrintCompanies);
            Assert.IsTrue(bluePrintCompanies.Count() > 0);
        }

        [TestMethod]
        public void CanGetOECDMappingByCognosId()
        {
            var date = DateTime.Parse("2015-12-18");
            var OECDMappingItem = new OECDMapping
            {
                CognosReferenceId = "3R1075",
                BlueprintReferenceId = "1300LP",
                CreatedById = 1,
                CreatedDate = date,
                UpdatedById = 1,
                UpdatedDate = date
            };
            service.SaveOECDMapping(OECDMappingItem);

            var OECDMapItem = service.GetOECDMappingByCognosId("3R1075");
            Assert.IsNotNull(OECDMapItem);
        }

        [TestMethod]
        public void SaveOECDMapping()
        {
            var date = DateTime.Parse("2015-12-18");
            var OECDMappingItem = new OECDMapping
            {
                CognosReferenceId = "3R1075",
                BlueprintReferenceId = "1300LP",
                CreatedById = 1,
                CreatedDate = date,
                UpdatedById = 1,
                UpdatedDate = date
            };

            service.SaveOECDMapping(OECDMappingItem);
            var oecdItem = service.GetOECDMappingByCognosId("3R1075");
            Assert.IsNotNull(oecdItem);
        }

        [TestMethod]
        public void UpdateOECDMapping()
        {
            var date = DateTime.Parse("2015-12-18");
            var OECDMappingItem = new OECDMapping
            {
                CognosReferenceId = "3R1075",
                BlueprintReferenceId = "25MLP",
                CreatedById = 1,
                CreatedDate = date,
                UpdatedById = 1,
                UpdatedDate = date
            };

            service.SaveOECDMapping(OECDMappingItem);
            var oecdItem = service.GetOECDMappingByCognosId("3R1075");
            Assert.IsNotNull(oecdItem);
        }

        [TestMethod]
        public void UnmapOECDMapping()
        {
            var date = DateTime.Parse("2015-12-18");
            var OECDMappingItem = new OECDMapping
            {
                CognosReferenceId = "3R1075",
                BlueprintReferenceId = "1300LP",
                CreatedById = 1,
                CreatedDate = date,
                UpdatedById = 1,
                UpdatedDate = date
            };

            service.SaveOECDMapping(OECDMappingItem);
            var OECDMappingItemUnMap = new OECDMapping
            {
                CognosReferenceId = "3R1075",
                BlueprintReferenceId = null,
                CreatedById = 1,
                CreatedDate = date,
                UpdatedById = 1,
                UpdatedDate = date
            };

            service.SaveOECDMapping(OECDMappingItemUnMap);
            var oecdItem = service.GetOECDMappingByCognosId("3R1075");
            Assert.IsNull(oecdItem.BlueprintReferenceId);

            repository.DeleteOECDMapping(oecdItem.Id);
        }
    }
}
