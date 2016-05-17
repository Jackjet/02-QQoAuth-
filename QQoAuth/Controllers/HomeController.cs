using QConnectSDK;
using QConnectSDK.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QQoAuth.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        //
        // GET: /Home/
        public ActionResult Index(string returnUrl)
        {
            this.Session["return"] = returnUrl;
            var context = new QzoneContext();
            string state = Guid.NewGuid().ToString("N");
            Session["requeststate"] = state;
            string scope = "get_user_info,add_share,list_album,upload_pic,check_page_fans,add_t,add_pic_t,del_t,get_repost_list,get_info,get_other_info,get_fanslist,get_idolist,add_idol,del_idol,add_one_blog,add_topic,get_tenpay_addr";
            var authenticationUrl = context.GetAuthorizationUrl(state, scope);
            return new RedirectResult(authenticationUrl);

        }


        public ActionResult Login(string code,string state)
        {
            QOpenClient qzone = null;
            QConnectSDK.Models.User currentUser = null;

            var verifier = Request.Params["code"];
            string state1 = Session["requeststate"].ToString();
            qzone = new QOpenClient(verifier, state1);
            currentUser = qzone.GetCurrentUser();
            if (null != currentUser)
            {
                return Content(currentUser.Nickname);
            }
            Session["QzoneOauth"] = qzone;
            return View();
        }
    }
}
