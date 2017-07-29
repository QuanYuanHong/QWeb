using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.Web.Controllers
{
    public class StockOutController : Controller
    {
        // GET: StockOut
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

            Book.DAL.DalT_Stock_OutHead dal = new DAL.DalT_Stock_OutHead();
            //获取数据
            int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            int RecordCount = dal.GetRecordCount(where);
            int startIndex = (CurrentPageIndex - 1) * PageSize + 1;
            int endIndex = CurrentPageIndex * PageSize;

            List<Book.Model.T_Stock_OutHead> lst = dal.GetModelListByPage(where, " id desc", startIndex, endIndex);

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
            Book.Model.T_Stock_Out Out = new Model.T_Stock_Out();
            Out.Head = new Book.Model.T_Stock_OutHead();
            Out.Head.Creater = Request.Form["StockCreater"];
            Out.Head.CreateTime = Convert.ToDateTime(Request.Form["PYear"]);
            Out.Head.CustomerId = Convert.ToInt32(Request.Form["Customer.Id"]);

            Out.Items = new List<Model.T_Stock_OutItems>();
            int i = 0;
            while (Request.Form["items[" + i + "].book.Name"] != null)
            {
                //比较库存
                int id = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].Replace(",", ""));
                int amount = Convert.ToInt32(Request.Form["items[" + i + "].book.Amount"]);
                Book.DAL.DalT_Stock_Report dal = new DAL.DalT_Stock_Report();
                Book.Model.T_Stock_Report report = dal.GetModel(id);
                int a = (Int32)report.Amount;
                int all = a - amount;
                if (all < 0)
                {
                    string tmp = "{\"statusCode\":\"300\",\"message\":\"库存不足\",\"navTabId\":\"OutList\",\"rel\":\"OutList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                    i++;//用于保存下一本书
                    return Content(tmp);
                    
                }
                else
                {
                    Book.Model.T_Stock_OutItems item = new Model.T_Stock_OutItems();
                    item.BookId = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].Replace(",", ""));
                    item.Amount = Convert.ToInt32(Request.Form["items[" + i + "].book.Amount"]);
                    item.Discount = Convert.ToDecimal(Request.Form["items[" + i + "].Discount"]);

                    Out.Items.Add(item);
                    i++;
                }                

                //Book.Model.T_Stock_OutItems item = new Model.T_Stock_OutItems();
                //item.BookId = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].Replace(",", ""));
                //item.Amount = Convert.ToInt32(Request.Form["items[" + i + "].book.Amount"]);
                //item.Discount = Convert.ToDecimal(Request.Form["items[" + i + "].Discount"]);

                //Out.Items.Add(item);
                //i++;
            }

            Book.BLL.Out bllIn = new BLL.Out();
            bool result = bllIn.Add(Out);

            if (result)
            {
                string tmp = "{\"statusCode\":\"200\",\"message\":\"插入成功\",\"navTabId\":\"OutList\",\"rel\":\"OutList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }
            else
            {
                string tmp = "{\"statusCode\":\"300\",\"message\":\"插入失败\",\"navTabId\":\"OutList\",\"rel\":\"OutList\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                Response.Write(tmp);
            }

            return null;
        }

        public ActionResult Edit(int headid)
        {
            ViewBag.headid = headid;

            //dal,bll读出head
            DAL.DalT_Stock_OutHead dalHead = new DAL.DalT_Stock_OutHead();
            Book.Model.T_Stock_OutHead head = dalHead.GetModelOfChild(headid);

            //dal,bll读取headitems

            DAL.DalT_Stock_OutItems dalItem = new DAL.DalT_Stock_OutItems();
            List<Book.Model.T_Stock_OutItems> items = dalItem.GetListOfChild(" headid=" + headid);

            ViewBag.head = head;
            ViewBag.items = items;
            return View();
        }
        public ActionResult EditSave()
        {
            Book.Model.T_Stock_Out Out = new Model.T_Stock_Out();
            Out.Head = new Book.Model.T_Stock_OutHead();
            Out.Head.Creater = Request.Form["CreateUser"];
            Out.Head.CreateTime = Convert.ToDateTime(Request.Form["PYear"]);
            Out.Head.CustomerId = Convert.ToInt32(Request.Form["Customer.Id"]);

            Out.Head.Id = Convert.ToInt32(Request.Form["HeadId"]); //用于编辑

            Out.Items = new List<Model.T_Stock_OutItems>();

            int i = 0;
            while (Request.Form["items[" + i + "].book.Name"] != null)
            {
                Book.Model.T_Stock_OutItems item = new Model.T_Stock_OutItems();
                item.BookId = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].Replace(",", ""));

                //同一个数据有多个以逗号隔开的值时，读取第一个值（例如：3，2，4 就读取3）
                //方法一
                //item.BookId = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].Split(',')[0]);
                //方法二
                //int seat = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].IndexOf(','));
                //item.BookId = Convert.ToInt32(Request.Form["items[" + i + "].book.Id"].Substring(0, seat));

                item.Amount = Convert.ToInt32(Request.Form["items[" + i + "].book.Amount"]);
                item.Discount = Convert.ToDecimal(Request.Form["items[" + i + "].Discount"]);
                item.HeadId = Convert.ToInt32(Request.Form["Customer.Id"]);//用于编辑

                Out.Items.Add(item);
                i++;
            }

            Book.BLL.Out bllIn = new BLL.Out();
            bool result = bllIn.Edit(Out);

            if (result)
            {
                string tmp = "{\"statusCode\":\"200\", \"message\":\"操作成功\", \"navTabId\":\"OutList\", \"rel\":\"OutList\",  \"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                return Content(tmp);

            }
            else
            {
                string tmp = "{\"statusCode\":\"300\", \"message\":\"操作不成功\", \"navTabId\":\"OutList\", \"rel\":\"OutList\",  \"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
                return Content(tmp);
            }

        }

        public ActionResult Delete(string ids)
        {
            BLL.Out Out = new BLL.Out();
            bool result = Out.DEL(ids);
            String tmp = "";
            if (result)
                tmp = "{\"statusCode\":\"200\", \"message\":\"操作成功\", \"navTabId\":\"OutList\", \"rel\":\"OutList\",  \"callbackType\":\"\", \"forwardUrl\":\"\"}";
            else
                tmp = "{\"statusCode\":\"300\", \"message\":\"插入失败\", \"navTabId\":\"OutList\", \"rel\":\"OutList\",  \"callbackType\":\"\", \"forwardUrl\":\"\"}";
            return Content(tmp);
        }
    }
}