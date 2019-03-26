using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leaf;
using Leaf.xNet;

namespace Course.Models.News.NewsClasses
{
    public class VK: SocialNews
    {
        private const string version = "5.92";
        private const string permission = "73730";
        private string oAuthCodeUri;
        private string oAuthATUri;
        private string userGroupsIdUri;

        public VK():base()
        {
            applicationSecret = "myAB2FFGPRwGyqshDA1d";
            applicationId = "6727881";
            redirectUri = String.Format("{0}/User/VK", serverUri);
            oAuthCodeUri = String.Format("https://oauth.vk.com/authorize?client_id={0}&redirect_uri={1}&display=page&scope={2}&v={3}"
                , applicationId, redirectUri, permission, version);
            oAuthATUri = String.Format("https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&redirect_uri={2}&code="
                , applicationId, applicationSecret, redirectUri);
        }
        
        public string[] getAccessTokenJSON(string code)
        {
            Leaf.xNet.HttpResponse tokenResponse = null;
            string[] result = new string[2];
            using (var tokenRequest = new Leaf.xNet.HttpRequest())
            {
                tokenRequest.UserAgent = Http.ChromeUserAgent();
                tokenResponse = tokenRequest.Get(oAuthATUri + code);
                result[0] = JSONSerializer.getValueOfJSONString("access_token", tokenResponse.ToString());
                result[1] = " " + JSONSerializer.getValueOfJSONString("user_id", tokenResponse.ToString());
            }
            return result;
       }

        public Dictionary<string, string> getVKUserGroups(string accessToken)
        {
            Leaf.xNet.HttpResponse groupsResponse;
            string groupSearchUri = String.Format("https://api.vk.com/method/groups.get?extended=1&access_token={0}&v=5.92", accessToken);
            using (var groupsRequest = new Leaf.xNet.HttpRequest())
            {
                groupsRequest.UserAgent = Http.ChromeUserAgent();
                groupsResponse = groupsRequest.Get(groupSearchUri);
                return JSONSerializer.getGroupsIdAndNameDictionary(groupsResponse.ToString());
            }
        }

        public List<Post> getVKNews(string accessToken ,string groupString)
        {
            List<Post> news = new List<Post>();
            List<string> groupList = JSONSerializer.getGroupsList(groupString);
            for(int i = 0; i < 10; i++)
            {
                foreach(var element in groupList)
                {
                    news.Add(getVKPost(accessToken, element, i));
                }
            }
            return news;
        }

        public Post getVKPost(string accessToken ,string groupId, int numberOfPost)
        {
            Leaf.xNet.HttpResponse postResponse = null;
            Post result = new Post();
            using (var postRequest = new Leaf.xNet.HttpRequest())
            {
                postRequest.UserAgent = Http.ChromeUserAgent();
                postResponse = postRequest.Get(
                    String.Format("https://api.vk.com/method/wall.get?extended=1&owner_id=-{0}&count=1&offset={1}&access_token={2}&v={3}"
                    , groupId, numberOfPost, accessToken, version));
                result = JSONSerializer.getPost(postResponse.ToString());
                return result;
            }
        }
    }
}