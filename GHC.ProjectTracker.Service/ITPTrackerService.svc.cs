using GHC.ProjectTracker.Business;
using GHC.ProjectTracker.IService;
using GHC.ProjectTracker.Model;
using System.Collections.Generic;
using System;

namespace GHC.ProjectTracker.Service
{
    public class ITPTrackerService : IITPTrackerService
    {
        private readonly IITPTrackerManager iTProjectTrackerManager;

        // Constructor for Dependency Injection - only works in conjunction with a custom ServiceHostFactory (WcfServiceFactory.cs)
        public ITPTrackerService(IITPTrackerManager iTProjectTrackerManager)
        {
            this.iTProjectTrackerManager = iTProjectTrackerManager;
        }

        public User GetUser(int userId)
        {
            return iTProjectTrackerManager.GetUser(userId);
        }

        public IEnumerable<User> GetAllActiveUsers(bool isActive)
        {
            return iTProjectTrackerManager.GetAllActiveUsers(isActive);
        }

        public User GetUserByUserName(string username)
        {
            return iTProjectTrackerManager.GetUserByUserName(username);
        }

        public User GetUpdateCreateUser(User user, bool isUpdate = false)
        {
            return iTProjectTrackerManager.GetUpdateCreateUser(user, isUpdate);
        }

        public bool SetUserState(int userId, bool isRestore)
        {
            return iTProjectTrackerManager.SetUserState(userId, isRestore);
        }

        public IEnumerable<Menu> GetParentMenu()
        {
            return iTProjectTrackerManager.GetParentMenu();
        }

        public Country GetCountryById(int countryId)
        {
            return iTProjectTrackerManager.GetCountryById(countryId);
        }

        public IEnumerable<Country> GetCountryNames()
        {
            return iTProjectTrackerManager.GetCountryNames();
        }
        public IEnumerable<Country> GetCountries()
        {
           return iTProjectTrackerManager.GetCountries();
        }

        public IEnumerable<SystemLookup> GetLookupData(List<string> categories)
        {
            return iTProjectTrackerManager.GetLookupData(categories);
        }

        public IEnumerable<Menu> GetMenu(int[] roleIds)
        {
            return iTProjectTrackerManager.GetMenu(roleIds);
        }
    }
}
