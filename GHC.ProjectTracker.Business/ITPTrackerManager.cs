using GHC.ProjectTracker.Data;
using GHC.ProjectTracker.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Business
{
    public class ITPTrackerManager : IITPTrackerManager
    {
        private readonly IITPTrackerRepository repository;

        public ITPTrackerManager(IITPTrackerRepository repository)
        {
            this.repository = repository;
        }

        public User GetUser(int userId)
        {
            return repository.GetUserById(userId);
        }

        public IEnumerable<User> GetAllActiveUsers(bool isActive)
        {
            return repository.GetAllActiveUsers(isActive);
        }

        public User GetUserByUserName(string username)
        {
            return repository.GetUserByUserName(username);
        }

        public User GetUpdateCreateUser(User user, bool isUpdate = false)
        {
            return repository.GetUpdateCreateUser(user, isUpdate);
        }

        
        public bool SetUserState(int userId, bool isRestore)
        {
            return repository.SetUserState(userId, isRestore);
        }

        public IEnumerable<Menu> GetParentMenu()
        {
            return repository.GetParentMenu();
        }

        public Country GetCountryById(int countryId)
        {
            return repository.GetCountryById(countryId);
        }

        public IEnumerable<Country> GetCountryNames()
        {
            return repository.GetCountryNames();
        }
        public IEnumerable<Country> GetCountries()
        {
            return repository.GetCountries();
        }

        public List<SystemLookup> GetLookupData(List<string> categories)
        {
            return repository.GetLookupData(categories);
        }

        public IEnumerable<Menu> GetMenu(int[] roleIds)
        {
            return repository.GetMenu(roleIds);
        }
    }
}
