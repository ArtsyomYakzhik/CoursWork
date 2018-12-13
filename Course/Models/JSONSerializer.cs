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

        private static void getDeserializeObject(string json)
        {
            jsonDeserialize = jsonSerializer.Deserialize<dynamic>(json);
        }
    }
}