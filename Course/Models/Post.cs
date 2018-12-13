using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.Models
{
    public class Post
    {
        public string groupName { get; set; }
        public string postText { get; set; }
        public List<string> attachedPhoto { get; set; }
        public List<string> attachedVideo { get; set; }

        public Post()
        {
            attachedPhoto = new List<string>();
            attachedVideo = new List<string>();
            groupName = "";
            postText = "";
        }
    }
}