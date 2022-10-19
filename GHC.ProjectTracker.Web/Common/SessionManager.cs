using GHC.ProjectTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace GHC.ProjectTracker.Web.Common
{
    public class SessionManager : ISessionManager
    {
        public User LoggedInUser
        {
            get { return HttpContext.Current.Session.GetDataFromSession<User>("LoggedInUser"); }
            set { HttpContext.Current.Session.SetDataToSession("LoggedInUser", value); }
        }

        public string GetUserWelcomeNote()
        {
            return string.Format("Logged in as: {0}", LoggedInUser.FullName);
        }
    }

    internal static class SessionExtensions
    {
        public static T GetDataFromSession<T>(this HttpSessionState session, string key)
        {
            return (T)session[key];
        }

        public static void SetDataToSession(this HttpSessionState session, string key, object value)
        {
            session[key] = value;
        }
    }
}