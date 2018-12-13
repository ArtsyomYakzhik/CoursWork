using Course.Models;
using Course.Models.Interactions;
using CourseWork.Models;
using CourseWork.Models.DataBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course.Controllers
{
    public class HomeController : Controller
    {
        UserInteraction userInteraction = new UserInteraction();
        AccessTokensInteraction accessTokensInteraction = new AccessTokensInteraction();

        public ActionResult Signin()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(string login, string password, string repassword)
        {
            if (userInteraction.FindUserByLogin(login) == null &&
                password == repassword)
            {
                userInteraction.AddNewUser(login, password);
                accessTokensInteraction.AddNewAccessTokens(login);
                Session["userid"] = login;
                return RedirectToActionPermanent("Main", "User");
            }
            else
                return RedirectToActionPermanent("Signup");
        }

        public ActionResult Login(string login, string password)
        {
            if (userInteraction.FindUserByLogin(login) != null &&
                userInteraction.FindUserByLogin(login).Password == password)
            {
                Session["userid"] = login;
                return RedirectToActionPermanent("Main", "User");
            }
            else
                return RedirectToActionPermanent("Signup");
        }
    }
}