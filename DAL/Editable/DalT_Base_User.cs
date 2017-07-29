using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Data;

namespace Book.DAL
{
    public partial class DalT_Base_User
    {
        public bool IsExist(string UserName, string Password)/*是否存在用户*/
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SqlConnection"];
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from t_base_user where username=@username and password=@password";
            cm.Parameters.Add("@username", UserName);
            cm.Parameters.Add("@password", Password);
            cm.Connection = co;

            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                co.Close();
                return true;
            }
            else
            {
                dr.Close();
                co.Close();
                return false;
            }
        }

        public List<Book.Model.T_Base_User> GetModelListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = GetListByPage(strWhere, orderby, startIndex, endIndex);
            List<Book.Model.T_Base_User> list = new List<Model.T_Base_User>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(DataRowToModel(dr));
            }
            return list;
        }
    }
}
