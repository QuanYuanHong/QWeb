using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace Book.Web.Controllers
{
    public class ProviderController : Controller
    {
        // GET: Book
        public ActionResult Index(string keyword,int pageNum = 1)
        {
            //Book.Web.Models.BookEntities db = new Models.BookEntities();
            //List<Book.Web.Models.T_Base_Provider> lst = db.T_Base_Provider.ToList();
            //ViewBag.lst = lst;
            int CurrentPageIndex = pageNum;
            string where = "";
            if(keyword == "")
            {
                where = "1 = 1";
            }else
            {
                where = " name like '%" + keyword + "%'";
            }

            Book.DAL.DalT_Base_Provider dal = new DAL.DalT_Base_Provider();
            //获取数据
            int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            int RecordCount=dal.GetRecordCount(where);
            int startIndex = (CurrentPageIndex - 1) * PageSize + 1;
            int endIndex = CurrentPageIndex * PageSize;

            List<Book.Model.T_Base_Provider> lst = dal.GetModelListByPage(where, " id desc", startIndex, endIndex);

            //展示数据
            ViewBag.CurrentPageIndex = CurrentPageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.RecordCount = RecordCount;
            ViewBag.lst = lst;

            return View();
        }

        //public ActionResult Index1()
        //{

        //    Book.DAL.DalT_Base_Provider dal = new DAL.DalT_Base_Provider();
        //    List<Book.Model.T_Base_Provider> lst = dal.GetList();

        //    ViewBag.lst = lst;
        //    return View();
        //}

        public ActionResult Add()
        {
            return View();
        }
        public void AddSave(Book.Model.T_Base_Provider provider)
        {
            Book.DAL.DalT_Base_Provider dal = new Book.DAL.DalT_Base_Provider();
            int res = dal.Add(provider);

            if (res > 0)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"插入成功\",\"navTabId\":\"ProviderList\",\"rel\":\"ProviderList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"插入失败\",\"navTabId\":\"ProviderList\",\"rel\":\"ProviderList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }

        }

        public ActionResult Delete(string ids)
        {
            DAL.DalT_Base_Provider dal = new DAL.DalT_Base_Provider();
            bool result = dal.DeleteList(ids);
            if(result)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"删除成功\",\"navTabId\":\"ProviderList\",\"rel\":\"ProviderList\",\"callbackType\":\"\",\"forwardUrl\":\"\"}";
                return Content(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"删除失败\",\"navTabId\":\"ProviderList\",\"rel\":\"ProviderList\",\"callbackType\":\"\",\"forwardUrl\":\"\"}";
                return Content(tmp);
            }
        }

        public ActionResult Update(int id)
        {
            Book.DAL.DalT_Base_Provider dal = new DAL.DalT_Base_Provider();
            Book.Model.T_Base_Provider provider = dal.GetModel(id);
            ViewBag.Provider = provider;
            if(provider == null)
            {
                return Content("资料不存在！");
            }
            return View();
        }
        public void  UpdateSave(Book.Model.T_Base_Provider provider)
        {
            Book.DAL.DalT_Base_Provider dal = new DAL.DalT_Base_Provider();
            bool result = dal.Update(provider);
            if (result)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"插入成功\",\"navTabId\":\"ProviderList\",\"rel\":\"ProviderList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"插入失败\",\"navTabId\":\"ProviderList\",\"rel\":\"ProviderList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
        }

        public ActionResult GetAllProvider()
        {
            Book.DAL.DalT_Base_Provider dal = new DAL.DalT_Base_Provider();
            List<Book.Model.T_Base_Provider> lst = dal.GetModelList("1=1");

            string result = "[";
            foreach(var item in lst)
            {
                result += " {";
                result += "  \"Id\": \"" + item.Id + "\",";
                result += "  \"Name\": \"" + item.Name + "\"";

                result += " },";
            }
            if(lst.Count>=1)
            {
                result = result.Substring(0, result.Length - 1);
            }
            result += "]";
            return Content(result);
        }
    }
}