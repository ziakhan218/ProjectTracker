using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHC.DataPortal.Business;
using GHC.DataPortal.Data;
using GHC.DataPortal.Data.Infrastructure;
using GHC.DataPortal.IService;
using GHC.DataPortal.Model.UploadConfig;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GHC.DataPortal.Service.IntegrationTests
{
    [TestClass]
    public class UploadConfigServiceIntegrstionTest
    {
        private UnityContainer unityContainer;
        private IUploadConfigService service;
        IUploadConfigManager uploadConfigManager;
        UploadConfig uploadConfig;
        List<UploadConfig> uploadConfigList;
        List<UploadConfigParam> uploadConfigParam;
        UploadConfigParam configParam;
        UploadHistory configHistory;
        [TestInitialize]
        public void Setup()
        {
            this.unityContainer = new UnityContainer();
            unityContainer.RegisterType<IRetailService, RetailService>()
                     .RegisterType<IUploadConfigManager, UploadConfigManager>()
                     .RegisterType<IDataPortalUnitOfWork, DataPortalUnitOfWork>()
                     .RegisterType<IDataPortalDbContext, DataPortalDbContext>()
                     .RegisterType<IUploadConfigRepository, UploadConfigRepository>();

            this.service = new UploadConfigService(unityContainer.Resolve<IUploadConfigManager>());
            uploadConfig = new UploadConfig();
            uploadConfigList = new List<UploadConfig>();
            uploadConfigParam = new List<UploadConfigParam>();
            configParam = new UploadConfigParam();
            configHistory = new UploadHistory();
            uploadConfig.Id = 4;
            uploadConfig.Name = "RealEstate";
            uploadConfig.Folder = "RealEstateInfo";
            uploadConfig.Catalog = "RealEstatedb";
            uploadConfig.Project = "RealEstateProject";
            uploadConfig.PackageName = "RealEstatePackage";
            uploadConfig.SheetNames = "uploadSheet,UpdatedSheet";
            uploadConfig.FileUploadPath = "/RealEstateProject/RealEstateInfo/RealEstate";
            uploadConfig.ViewName = "RealEstateView";
            uploadConfig.CreatedById = 46;
            uploadConfig.CreatedDate = DateTime.UtcNow;
            uploadConfig.UpdatedById = 46;
            uploadConfig.UpdatedDate = DateTime.UtcNow;

            //configParam.Id = 1;
            configParam.Name = "Location";
            configParam.Type = "String";
            configParam.ViewColumn = "Location";
            configParam.IsRequired = false;
            configParam.DefaultValue = "Peris";
            configParam.SequenceNumber = 1;
            configParam.CreatedById = 46;
            configParam.CreatedDate = DateTime.UtcNow;
            configParam.UpdatedById = 46;
            configParam.UpdatedDate = DateTime.UtcNow;

            configHistory.FileName = "Test";
            configHistory.ExecutionId = 1;
            configHistory.Status = 1;
            configHistory.UploadConfigId = 1;
            configHistory.UpdatedById = 46;
            configHistory.UpdatedDate = DateTime.UtcNow;

            uploadConfigList.Add(uploadConfig);
            uploadConfigParam.Add(configParam);
        }

        [TestMethod]
        public void CanSaveUpdateDeleteUploadConfigInfo()
        {
            //insert test record
           //int? uploadConfigId= service.InsertUploadInfo(uploadConfig, uploadConfigParam);
           //int? configHistoryId= service.InsertUploadHistory(configHistory);
           // if (uploadConfigId != null)
           // {
           //     service.GetUploadConfig(uploadConfigId.Value);
           //     service.GetUploadConfigParam(uploadConfigId.Value);
           // }
           // if(configHistoryId!=null)
           //     service.GetUploadConfigHistory(configHistoryId.Value);
           
        }

        [TestMethod]
        public void CanDeleteUploadConfigInfo()
        {
            //service.DeleteUploadConfig(uploadConfig);
        }
        //[TestMethod]
        //public void CanUpdateUploadConfigInfo()
        //{
        //    service.UpdateUploadInfo(uploadConfig, uploadConfigParam);
        //}
    }
}
