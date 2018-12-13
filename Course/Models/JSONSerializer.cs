using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Course.Models
{
    public static class JSONSerializer
    {
        private static JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        private static dynamic jsonDeserialize;

        public static string getValueOfJSONString(string name, string json)
        {
            getDeserializeObject(json);
            return jsonDeserialize[name].ToString();
        }
        
        public static Dictionary<string,string> getGroupsIdAndNameDictionary(string json)
        {
            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();
            getDeserializeObject(json);
            foreach(var element in jsonDeserialize["response"]["items"])
            {
                resultDictionary.Add(Convert.ToString(element["id"]),
                    element["name"]);
            }
            return resultDictionary;
        }

        public static Post getPost(string json)
        {
            Post post = new Post();
            getDeserializeObject(json);
            post.groupName = Convert.ToString(jsonDeserialize["response"]["groups"][0]["name"]);
            post.postText = Convert.ToString(jsonDeserialize["response"]["items"][0]["text"]);
            foreach(var element in jsonDeserialize["response"]["items"][0]["attachments"])
            {
               // post.attachedPhoto.Add(Convert.ToString(element["photo"]["sizes"][2]["url"]));
            }
            return post;
        }

        private static void getDeserializeObject(string json)
        {
            jsonDeserialize = jsonSerializer.Deserialize<dynamic>(json);
        }

        public static List<string> getGroupsList(string groups)
        {
            return groups.Split(' ').ToList();
        }
    }
}