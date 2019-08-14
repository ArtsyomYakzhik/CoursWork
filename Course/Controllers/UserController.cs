using Course.Models;
using Course.Models.DataBaseModel;
using Course.Models.Interactions;
using Course.Models.News.NewsClasses;
using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;


namespace Course.Controllers
{
    public class UserController : Controller
    {
        UserInteraction userInteraction = new UserInteraction();
        AccessTokensInteraction accessTokens = new AccessTokensInteraction();
        private VK vk = new VK();

        public UserController()
        {

        }

        public ActionResult Main()
        {
            accessTokens.FindAccessTokensByLogin(Convert.ToString(Session["userid"]));
            ViewBag.code = vk.oAuthCodeUri;
            return View();
        }

        public ActionResult VK(string code)
        {
            accessTokens.EditAccessTokensValue(
                accessTokens.FindAccessTokensByLogin(Convert.ToString(Session["userid"])).Id
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
            foreach (var element in Request.Form)
            {
                if (element.ToString() != "Submit")
                    accessTokens.SubcribeToGroup(Session["userid"].ToString(), element.ToString());
            }
            return RedirectToActionPermanent("VKAuth");
        }

        public ActionResult Logout()
        {
            Session["userid"] = null;
            return RedirectToActionPermanent("Signin", "Home");
        }

        public ActionResult RssPublications()
        {
            List<Post> posts = new List<Post>();
            string imgUrl = "";
            XmlReader reader = XmlReader.Create("https://pikabu.ru/rss.php");
            var feed = new Rss20FeedFormatter();
            feed.ReadFrom(reader);
            for (int i = 0; i < 10; i++)
            {
                var item = feed.Feed.Items.ToList()[i];
                Post post = new Post();
                if (item.Links.Count > 1)
                {
                    imgUrl = item.Links[1].Uri.AbsoluteUri;
                    post.attachedPhoto.Add(imgUrl);
                }
                post.groupName = item.Title.Text;
                if (item.Summary.Text != null)
                    post.postText = item.Summary.Text;
                posts.Add(post);
            }
            ViewBag.news = posts;
            return View();
        }

        public ActionResult RssTutBy()
        {
            string rssUri = String.Format("https://news.tut.by/rss/afisha.rss");
            List<Post> posts = new List<Post>();
            string imgUrl = "";
            XmlReader reader = XmlReader.Create(rssUri);
            var feed = new Rss20FeedFormatter();
            feed.ReadFrom(reader);
            for (int i = 0; i < 10; i++)
            {
                var item = feed.Feed.Items.ToList()[i];
                Post post = new Post();
                if (item.Links.Count > 1)
                {
                    imgUrl = item.Links[1].Uri.AbsoluteUri;
                    post.attachedPhoto.Add(imgUrl);
                }
                post.groupName = item.Title.Text;
                if (item.Summary.Text != null)
                    post.postText = item.Summary.Text;
                posts.Add(post);
            }
            ViewBag.news = posts;
            return View();
        }
    }
}