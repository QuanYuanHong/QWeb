
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Model
{
    public class T_Stock_In
    {
        public Book.Model.T_Stock_InHead Head
        {
            get;
            set;
        }
        public List<Book.Model.T_Stock_InItems> Items
        {
            get;
            set;
        }
    }
}
