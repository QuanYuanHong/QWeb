using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
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
                where = " name like '%" + keyword + "%'";
            }

            Book.DAL.DalT_Base_Customer dal = new DAL.DalT_Base_Customer();
            //获取数据
            int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            int RecordCount = dal.GetRecordCount(where);
            int startIndex = (CurrentPageIndex - 1) * PageSize + 1;
            int endIndex = CurrentPageIndex * PageSize;

            List<Book.Model.T_Base_Customer> lst = dal.GetModelListByPage(where, " id desc", startIndex, endIndex);

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
        public void AddSave(Book.Model.T_Base_Customer customer)
        {
            Book.DAL.DalT_Base_Customer dal = new Book.DAL.DalT_Base_Customer();
            int res = dal.Add(customer);

            if (res > 0)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"插入成功\",\"navTabId\":\"CustomerList\",\"rel\":\"CustomerList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"插入失败\",\"navTabId\":\"CustomerList\",\"rel\":\"CustomerList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }

        }

        public ActionResult Delete(string ids)
        {
            DAL.DalT_Base_Customer dal = new DAL.DalT_Base_Customer();
            bool result = dal.DeleteList(ids);
            if (result)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"删除成功\",\"navTabId\":\"CustomerList\",\"rel\":\"CustomerList\",\"callbackType\":\"\",\"forwardUrl\":\"\"}";
                return Content(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"删除失败\",\"navTabId\":\"CustomerList\",\"rel\":\"CustomerList\",\"callbackType\":\"\",\"forwardUrl\":\"\"}";
                return Content(tmp);
            }
        }

        public ActionResult Update(int id)
        {
            Book.DAL.DalT_Base_Customer dal = new DAL.DalT_Base_Customer();
            Book.Model.T_Base_Customer customer = dal.GetModel(id);
            ViewBag.Customer = customer;
            if (customer == null)
            {
                return Content("资料不存在！");
            }
            return View();
        }
        public void UpdateSave(Book.Model.T_Base_Customer customer)
        {
            Book.DAL.DalT_Base_Customer dal = new DAL.DalT_Base_Customer();
            bool result = dal.Update(customer);
            if (result)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"插入成功\",\"navTabId\":\"CustomerList\",\"rel\":\"CustomerList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"插入失败\",\"navTabId\":\"CustomerList\",\"rel\":\"CustomerList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
        }

        public ActionResult GetAllCustomer()
        {
            Book.DAL.DalT_Base_Customer dal = new DAL.DalT_Base_Customer();
            List<Book.Model.T_Base_Customer> lst = dal.GetModelList("1=1");

            string result = "[";
            foreach (var item in lst)
            {
                result += " {";
                result += "  \"Id\": \"" + item.Id + "\",";
                result += "  \"Name\": \"" + item.Name + "\"";

                result += " },";
            }
            if (lst.Count >= 1)
            {
                result = result.Substring(0, result.Length - 1);
            }
            result += "]";
            return Content(result);
        }
    }
}