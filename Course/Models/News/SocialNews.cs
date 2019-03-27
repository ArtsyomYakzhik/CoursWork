using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.Models.News
{
    abstract public class SocialNews
    {
        protected string applicationId;
        protected string applicationSecret;
        protected string redirectUri;
        protected string serverUri;

        public SocialNews()
        {
            serverUri = "http://" + HttpContext.Current.Request.Url.Host +
                ":" + HttpContext.Current.Request.Url.Port;
        }

        abstract public string[] getAccessTokenJSON(string code);
    }
}