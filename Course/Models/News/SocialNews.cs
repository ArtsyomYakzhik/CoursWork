using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.Models.News
{
    abstract public class SocialNews
    {
        public string applicationId { get; set; }
        public string applicationSecret { get; set; }
        public string redirectUri { get; set; }

    }
}