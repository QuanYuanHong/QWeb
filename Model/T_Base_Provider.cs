using System;
namespace Book.Model
{
    /// <summary>
    /// T_Base_Provider:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_Base_Provider
    {
        public T_Base_Provider()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _connector;
        private string _phone;
        private string _address;
        private string _email;
        private string _fax;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 供货商名字
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 供货商联系人名字
        /// </summary>
        public string Connector
        {
            set { _connector = value; }
            get { return _connector; }
        }
        /// <summary>
        /// 供货商电话
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 供货商地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 供货商邮件
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 供货商传真
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        #endregion Model

    }
}

