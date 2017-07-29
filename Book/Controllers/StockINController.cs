using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Web.Controllers
{
    public class StockINController : Controller
    {
        // GET: StockIN
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
                where = " creater like '%" + keyword + "%'";
            }

            Book.DAL.DalT_Stock_InHead dal = new DAL.DalT_Stock_InHead();
            //获取数据
            int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            int RecordCount = dal.GetRecordCount(where);
            int startIndex = (CurrentPageIndex - 1) * PageSize + 1;
            int endIndex = CurrentPageIndex * PageSize;

            List<Book.Model.T_Stock_InHead> lst = dal.GetModelListByPage(where, " id desc", startIndex, endIndex);

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
        public ActionResult AddSave()
        {
            Book.Model.T_Stock_In In = new Model.T_Stock_In();
            In.Head = new Book.Model.T_Stock_InHead();
            In.Head.Creater = Request.Form["StockCreater"];
            In.Head.CreateTime = Convert.ToDateTime(Request.Form["PYear"]);
            In.Head.ProviderId = Convert.ToInt32(Request.Form["Provider.Id"]);

            In.Items = new List<Model.T_Stock_InItems>();
            int i = 0;
            while (Request.Form["items[" + i + "].book.Name"] != null)
            {
                Book.Model.T_Stock_InItems item = new Model.T_Stock_InItems();
                item.BookId = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].Replace(",", ""));
                item.Amount = Convert.ToInt32(Request.Form["items[" + i + "].Amount"]);
                item.Discount = Convert.ToDecimal(Request.Form["items[" + i + "].Discount"]);

                In.Items.Add(item);
                i++;
            }

            Book.BLL.In bllIn = new BLL.In();
            bool result = bllIn.Add(In);

            if (result)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"插入成功\",\"navTabId\":\"StockList\",\"rel\":\"StockList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"插入失败\",\"navTabId\":\"StockList\",\"rel\":\"StockList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }

            return null;
        }

        public ActionResult Edit(int headid)
        {


            //dal,bll读出head
            DAL.DalT_Stock_InHead dalHead = new DAL.DalT_Stock_InHead();
            Book.Model.T_Stock_InHead head = dalHead.GetModelOfChild(headid);

            //dal,bll读取headitems

            DAL.DalT_Stock_InItems dalItem = new DAL.DalT_Stock_InItems();
            List<Book.Model.T_Stock_InItems> items = dalItem.GetListOfChild(" headid=" + headid);
            ViewBag.headid = headid;
            ViewBag.head = head;
            ViewBag.items = items;
            return View();
        }
        public ActionResult EditSave()
        {
            Book.Model.T_Stock_In In = new Model.T_Stock_In();
            In.Head = new Book.Model.T_Stock_InHead();
            In.Head.Creater = Request.Form["CreateUser"];
            In.Head.CreateTime = Convert.ToDateTime(Request.Form["PYear"]);
            In.Head.ProviderId = Convert.ToInt32(Request.Form["Provider.Id"]);

            In.Head.Id = Convert.ToInt32(Request.Form["HeadId"]); //用于编辑

            In.Items = new List<Model.T_Stock_InItems>();

            int i = 0;
            while (Request.Form["items[" + i + "].book.Name"] != null)
            {
                Book.Model.T_Stock_InItems item = new Model.T_Stock_InItems();
                item.BookId = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].Replace(",", ""));

                //同一个数据有多个以逗号隔开的值时，读取第一个值（例如：3，2，4 就读取3）
                //方法一
                //item.BookId = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].Split(',')[0]);
                //方法二
                //int seat = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].IndexOf(','));
                //item.BookId = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].Substring(0, seat));

                item.Amount = Convert.ToInt32(Request.Form["items[" + i + "].Amount"]);
                item.Discount = Convert.ToDecimal(Request.Form["items[" + i + "].Discount"]);
                item.HeadId = Convert.ToInt32(Request.Form["Provider.Id"]);//用于编辑

                In.Items.Add(item);
                i++;
            }

            Book.BLL.In bllIn = new BLL.In();
            bool result = bllIn.Edit(In);

            if (result)
            {
                string tmp = "{\"statusCode\":\"200\", \"message\":\"操作成功\", \"navTabId\":\"StockList\", \"rel\":\"StockList\",  \"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                return Content(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\", \"message\":\"操作不成功\", \"navTabId\":\"StockList\", \"rel\":\"StockList\",  \"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                return Content(tmp);
            }
        }

        public ActionResult Delete(string ids)
        {
            BLL.In IN = new BLL.In();
            bool result = IN.DEL(ids);
            String tmp = "";
            if (result)
                tmp = "{\"statusCode\":\"200\", \"message\":\"操作成功\", \"navTabId\":\"StockList\", \"rel\":\"StockList\",  \"callbackType\":\"\", \"forwardUrl\":\"\"}";
            else
                tmp = "{\"statusCode\":\"300\", \"message\":\"插入失败\", \"navTabId\":\"StockList\", \"rel\":\"StockList\",  \"callbackType\":\"\", \"forwardUrl\":\"\"}";
            return Content(tmp);
        }
    }
}

