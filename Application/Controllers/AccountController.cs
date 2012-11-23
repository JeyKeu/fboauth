//-----------------------------------------------------------------------
// <author>Sidharth Balakrishnan</author>
// <summary>
//     Create Date : 23 Nov 2012 
// </summary>
//-----------------------------------------------------------------------

namespace Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Security;
    using Application.Models;

    /// <summary>
    /// Controller for accounts management
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Create an instance for FBAuthEntities
        /// </summary>
        private FBAuthEntities db = new FBAuthEntities();
 
        /// <summary>
        /// Login Page
        /// </summary>
        /// <returns>Login View</returns>
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// Redirect URL from login page, where operations like cookie setting, database updates will be done
        /// </summary>
        /// <param name="state">Return URL</param>
        /// <param name="code">FB code</param>
        /// <returns>Redirects to different page based on the response from facebook</returns>
        public ActionResult LoginComplete(string state, string code)
        {
            if (Request[Constants.FACEBOOK_ERROR_REASON] == Constants.FACEBOOK_USER_DENIED)
            {
                return View("Error");
            }
            
            if (string.IsNullOrEmpty(code)) 
            {
                return View("Error");
            }
            
            var returnUrl = state;
            var token = Service.GetFacebookAccessToken(code, returnUrl, Constants.FACEBOOK_LOGIN_COMPLETE_URL);
            dynamic response = Service.GetFacebookResponse("me", token);
            string uid = response.id;
            string email = response.email;
            string username = response.username;
            string firstname = response.first_name;
            string lastname = response.last_name;
            string gender = response.gender;
            string birthday = response.birthday;
            if (!string.IsNullOrEmpty(uid))
            {
                Session["facebooktoken"] = token;
                FormsAuthentication.SetAuthCookie(username, true);
                this.UpdateUser(uid, email, username, firstname, lastname, gender, birthday);
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Log off
        /// </summary>
        /// <returns>Destroys forms authentication cookie</returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region DB Jobs
        /// <summary>
        /// Checks if the user already exists in the database and does an update 
        /// </summary>
        /// <param name="uid">FB UserID </param>
        /// <param name="email">FB Email</param>
        /// <param name="username">FB Username</param>
        /// <param name="firstname">FB Firstname</param>
        /// <param name="lastname">FB Lastname</param>
        /// <param name="gender">FB Gender</param>
        /// <param name="birthday">FB Birthday</param>
        public void UpdateUser(string uid, string email, string username, string firstname, string lastname, string gender, string birthday)
        {
            var user = this.db.Users.Where(x => x.UserName == username).ToList();
            if (user.Count == 0)
            {
                var u = new User();
                u.UserID = Guid.NewGuid();
                u.UserKey = uid;
                u.UserName = username;
                u.Email = email;
                u.FirstName = firstname;
                u.LastName = lastname;
                u.Gender = gender;
                u.Birthdate = Convert.ToDateTime(birthday);
                u.CreatedDate = DateTime.Now;
                u.LastModifiedDate = DateTime.Now;
                u.LastLoginDate = DateTime.Now;
                u.IsActivated = true;
                u.IsLockedOut = false;
                this.db.AddToUsers(u);
                this.db.SaveChanges();
            }
        }
        #endregion
    }
}
