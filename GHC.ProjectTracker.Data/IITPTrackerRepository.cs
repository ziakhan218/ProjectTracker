using GHC.ProjectTracker.Model;
using System.Collections.Generic;

namespace GHC.ProjectTracker.Data
{
    public interface IITPTrackerRepository
    {
        User GetUserById(int userId);

        IEnumerable<User> GetAllActiveUsers(bool isActive);

        User GetUserByUserName(string username);

        User GetUpdateCreateUser(User user, bool isUpdate = false);

        bool SetUserState(int userId, bool isRestore);

        IEnumerable<Menu> GetParentMenu();

        Country GetCountryById(int countryId);
        IEnumerable<Country> GetCountryNames();
        IEnumerable<Country> GetCountries();

        List<SystemLookup> GetLookupData(List<string> categories);

        IEnumerable<Menu> GetMenu(int[] roleIds);
    }
}
