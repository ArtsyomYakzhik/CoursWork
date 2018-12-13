using Course.Models.DataBaseModel;
using Course.Models.Interactions;
using Course.Models.News.NewsClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course.Controllers
{
    public class UserController : Controller
    {
        UserInteraction userInteraction = new UserInteraction();
        AccessTokensInteraction accessTokens = new AccessTokensInteraction();
        private VK vk = new VK();

        public ActionResult Main()
        {
            accessTokens.FindAccessTokensByLogin(Convert.ToString(Session["userid"]));
            ViewBag.code = vk.oAuthCodeUri;
            return View();
        }

        public ActionResult VK(string code)
        {
            accessTokens.EditAccessTokensValue(accessTokens.FindAccessTokensByLogin(Session["userid"].ToString()).Id
                , vk.getAccessTokenJSON(code), null, null);
            Session["groups"] = vk.getUserGroups(accessTokens.FindAccessTokensByLogin(Session["userid"].ToString()).vkAT);
            ViewBag.groups = (Dictionary<string, string>)Session["groups"];
            return View();
        }

        
    }
}