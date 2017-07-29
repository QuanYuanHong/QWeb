using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DAL
{
    public partial class DalT_Stock_InItems
    {
        public List<Book.Model.T_Stock_InItems> GetListOfChild(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM V_InItems_Book ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            List<Book.Model.T_Stock_InItems> lst = new List<Model.T_Stock_InItems>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Book.Model.T_Stock_InItems item = DataRowToModel(dr);
                Book.Model.T_Base_Book book = new Model.T_Base_Book();

                book.Author = Convert.ToString(dr["Author"]);
                book.Code = Convert.ToString(dr["Code"]);
                book.Pic = Convert.ToString(dr["Pic"]);
                book.Version = Convert.ToString(dr["Version"]);
                book.Pyear = Convert.ToDateTime(dr["Pyear"]);
                book.Id = Convert.ToInt32(dr["BookId"]);
                book.BookName = Convert.ToString(dr["BookName"]);
                book.Price = Convert.ToDecimal(dr["Price"]);

                item.Book = book;

                lst.Add(item);

            }


            return lst;
        }

        public bool DeleteWhere(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Stock_InItems ");
            strSql.Append(" where " + where);

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
