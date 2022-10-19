using GHC.ProjectTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.IService
{
    [ServiceContract]
    public interface IITPTrackerService
    {
        [OperationContract]
        IEnumerable<User> GetAllActiveUsers(bool isActive);

        [OperationContract]
        User GetUser(int userId);

        [OperationContract]
        User GetUserByUserName(string userName);

        [OperationContract]
        User GetUpdateCreateUser(User user, bool isUpdate = false);

        [OperationContract]
        bool SetUserState(int userId, bool isRestore);

        [OperationContract]
        IEnumerable<Menu> GetParentMenu();

        [OperationContract]
        Country GetCountryById(int countryId);

        [OperationContract]
        IEnumerable<Country> GetCountryNames();
        [OperationContract]
        IEnumerable<Country> GetCountries();

        [OperationContract]
        IEnumerable<SystemLookup> GetLookupData(List<string> categories);

        [OperationContract]
        IEnumerable<Menu> GetMenu(int[] roleIds);

        

    }
}
