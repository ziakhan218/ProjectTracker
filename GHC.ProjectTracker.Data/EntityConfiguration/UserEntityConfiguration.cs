using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GHC.ProjectTracker.Model;

namespace GHC.ProjectTracker.Data.EntityConfigurations
{
    public class UserEntityConfiguration : EntityConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            ToTable("User");
            HasKey(b => b.Id);
            Property(t => t.FirstName).IsRequired();
            Property(t => t.LastName).IsRequired();
            Property(t => t.UserName).IsRequired();
            Property(t => t.EmailAddress).IsRequired();
        }
    }
}
