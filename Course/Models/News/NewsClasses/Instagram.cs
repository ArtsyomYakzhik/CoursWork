using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.Models.News.NewsClasses
{
    public class Instagram: SocialNews
    {
        public string oAuthCodeUri;
        public string oAuthATUri;

        public Instagram()
        {
            applicationId = "9fd8c3912857458081ef63960c414a67";
            applicationSecret = "a5928f2d65d24539befe02ce3cecfdfe";
            redirectUri = String.Format("{0}/User/VK", serverUri);
            oAuthCodeUri = String.Format("https://api.instagram.com/oauth/authorize/?client_id={0}&redirect_uri={1}&response_type=code"
                ,applicationId, redirectUri);
            oAuthATUri = "https://api.instagram.com/oauth/access_token";
        }
        
    }
}