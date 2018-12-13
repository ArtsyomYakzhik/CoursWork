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
            return dBContext.AccessTokens.ToList();
        }

        public void AddNewAccessTokens(string login)
        {
            dBContext.AccessTokens.Add(new AccessTokens() { UserId = login});
            SaveChanges();
        }

        public void DeleteAccessTokens(string accessTokensId)
        {
            dBContext.AccessTokens.Remove(FindAccessTokensById(accessTokensId));
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
            return dBContext.AccessTokens.Find(accessTokensId);
        }
        public AccessTokens FindAccessTokensByLogin(string login)
        {
            return dBContext.AccessTokens.Single();
        }
    }
}