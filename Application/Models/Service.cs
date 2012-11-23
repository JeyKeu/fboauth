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
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Text;
    using Facebook;

    /// <summary>
    /// Service request to facebook
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Method to get facebook authentication token
        /// </summary>
        /// <param name="code">Facebook code</param>
        /// <param name="returnUrl">return url</param>
        /// <param name="fbRedirectUri">redirect url</param>
        /// <returns>Access token as a string</returns>
        public static string GetFacebookAccessToken(string code, string returnUrl, string fbRedirectUri)
        {
            var f = new FacebookClient();
            dynamic result = f.Get(
                "oauth/access_token", 
                new
                {
                    client_id = Constants.FACEBOOK_APP_ID,
                    client_secret = Constants.FACEBOOK_SECRET,
                    redirect_uri = fbRedirectUri,
                    code = code,
                    state = returnUrl
                });
            return result.access_token as string;
        }

        /// <summary>
        /// Get facebook response
        /// </summary>
        /// <param name="actionUrl">action url</param>
        /// <param name="accessToken">access token</param>
        /// <returns>dynamic response with all the requested user details</returns>
        public static dynamic GetFacebookResponse(string actionUrl, string accessToken)
        {
            FacebookClient FbApp;
            if (string.IsNullOrEmpty(accessToken))
            {
                FbApp = new FacebookClient();
            }
            else
            {
                FbApp = new FacebookClient(accessToken);
            }

            return FbApp.Get(actionUrl) as JsonObject;
        }
    }
}
