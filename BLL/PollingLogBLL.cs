﻿/**  版本信息模板在安装目录下，可自行修改。
* Polling_Log.cs
*
* 功 能： N/A
* 类 名： Polling_Log
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/4/5 20:13:40   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：元大信息科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;

using YDIOTSvr.DataLayer.Entity;using YDIOTSvr.DataLayer.DAL;
namespace YDIOTSvr.DataLayer.BLL
{
	/// <summary>
	/// 墒情传感器的表。有三个探针
	/// </summary>
	public partial class PollingLogBLL
	{
        private readonly PollingLogDAL dal = new PollingLogDAL();
		public PollingLogBLL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Pl_ID)
		{
			return dal.Exists(Pl_ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(PollingLogEntity model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(PollingLogEntity model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Pl_ID)
		{
			
			return dal.Delete(Pl_ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Pl_IDlist )
		{
			return dal.DeleteList(Pl_IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public PollingLogEntity GetModel(int Pl_ID)
		{
			
			return dal.GetModel(Pl_ID);
		}

	
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<PollingLogEntity> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<PollingLogEntity> DataTableToList(DataTable dt)
		{
			List<PollingLogEntity> modelList = new List<PollingLogEntity>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				PollingLogEntity model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

