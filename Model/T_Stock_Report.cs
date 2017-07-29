/**  版本信息模板在安装目录下，可自行修改。
* T_Stock_Report.cs
*
* 功 能： N/A
* 类 名： T_Stock_Report
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/5/11 11:23:49   N/A    初版
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
	/// T_Stock_Report:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Stock_Report
	{
		public T_Stock_Report()
		{}
		#region Model
		private int _id;
		private int? _bookid;
		private int? _amount;
		private DateTime? _lasttime;
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
		public int? BookId
		{
			set{ _bookid=value;}
			get{return _bookid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastTime
		{
			set{ _lasttime=value;}
			get{return _lasttime;}
		}
		#endregion Model

	}
}

