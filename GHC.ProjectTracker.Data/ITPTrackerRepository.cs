using GHC.ProjectTracker.Data.EntityRepository;
using GHC.ProjectTracker.Data.Infrastructure;
using GHC.ProjectTracker.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GHC.ProjectTracker.Data
{
    public class ITPTrackerRepository : RepositoryBase, IITPTrackerRepository
    {
        private IITPTrackerUnitOfWork unitOfWork;

        public ITPTrackerRepository(IITPTrackerUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User GetUserById(int userId)
        {
            var user = (from u in ((IITPTrackerDbContext)DataContext).Users
                       where u.Id == userId
                       select u).First();
            return user;
        }

        public IEnumerable<User> GetAllActiveUsers(bool isActive = true)
        {
            return ((IITPTrackerDbContext)DataContext)
                        .Users
                        .Where(u => u.IsActive == isActive && !u.IsSystemUser)
                        .OrderBy(u => new { u.FirstName, u.LastName})
                        .ToList();
        }

        public User GetUserByUserName(string username)
        {
            var user = (from u in ((IITPTrackerDbContext)DataContext).Users
                        where u.UserName == username
                        select u).FirstOrDefault();
            return user;
        }

        public User GetUpdateCreateUser(User user, bool isUpdate = false)
        {
            var tempUsersList = ((IITPTrackerDbContext)DataContext).Users.ToList();
            var dbUser = (from u in ((IITPTrackerDbContext)DataContext).Users
                          where u.UserName.ToLower() == user.UserName.ToLower()
                          select u).FirstOrDefault();

            
            if (dbUser == null) 
            {
                user.IsActive = true;
                SetAdded(user);
                unitOfWork.SaveChanges();
            }
            else if(dbUser != null && isUpdate == true)
            {
                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                dbUser.EmailAddress = user.EmailAddress;
                dbUser.UserName = user.UserName;
                SetModified(dbUser);
                unitOfWork.SaveChanges();
            }

            return dbUser;
        }

        public bool SetUserState(int userId, bool isRestore)
        {
            var activeUser = DataContext.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (activeUser != null)
            {
                activeUser.IsActive = isRestore;
                unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Menu> GetMenu(int[] roles)
        {
            var results = (from menu in ((IITPTrackerDbContext)DataContext).Menu
                           join rm in ((IITPTrackerDbContext)DataContext).RoleMenu on menu.Id equals rm.MenuId
                           where roles.Contains(rm.RoleId)
                           orderby menu.SequenceNumber
                           select menu).Distinct();

            return results.ToList();
        }

        public IEnumerable<Menu> GetParentMenu()
        {
            var results = (from menu in ((IITPTrackerDbContext)DataContext).Menu
                           where menu.ParentId == null
                           orderby menu.SequenceNumber
                           select menu);

            return results.ToList();
        }

        public Country GetCountryById(int countryId)
        {
            var country = (from u in ((IITPTrackerDbContext)DataContext).Countries
                        where u.Id == countryId
                        select u).First();
            return country;
        }

        public IEnumerable<Country> GetCountryNames()
        {
            return ((IITPTrackerDbContext)DataContext)
                        .Countries
                        .OrderBy(c => c.Name)
                        .Select(x => new { x.Id, x.Name }).ToList().Select(c => new Country {Id = c.Id,Name = c.Name });
        }

        public List<SystemLookup> GetLookupData(List<string> categories)
        {
            var lookupList = (from sl in ((IITPTrackerDbContext)DataContext).SystemLookups
                              where sl.IsActive && categories.Contains(sl.Category)
                              select sl).ToList();
            return lookupList;
        }

        public IEnumerable<Country> GetCountries()
        {
            return ((IITPTrackerDbContext)DataContext)
                        .Countries
                        .OrderBy(c => c.Name)
                        .ToList();
        }
    }
}
