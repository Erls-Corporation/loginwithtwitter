using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;
using GetConnectedWithTwitter.Models;

namespace GetConnectedWithTwitter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Invite()
        {
            List<TwitterFollower> listfollwers = getTwitterFollowerList();
            return View(listfollwers);
        }

        #region Twitter
        public ActionResult GetConnectedWithTwitter()
        {
            string _consumerKey = System.Configuration.ConfigurationManager.AppSettings["consumerKeyLogin"];
            string _consumerSecret = System.Configuration.ConfigurationManager.AppSettings["consumerSecretLogin"];
            TwitterService service = new TwitterService(_consumerKey, _consumerSecret);

            OAuthRequestToken requestToken = service.GetRequestToken();

            var uri = service.GetAuthorizationUri(requestToken);
            return new RedirectResult(uri.ToString(), false /*permanent*/);
        }

        // This URL is registered as the application's callback at http://dev.twitter.com

        public ActionResult ReturnFromTwitter(string oauth_token, string oauth_verifier)
        {
            var requestToken = new OAuthRequestToken { Token = oauth_token };

            // Step 3 - Exchange the Request Token for an Access Token
            string _consumerKey = System.Configuration.ConfigurationManager.AppSettings["consumerKeyLogin"];
            string _consumerSecret = System.Configuration.ConfigurationManager.AppSettings["consumerSecretLogin"];
            TwitterService service = new TwitterService(_consumerKey, _consumerSecret);
            OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier);

            string TwitterToken = accessToken.Token;
            string TwitterToeknSecret = accessToken.TokenSecret;
            Session[Sessionvars.TwitterRequestToken] = TwitterToken;  //You can save this token in your database for pulling user's data in future. this will be save every time while getting permission
            Session[Sessionvars.TwitterRequestTokenSecert] = TwitterToeknSecret; //You can save this token in your database for pulling user's data in future. this will be save every time while getting permission
            ViewBag.TwitterToken = accessToken.Token;

            // Step 4 - User authenticates using the Access Token

            //service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
            //TwitterUser user = service.VerifyCredentials();
            //var status = user.Status;
            //ViewBag.status = status;
            //ViewBag.UserName = user.Name;
            //ViewBag.location = user.Location;
            //ViewBag.count = user.FollowersCount;

            //TwitterDirectMessage Ds = service.SendDirectMessage(user.Id, "hi this test messages");
            //service.SendTweet("hi this is test from me at live");
            //service.SendTweet("msg", user.Id);
            //ViewBag.Userid = service.BeginFollowUserNotifications(user.Id);



            return RedirectToAction("Invite");
        }

        public JsonResult TwitterTweet()
        {
            bool result = false;

            string UserTweet = Request.Params["tweet"];
            string _consumerKey = System.Configuration.ConfigurationManager.AppSettings["consumerKeyLogin"];
            string _consumerSecret = System.Configuration.ConfigurationManager.AppSettings["consumerSecretLogin"];
            TwitterService service = new TwitterService(_consumerKey, _consumerSecret);

            service.AuthenticateWith(Session[Sessionvars.TwitterRequestToken].ToString(), Session[Sessionvars.TwitterRequestTokenSecert].ToString());

            TwitterUser user = service.VerifyCredentials();
            if (!string.IsNullOrEmpty(UserTweet))
            {
                service.SendTweet(UserTweet);
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TwitterSendDirectMessage()
        {
            bool result = false;

            string _consumerKey = System.Configuration.ConfigurationManager.AppSettings["consumerKeyLogin"];
            string _consumerSecret = System.Configuration.ConfigurationManager.AppSettings["consumerSecretLogin"];
            TwitterService service = new TwitterService(_consumerKey, _consumerSecret);

            service.AuthenticateWith(Session[Sessionvars.TwitterRequestToken].ToString(), Session[Sessionvars.TwitterRequestTokenSecert].ToString());
            string TwitterDirectMessage = Request.Params["TwitterDM"];

            string ListSelectUserDM = Convert.ToString(Request.Params["SelectedUserForDM"]);
            char[] splitchars = { ',' };
            string[] DirectMessageUserId = ListSelectUserDM.Split(splitchars, StringSplitOptions.RemoveEmptyEntries);
            if (DirectMessageUserId.Count() > 0)
            {
                foreach (var list in DirectMessageUserId)
                {
                    TwitterDirectMessage Ds = service.SendDirectMessage(Convert.ToInt32(list), TwitterDirectMessage);
                }
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public List<TwitterFollower> getTwitterFollowerList()
        {
            List<TwitterFollower> twitterFollowerList = new List<TwitterFollower>();
            
            string _consumerKey = System.Configuration.ConfigurationManager.AppSettings["consumerKeyLogin"];
            string _consumerSecret = System.Configuration.ConfigurationManager.AppSettings["consumerSecretLogin"];
            TwitterService service = new TwitterService(_consumerKey, _consumerSecret);

            if (!string.IsNullOrEmpty(Session[Sessionvars.TwitterRequestToken].ToString()))
            {

                service.AuthenticateWith(Session[Sessionvars.TwitterRequestToken].ToString(), Session[Sessionvars.TwitterRequestTokenSecert].ToString());

                TwitterUser user = service.VerifyCredentials();
                var followers = service.ListFollowers();

                foreach (var follower in followers)
                {
                    twitterFollowerList.Add(
                   new TwitterFollower
                   {
                       Id = follower.Id,
                       ScreenName = follower.ScreenName
                   });
                }

            }

            return twitterFollowerList;
        }

        #endregion
    }
}
