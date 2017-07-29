using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Maticsoft.DBUtility;

namespace Book.DAL
{
    public partial class DalT_Stock_InHead
    {
        public Book.Model.T_Stock_InHead GetModelOfChild(int Id)
        {
            Book.Model.T_Stock_InHead head = GetModel(Id);
            head.Provider = new DalT_Base_Provider().GetModel((int)head.ProviderId);
            return head;
        }

        public List<Book.Model.T_Stock_InHead> GetModelListByPage(string strWhere, string orderby, int startIndex, int endIndex)

        {
            DataSet ds = GetListByPageByView(strWhere, orderby, startIndex, endIndex);
            List<Book.Model.T_Stock_InHead> list = new List<Model.T_Stock_InHead>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Book.Model.T_Stock_InHead head = DataRowToModel(dr);

                Book.Model.T_Base_Provider provider = new Model.T_Base_Provider();
                provider.Id = Convert.ToInt32(dr["Id"]);
                provider.Address = Convert.ToString(dr["Address"]);
                provider.Connector = Convert.ToString(dr["Connector"]);
                provider.Email = Convert.ToString(dr["Email"]);
                provider.Name = Convert.ToString(dr["Name"]);
                provider.Phone = Convert.ToString(dr["Phone"]);
                provider.Fax = Convert.ToString(dr["Fax"]);

                head.Provider = provider;
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
            strSql.Append(")AS Row, T.*  from V_Head_Provider T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<Book.Model.T_Stock_InHead> GetModelListByPageByView(string strWhere, string orderby, int startIndex, int endIndex)

        {
            DataSet ds = GetListByPageByView(strWhere, orderby, startIndex, endIndex);
            List<Book.Model.T_Stock_InHead> list = new List<Model.T_Stock_InHead>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Book.Model.T_Stock_InHead head = DataRowToModel(dr);
                //  book.DAL.T_Base_Provider dalProvider = new T_Base_Provider();
                // head.Provider = dalProvider.GetModel((int)head.ProviderId);

                Book.Model.T_Base_Provider provider = new Model.T_Base_Provider();
                provider.Address = Convert.ToString(dr["Address"]);
                provider.Email = Convert.ToString(dr["Email"]);
                provider.Connector = Convert.ToString(dr["Connector"]);
                provider.Id = Convert.ToInt32(dr["Id"]);
                provider.Name = Convert.ToString(dr["Name"]);
                provider.Phone = Convert.ToString(dr["Phone"]);

                head.Provider = provider;

                list.Add(head);
            }
            return list;
        }
    }
}
