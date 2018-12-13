using Course.Models.DataBaseModel;
using Course.Models.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.Models.Interactions
{
    public class AccessTokensInteraction: Interaction
    {
        public AccessTokensInteraction(): base() { }

        public List<AccessTokens> GetAccessTokensList()
        {
            return dBContext.AccessTokenses.ToList();
        }

        public void SubcribeToGroup(string login, string groupId)
        {
            FindAccessTokensByLogin(login).vkGroups.Replace(groupId, "");
            FindAccessTokensByLogin(login).vkGroups += groupId + " ";
            SaveChanges();
        }

        public void AddNewAccessTokens(string login)
        {
            dBContext.AccessTokenses.Add(new AccessTokens() { UserId = login});
            SaveChanges();
            dBContext.AccessTokenses.Where(c => (c.UserId == login)).Single().vkGroups = "";
            SaveChanges();
        }

        public void DeleteAccessTokens(string accessTokensId)
        {
            dBContext.AccessTokenses.Remove(FindAccessTokensById(accessTokensId));
            SaveChanges();
        }

        public void EditAccessTokensValue(string accessTokensId, string[] vk, string[] youtube, string[] instagram)
        {
            if (vk != null)
            {
                FindAccessTokensByLogin(accessTokensId).vkAT = vk[0];

                FindAccessTokensByLogin(accessTokensId).vkId = vk[1];
            }
            if (youtube != null)
            {
                FindAccessTokensByLogin(accessTokensId).youtubeAT = youtube[0];

                FindAccessTokensByLogin(accessTokensId).youtubeId = youtube[1];
            }
            if (instagram != null)
            {
                FindAccessTokensByLogin(accessTokensId).instagramAT = instagram[0];

                FindAccessTokensByLogin(accessTokensId).instagramId = instagram[1];
            }
            SaveChanges();
        }

        public AccessTokens FindAccessTokensById(string accessTokensId)
        {
            return dBContext.AccessTokenses.Find(accessTokensId);
        }
        public AccessTokens FindAccessTokensByLogin(string login)
        {
            return dBContext.AccessTokenses.Single();
        }
    }
}