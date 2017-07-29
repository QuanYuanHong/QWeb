using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Book.DAL
{
    public class DalT_Base_Book
    {
        public List<Book.Model.T_Base_Book> GetList()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SqlConnection"];
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select * from t_base_book";
            cm.Connection = co;


            SqlDataReader dr = cm.ExecuteReader();

            List<Book.Model.T_Base_Book> lst = new List<Model.T_Base_Book>();
            while (dr.Read())
            {
                Book.Model.T_Base_Book book = new Model.T_Base_Book();
                book.Id = Convert.ToInt32(dr["Id"]);
                book.Code = Convert.ToString(dr["Code"]);
                book.Author = Convert.ToString(dr["Author"]);
                book.PressName = Convert.ToString(dr["PressName"]);
                book.Price = Convert.ToDecimal(dr["Price"]);
                book.Pyear = Convert.ToDateTime(dr["Pyear"]);
                book.Version = Convert.ToString(dr["Version"]);
                book.BookName = Convert.ToString(dr["BookName"]);
                book.Pic = Convert.ToString(dr["Pic"]);

                lst.Add(book);

            }
            dr.Close();
            co.Close();
            return lst;
        }

        public bool Add(Book.Model.T_Base_Book book)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SqlConnection"];
            co.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"insert into t_base_book(bookname,code,author,pressname,price,pyear,version,pic)
                                values(@bookname,@code,@author,@pressname,@price,@pyear,@version,@pic)";
            cmd.Connection = co;
            cmd.Parameters.AddWithValue("@bookname", book.BookName);
            cmd.Parameters.AddWithValue("@code", book.Code);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@pressname", book.PressName);
            cmd.Parameters.AddWithValue("@price", book.Price);
            cmd.Parameters.AddWithValue("@pyear", book.Pyear);
            cmd.Parameters.AddWithValue("@version", book.Version);
            cmd.Parameters.AddWithValue("@pic", book.Pic);
            cmd.Connection = co;

            int result = cmd.ExecuteNonQuery();
            co.Close();

            if (result > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool Delete(string ids)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SqlConnection"];
            co.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete from t_base_book where id in(" + ids + ")";
            cmd.Connection = co;

            int result = cmd.ExecuteNonQuery();
            co.Close();

            if (result > 0)
            {
                return true;
            }
            else
                return false;
        }

        public Book.Model.T_Base_Book GetModelById(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SqlConnection"];
            co.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from t_base_book where id=@id";
            cmd.Connection = co;
            cmd.Parameters.AddWithValue("@id", id);
            

            Book.Model.T_Base_Book book = null;
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                book = new Model.T_Base_Book();
                book.Id = Convert.ToInt32(dr["Id"]);
                book.BookName = Convert.ToString(dr["BookName"]);
                book.Code = Convert.ToString(dr["Code"]);
                book.Author = Convert.ToString(dr["Author"]);
                book.PressName = Convert.ToString(dr["PressName"]);
                book.Price = Convert.ToDecimal(dr["Price"]);
                book.Pyear = Convert.ToDateTime(dr["Pyear"]);
                book.Version = Convert.ToString(dr["Version"]);
                book.Pic = Convert.ToString(dr["Pic"]);
            }
            return book;
        }

        public bool Update(Book.Model.T_Base_Book book)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SqlConnection"];
            co.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update t_base_book set bookname=@bookname,code=@code,author=@author,pressname=@pressname,price=@price,pyear=@pyear,version=@version,pic=@pic where id=@id";
            cmd.Connection = co;
            cmd.Parameters.AddWithValue("@bookname", book.BookName);
            cmd.Parameters.AddWithValue("@code", book.Code);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@pressname", book.PressName);
            cmd.Parameters.AddWithValue("@price", book.Price);
            cmd.Parameters.AddWithValue("@pyear", book.Pyear);
            cmd.Parameters.AddWithValue("@version", book.Version);
            cmd.Parameters.AddWithValue("@pic", book.Pic);
            cmd.Parameters.AddWithValue("@id", book.Id);
            
            int result = cmd.ExecuteNonQuery();
            co.Close();

            if (result >= 0)
            {
                return true;
            }
            else
                return false;
        }

        public int GetRecord(string where)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SqlConnection"];
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select count(1) from t_base_book where " + where;
            int a = (int)cm.ExecuteScalar();
            co.Close();
            return a;
        }

        public List<Book.Model.T_Base_Book> GetListByPage(int CurrentPageIndex,int PageSize,string where)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SqlConnection"];

            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select top " + PageSize + " * from t_base_book where id not in (select top " + PageSize * (CurrentPageIndex - 1) + " id from t_base_book where " + where + ") and " + where;

            SqlDataReader dr = cm.ExecuteReader();

            List<Book.Model.T_Base_Book> lst = new List<Model.T_Base_Book>();
            while (dr.Read())
            {
                #region 参数转换
                Book.Model.T_Base_Book book = new Model.T_Base_Book();
                book.Id = Convert.ToInt32(dr["Id"]);
                book.Code = Convert.ToString(dr["Code"]);
                book.Author = Convert.ToString(dr["Author"]);
                book.PressName = Convert.ToString(dr["PressName"]);
                book.Price = Convert.ToDecimal(dr["Price"]);
                book.Pyear = Convert.ToDateTime(dr["Pyear"]);
                book.Version = Convert.ToString(dr["Version"]);
                book.BookName = Convert.ToString(dr["BookName"]);
                book.Pic = Convert.ToString(dr["Pic"]);
                #endregion

                lst.Add(book);
            }
            co.Close();
            return lst;
        }
    }
}
