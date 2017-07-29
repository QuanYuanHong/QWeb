using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Book.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        #region[注册]
        public ActionResult Signup(string signup = "")
        {
            string message = "";
            if (signup.Equals("fail"))
            {
                message = "注册失败";
                ViewBag.message = message;
            }
            return View();
        }
        public void SignupSave(Book.Model.T_Base_User user, string password)
        {
            Book.DAL.DalT_Base_User dal = new Book.DAL.DalT_Base_User();
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");//MD5密码加密
            user.Password = password;
            int res = dal.Add(user);

            if (res > 0)
            {
                //return Json(new { code = 1, message = "注册成功" });

                Response.Redirect("/home/login");
            }
            else
            {
                //return Json(new { code = 0, message = "注册失败" });

                Response.Redirect("/home/signup?signup=fail");
            }
        }
        #endregion

        #region[登录]
        public ActionResult Login(string login = "")
        {
            string message = "";
            if (login.Equals("fail"))
            {
                message = "帐号或密码错误";
                ViewBag.message = message;
            }
            return View();

        }
        public ActionResult CheckUser(string username, string password)//登录检查，是否存在，验证用户名、密码正确
        {
            //检查username, password是否合法
            password= FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");//MD5密码加密
            if (IfExist(username, password))
            {
                //标志入Cookies
                Response.Cookies["LoginTicket"].Value = "1";
                System.Web.Security.FormsAuthentication.SetAuthCookie(username, true); 
                Response.Redirect("/home/index");
            }
            else
            {
                Response.Redirect("/home/login?login=fail");
            }
            return null;
        }
        public bool IfExist(string username, string password)
        {
            DAL.DalT_Base_User dalUser = new DAL.DalT_Base_User();
            return dalUser.IsExist(username, password);

        }
        #endregion

        public ActionResult Index()
        {
            //if (Response.Cookies["LoginTicket"] == null)
            if (!User.Identity.IsAuthenticated)//指示是否验证了客户
            {
                Response.Redirect("/home/login");
                return null;
            }
            return View();
        }

    }
}