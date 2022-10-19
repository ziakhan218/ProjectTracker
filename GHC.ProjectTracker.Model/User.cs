
namespace GHC.ProjectTracker.Model
{
    [System.Serializable]
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsSystemUser { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
    }
}
