using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Book.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(string keyword, int pageNum = 1)
        {
            int CurrentPageIndex = pageNum;
            string where = "";
            if (keyword == "")
            {
                where = "1 = 1";
            }
            else
            {
                where = " username like '%" + keyword + "%'";
            }

            Book.DAL.DalT_Base_User dal = new DAL.DalT_Base_User();
            //获取数据
            int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            int RecordCount = dal.GetRecordCount(where);
            int startIndex = (CurrentPageIndex - 1) * PageSize + 1;
            int endIndex = CurrentPageIndex * PageSize;

            List<Book.Model.T_Base_User> lst = dal.GetModelListByPage(where, " id desc", startIndex, endIndex);

            //展示数据
            ViewBag.CurrentPageIndex = CurrentPageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.RecordCount = RecordCount;
            ViewBag.lst = lst;
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
        public void AddSave(Book.Model.T_Base_User user,string PassWord)
        {
            Book.DAL.DalT_Base_User dal = new Book.DAL.DalT_Base_User();
            PassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PassWord, "MD5");//MD5密码加密
            user.Password = PassWord;
            int res = dal.Add(user);            

            if (res > 0)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"插入成功\",\"navTabId\":\"UserList\",\"rel\":\"UserList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"插入失败\",\"navTabId\":\"UserList\",\"rel\":\"UserList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }

        }

        public ActionResult Delete(string ids)
        {
            DAL.DalT_Base_User dal = new DAL.DalT_Base_User();
            bool result = dal.DeleteList(ids);
            if (result)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"删除成功\",\"navTabId\":\"UserList\",\"rel\":\"UserList\",\"callbackType\":\"\",\"forwardUrl\":\"\"}";
                return Content(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"删除失败\",\"navTabId\":\"UserList\",\"rel\":\"UserList\",\"callbackType\":\"\",\"forwardUrl\":\"\"}";
                return Content(tmp);
            }
        }

        public ActionResult Update(int id)
        {
            Book.DAL.DalT_Base_User dal = new DAL.DalT_Base_User();
            Book.Model.T_Base_User user = dal.GetModel(id);
            ViewBag.User = user;
            if (user == null)
            {
                return Content("资料不存在！");
            }
            return View();
        }
        public void UpdateSave(Book.Model.T_Base_User user, string PassWord)
        {
            Book.DAL.DalT_Base_User dal = new DAL.DalT_Base_User();
            PassWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PassWord, "MD5");//MD5密码加密
            user.Password = PassWord;
            bool result = dal.Update(user);
            if (result)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"插入成功\",\"navTabId\":\"UserList\",\"rel\":\"UserList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"插入失败\",\"navTabId\":\"UserList\",\"rel\":\"UserList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
        }
    }
}