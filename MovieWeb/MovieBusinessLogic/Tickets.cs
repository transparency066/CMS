using System;
using System.Data;
using System.Collections.Generic;

namespace MovieBusinessLogic
{
    /// <summary>
    /// 影票
    /// </summary>
    public class Tickets
    {
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static int Exists(string strWhere)
        {
            return MovieRepository.MySQL.Tickets.Exists(strWhere);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(MovieModel.Tickets model)
        {
            return MovieRepository.MySQL.Tickets.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(MovieModel.Tickets model)
        {
            return MovieRepository.MySQL.Tickets.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(string strWhere)
        {
            return MovieRepository.MySQL.Tickets.Delete(strWhere);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static MovieModel.Tickets GetModel(string strWhere)
        {
            return MovieRepository.MySQL.Tickets.GetModel(strWhere);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<MovieModel.Tickets> GetList(string strWhere)
        {
            return MovieRepository.MySQL.Tickets.GetList(strWhere);
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static List<MovieModel.Tickets> GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return MovieRepository.MySQL.Tickets.GetList(strWhere, orderby, startIndex, endIndex);
        }

        #endregion  BasicMethod

        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

