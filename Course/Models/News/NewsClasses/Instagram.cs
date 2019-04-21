using Leaf.xNet;
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

        override public string[] getAccessTokenJSON(string code)
        {
            Leaf.xNet.HttpResponse tokenResponse = null;
            string[] result = new string[2];
            using (var tokenRequest = new Leaf.xNet.HttpRequest())
            {
                RequestParams requestParams = new RequestParams();
                requestParams.Add(new KeyValuePair<string, string>( "client_id", applicationId));
                requestParams.Add(new KeyValuePair<string, string>("client_secret", applicationSecret));
                requestParams.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                requestParams.Add(new KeyValuePair<string, string>("redirect_uri", redirectUri));
                requestParams.Add(new KeyValuePair<string, string>("code", code));
                tokenRequest.UserAgent = Http.ChromeUserAgent();
                tokenResponse = tokenRequest.Post(oAuthATUri, requestParams, false);
                result[0] = JSONSerializer.getValueOfJSONString("access_token", tokenResponse.ToString());
                result[1] = " " + JSONSerializer.getValueOfJSONString("user_id", tokenResponse.ToString());
            }
            return result;
        }
    }
}