using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Maticsoft.DBUtility;//Please add references

namespace Book.DAL
{
    partial class DalT_Stock_Report
    {
        public List<Book.Model.T_Stock_Report> GetModelList(string strWhere)
        {
            DataSet ds = GetList(strWhere);
            List<Book.Model.T_Stock_Report> list = new List<Model.T_Stock_Report>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Model.T_Stock_Report report = DataRowToModel(dr);
                report.book = new DalT_Base_Book().GetModelById((int)report.BookId);//存储report下book的数据（外键相关）
                list.Add(report);
            }
            return list;
        }
        public List<Book.Model.T_Stock_Report> GetModelListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = GetListByPage(strWhere, orderby, startIndex, endIndex);
            List<Book.Model.T_Stock_Report> list = new List<Model.T_Stock_Report>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }

        public DataSet GetListByPageByView(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from V_Report_Book T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<Book.Model.T_Stock_Report> GetModelListByPageByView(string strWhere, string orderby, int startIndex, int endIndex)

        {
            DataSet ds = GetListByPageByView(strWhere, orderby, startIndex, endIndex);
            List<Book.Model.T_Stock_Report> list = new List<Model.T_Stock_Report>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Book.Model.T_Stock_Report report = DataRowToModel(dr);
                //  book.DAL.T_Base_Provider dalProvider = new T_Base_Provider();
                // head.Provider = dalProvider.GetModel((int)head.ProviderId);

                Book.Model.T_Base_Book book = new Model.T_Base_Book();
                book.Author = Convert.ToString(dr["Author"]);
                book.BookName = Convert.ToString(dr["BookName"]);
                book.Price = Convert.ToDecimal(dr["Price"]);
                book.Version = Convert.ToString(dr["Version"]);

                report.book = book;

                list.Add(report);
            }
            return list;
        }
    }
}
