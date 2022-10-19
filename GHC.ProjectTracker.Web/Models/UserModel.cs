using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GHC.ProjectTracker.Web.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FirstNameLastName { get; set; }

        public string DisplayName { get; set; }

        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        public Boolean IsActive { get; set; }
    }
}