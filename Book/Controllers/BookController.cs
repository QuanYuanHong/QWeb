using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Book.Web.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index(string keyword,int pageNum = 1)
        {
            //Book.Web.Models.BookEntities db = new Models.BookEntities();
            //List<Book.Web.Models.T_Base_Book> lst = db.T_Base_Book.ToList();
            //ViewBag.lst = lst;
            int CurrentPageIndex = pageNum;
            string where = "";
            if(keyword == "")
            {
                where = "1 = 1";
            }else
            {
                where = " bookname like '%" + keyword + "%'";
            }

            Book.DAL.DalT_Base_Book dal = new DAL.DalT_Base_Book();
            //获取数据
            int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            int RecordCount=dal.GetRecord(where);
            
            List<Book.Model.T_Base_Book> lst = dal.GetListByPage(CurrentPageIndex,PageSize,where);

            //展示数据
            ViewBag.CurrentPageIndex = CurrentPageIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.RecordCount = RecordCount;
            ViewBag.lst = lst;

            return View();
        }
        public ActionResult Index1()
        {

            Book.DAL.DalT_Base_Book dal = new DAL.DalT_Base_Book();
            List<Book.Model.T_Base_Book> lst = dal.GetList();

            ViewBag.lst = lst;
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
        public void AddSave(Book.Model.T_Base_Book book)
        {
            Book.DAL.DalT_Base_Book dal = new Book.DAL.DalT_Base_Book();
            bool res = dal.Add(book);

            if (res)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"插入成功\",\"navTabId\":\"BookList\",\"rel\":\"BookList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"插入失败\",\"navTabId\":\"BookList\",\"rel\":\"BookList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }

        }

        public ActionResult Delete(string ids)
        {
            DAL.DalT_Base_Book dal = new DAL.DalT_Base_Book();
            bool result = dal.Delete(ids);
            if(result)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"删除成功\",\"navTabId\":\"BookList\",\"rel\":\"BookList\",\"callbackType\":\"\",\"forwardUrl\":\"\"}";
                return Content(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"删除失败\",\"navTabId\":\"BookList\",\"rel\":\"BookList\",\"callbackType\":\"\",\"forwardUrl\":\"\"}";
                return Content(tmp);
            }
        }

        public ActionResult Update(int id)
        {
            Book.DAL.DalT_Base_Book dal = new DAL.DalT_Base_Book();
            Book.Model.T_Base_Book book = dal.GetModelById(id);
            ViewBag.Book = book;
            if(book == null)
            {
                return Content("资料不存在！");
            }
            return View();
        }
        public void  UpdateSave(Book.Model.T_Base_Book book)
        {
            Book.DAL.DalT_Base_Book dal = new DAL.DalT_Base_Book();
            bool result = dal.Update(book);
            if (result)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"修改成功\",\"navTabId\":\"BookList\",\"rel\":\"BookList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"修改失败\",\"navTabId\":\"BookList\",\"rel\":\"BookList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
        }

        public FileResult ExportExcel()
        {
            DAL.DalT_Base_Book dal = new DAL.DalT_Base_Book();
            List<Model.T_Base_Book> lst = dal.GetList();
            var sbHtml = new StringBuilder();
            sbHtml.Append("<table border='1' cellspacing='0' cellpadding='0'>");
            sbHtml.Append("<tr>");
            var lstTitle = new List<string> { "书名", "作者", "编码", "图片", "出版社", "价格", "出版时间", "版本号" };
            foreach (var item in lstTitle)
            {
                sbHtml.AppendFormat("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>{0}</td>", item);
            }
            sbHtml.Append("</tr>");
            foreach (var item in lst)
            {
                sbHtml.Append("<tr>");
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", item.BookName);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", item.Author);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", item.Code);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", item.Pic);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", item.PressName);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", item.Price);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", item.Pyear);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", item.Version);
                sbHtml.Append("</tr>");
            }
            sbHtml.Append("</table>");

            byte[] fileContents = Encoding.UTF8.GetBytes(sbHtml.ToString());
            return File(fileContents, "doc/ms-excel", "书籍表.xls");

        }


        public ActionResult GetAll()
        {
            Book.DAL.DalT_Base_Book dal = new DAL.DalT_Base_Book();
            List<Book.Model.T_Base_Book> lst = dal.GetList();

            string result = "[";
            foreach (var item in lst)
            {
                result += " {";
                result += "  \"Name\": \"" + item.BookName + "\",";
                result += "  \"Id\": \"" + item.Id + "\",";
                result += "  \"Price\": \"" + item.Price + "\",";
                result += "  \"Version\": \"" + item.Version + "\",";
                result += "  \"Author\": \"" + item.Author + "\"";

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