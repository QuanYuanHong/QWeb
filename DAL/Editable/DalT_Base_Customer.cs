using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Book.DAL
{
    public partial class DalT_Base_Customer
    {
        public List<Book.Model.T_Base_Customer> GetModelListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = GetListByPage(strWhere, orderby, startIndex, endIndex);
            List<Book.Model.T_Base_Customer> list = new List<Model.T_Base_Customer>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }

        public List<Book.Model.T_Base_Customer> GetModelList(string strWhere)

        {
            DataSet ds = GetList(strWhere);
            List<Book.Model.T_Base_Customer> list = new List<Model.T_Base_Customer>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }
    }
}
