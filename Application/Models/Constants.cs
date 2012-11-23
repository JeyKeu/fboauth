//-----------------------------------------------------------------------
// <author>Sidharth Balakrishnan</author>
// <summary>
//     Create Date : 23 Nov 2012 
// </summary>
//-----------------------------------------------------------------------

namespace Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Declare all the constants in this file
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Under which URL will you be running your website? 
        /// Please replace with your base URL. Ex: http://localhost:00000/ or http://www.google.com/
        /// Please don't forget http://
        /// </summary>
        public const string BASE_URL = "http://localhost:64986/";

        /// <summary>
        /// Replace it with your Facebook App ID
        /// </summary>
        public const string FACEBOOK_APP_ID = "297787640335236";

        /// <summary>
        /// Replace it with your Facebook App Secret
        /// </summary>
        public const string FACEBOOK_SECRET = "43c2a09b889a39b92c5a956e612e6107";

        /// <summary>
        /// Redirect url after the login is complete
        /// </summary>
        public const string FACEBOOK_LOGIN_COMPLETE_URL = BASE_URL + "Account/LoginComplete";

        /// <summary>
        /// One of the error reasons for denied fb authentication
        /// </summary>
        public const string FACEBOOK_USER_DENIED = "user_denied";

        /// <summary>
        /// Facebook error reason response
        /// </summary>
        public const string FACEBOOK_ERROR_REASON = "error_reason";

        /// <summary>
        /// Scope for personal data request from facebook. If you plan to remove some parameter, please make sure to change the UpdateUser() method in AccountController.cs
        /// </summary>
        public const string FACEBOOK_SCOPE = "email,user_likes,user_birthday,user_hometown,user_location";
        
        /// <summary>
        /// Generate Facebook login URL
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns>returns Facebook login URL</returns>
        public static string GetFacebookLoginUrl(string returnUrl)
        {
            var url = string.Format(
                                    @"https://www.facebook.com/dialog/oauth/?client_id={0}&redirect_uri={1}&scope={2}&state={3}", 
                                    FACEBOOK_APP_ID, 
                                    FACEBOOK_LOGIN_COMPLETE_URL, 
                                    FACEBOOK_SCOPE, 
                                    (returnUrl ?? BASE_URL));
            return url;
        }
    }
}