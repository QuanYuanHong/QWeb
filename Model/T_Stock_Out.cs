using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Model
{
    public class T_Stock_Out
    {
        public Book.Model.T_Stock_OutHead Head
        {
            get;
            set;
        }
        public List<Book.Model.T_Stock_OutItems> Items
        {
            get;
            set;
        }
    }
}
