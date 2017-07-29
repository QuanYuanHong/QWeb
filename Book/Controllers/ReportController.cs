using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Web.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index(string keyword, int pageNum = 1)
        {
            //Book.web.Models.Entities db = new Models.Entities();
            //List<Book.web.Models.T_Base_Provider> lst = db.T_Base_Provider.ToList();
            //ViewBag.lst = lst;
            //return View();
            int CurrentPageIndex = pageNum;
            string where = " 1=1";
            //if (keyword == "")
            //{
            //    where = "1 = 1";

            //}
            //else
            //{
            //    where = " name like '%" + keyword + "%'";
            //}
            Book.DAL.DalT_Stock_Report dal = new DAL.DalT_Stock_Report();
            //获取数据

            int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            int RecordCount = dal.GetRecordCount(where);
            int startIndex = (CurrentPageIndex - 1) * PageSize + 1;
            int endIndex = CurrentPageIndex * PageSize;
            List<Book.Model.T_Stock_Report> lst = dal.GetModelListByPageByView(where, " id desc", startIndex, endIndex);
            //展示数据
            ViewBag.CurrentPageIndex = CurrentPageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.RecordCount = RecordCount;
            ViewBag.lst = lst;
            return View();
        }

        public ActionResult GetAllBookReport()
        {
            Book.DAL.DalT_Stock_Report dal = new DAL.DalT_Stock_Report();
            List<Book.Model.T_Stock_Report> lst = dal.GetModelList("1=1");

            string result = "[";
            foreach (var item in lst)
            {
                result += " {";
                result += "  \"Name\": \"" + item.book.BookName + "\",";
                result += "  \"Id\": \"" + item.BookId + "\",";
                result += "  \"Price\": \"" + item.book.Price + "\",";
                result += "  \"Version\": \"" + item.book.Version + "\",";
                result += "  \"Author\": \"" + item.book.Author + "\",";
                result += "  \"Amount\": \"" + item.Amount + "\"";

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