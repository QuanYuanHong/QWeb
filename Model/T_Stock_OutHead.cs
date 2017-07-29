/**  版本信息模板在安装目录下，可自行修改。
* T_Stock_OutHead.cs
*
* 功 能： N/A
* 类 名： T_Stock_OutHead
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/5/31 21:22:27   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Book.Model
{
	/// <summary>
	/// T_Stock_OutHead:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Stock_OutHead
	{
		public T_Stock_OutHead()
		{}
		#region Model
		private int _id;
		private DateTime _createtime;
		private int? _customerid;
		private string _creater;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CustomerId
		{
			set{ _customerid=value;}
			get{return _customerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Creater
		{
			set{ _creater=value;}
			get{return _creater;}
		}

        /// <summary>
        /// 
        /// </summary>
        public T_Base_Customer Customer
        {
            get;
            set;
        }
        #endregion Model

    }
}

