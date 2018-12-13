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
            accessTokens.EditAccessTokensValue(accessTokens.FindAccessTokensByLogin(Convert.ToString(Session["userid"])).Id
                , vk.getAccessTokenJSON(code));
            return View();
        }

        public ActionResult VKAuth()
        {
            ViewBag.news = vk.getVKNews(accessTokens.FindAccessTokensByLogin(Session["userid"].ToString()).vkAT
                , accessTokens.FindAccessTokensByLogin(Session["userid"].ToString()).vkGroups);
            return View("VK");
        }

        public ActionResult SubscribeToVkGroup()
        {
            Session["groups"] = vk.getVKUserGroups(accessTokens.FindAccessTokensByLogin(Convert.ToString(Session["userid"])).vkAT);
            ViewBag.groups = (Dictionary<string, string>)Session["groups"];
            return View();
        }

        [HttpPost]
        public ActionResult SetGroups()
        {
            foreach(var element in Request.Form)
            {
                if(element.ToString()!= "Submit")
                accessTokens.SubcribeToGroup(Session["userid"].ToString(), element.ToString());
            }
            return RedirectToActionPermanent("VKAuth");
        }
        
        public ActionResult Logout()
        {
            Session["userid"] = null;
            return RedirectToActionPermanent("Signin", "Home");
        }
    }
}