using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Maticsoft.DBUtility;

namespace Book.DAL
{
    public partial class DalT_Stock_OutHead
    {
        public Book.Model.T_Stock_OutHead GetModelOfChild(int Id)
        {
            Book.Model.T_Stock_OutHead head = GetModel(Id);
            head.Customer = new DalT_Base_Customer().GetModel((int)head.CustomerId);
            return head;
        }

        public List<Book.Model.T_Stock_OutHead> GetModelListByPage(string strWhere, string orderby, int startIndex, int endIndex)

        {
            DataSet ds = GetListByPageByView(strWhere, orderby, startIndex, endIndex);
            List<Book.Model.T_Stock_OutHead> list = new List<Model.T_Stock_OutHead>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Book.Model.T_Stock_OutHead head = DataRowToModel(dr);

                Book.Model.T_Base_Customer customer = new Model.T_Base_Customer();
                customer.Id = Convert.ToInt32(dr["Id"]);
                customer.Address = Convert.ToString(dr["Address"]);
                customer.Name = Convert.ToString(dr["Name"]);
                customer.Phone = Convert.ToString(dr["Phone"]);

                head.Customer = customer;
                list.Add(head);
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
            strSql.Append(")AS Row, T.*  from V_Head_Customer T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<Book.Model.T_Stock_OutHead> GetModelListByPageByView(string strWhere, string orderby, int startIndex, int endIndex)

        {
            DataSet ds = GetListByPageByView(strWhere, orderby, startIndex, endIndex);
            List<Book.Model.T_Stock_OutHead> list = new List<Model.T_Stock_OutHead>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Book.Model.T_Stock_OutHead head = DataRowToModel(dr);
                //  book.DAL.T_Base_Customer dalCustomer = new T_Base_Customer();
                // head.Customer = dalCustomer.GetModel((int)head.CustomerId);

                Book.Model.T_Base_Customer customer = new Model.T_Base_Customer();
                customer.Id = Convert.ToInt32(dr["Id"]);
                customer.Address = Convert.ToString(dr["Address"]);
                customer.Name = Convert.ToString(dr["Name"]);
                customer.Phone = Convert.ToString(dr["Phone"]);

                head.Customer = customer;
                list.Add(head);
            }
            return list;
        }
    }
}
