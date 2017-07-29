using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.BLL
{
    public class In
    {
        public bool Add(Book.Model.T_Stock_In stockin)
        {
            DAL.DalT_Stock_InHead bllHead = new DAL.DalT_Stock_InHead();
            stockin.Head.Id = bllHead.Add(stockin.Head);

            DAL.DalT_Stock_InItems dalItems = new DAL.DalT_Stock_InItems();
            foreach(Book.Model.T_Stock_InItems item in stockin.Items)
            {
                item.HeadId = stockin.Head.Id;
                dalItems.Add(item);

                //更新库存（没有使用触发器之前的方法（2017.5.19之前））
                //var bookid = item.BookId;
                //Book.DAL.DalT_Stock_Report dalReport = new DAL.DalT_Stock_Report();
                //List<Book.Model.T_Stock_Report> lstReport = dalReport.GetModelListByPage("bookid= " + bookid, "", 0, 10);
                //if (lstReport.Count > 0)
                //{
                //    lstReport[0].Amount += item.Amount;
                //    dalReport.Update(lstReport[0]);
                //}
                //else
                //{
                //    Book.Model.T_Stock_Report newReport = new Model.T_Stock_Report();
                //    newReport.BookId = item.BookId;
                //    newReport.Amount = item.Amount;
                //    newReport.LastTime = DateTime.Now;
                //    dalReport.Add(newReport);
                //}
            }
            return true;
        }

        public bool Edit(Book.Model.T_Stock_In stockin)
        {

            //表单头的修改
            DAL.DalT_Stock_InHead dalHead = new DAL.DalT_Stock_InHead();
            bool result = dalHead.Update(stockin.Head);   //编辑

            //单体的修改
            //先删除所有的记录
            DAL.DalT_Stock_InItems dalItems = new DAL.DalT_Stock_InItems();

            ///delete from biao where headin =1

            dalItems.DeleteWhere("headid =" + stockin.Head.Id);  //删除老的数据

            foreach (Book.Model.T_Stock_InItems item in stockin.Items)
            {
                item.HeadId = stockin.Head.Id;
                dalItems.Add(item);
            }

            return true;
        }

        public bool DEL(String ids)
        {
            DAL.DalT_Stock_InItems dalItems = new DAL.DalT_Stock_InItems();

            ///delete from biao where headin =1

            dalItems.DeleteWhere("headid =" + ids);  //删除老的数据
            //表单头的删除
            DAL.DalT_Stock_InHead dalHead = new DAL.DalT_Stock_InHead();
            bool result = dalHead.DeleteList(ids);   //编辑

            //单体的修改
            //先删除所有的记录
            return true;
        }
    }
}
