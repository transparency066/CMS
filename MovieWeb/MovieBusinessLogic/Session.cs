using System;
using System.Data;
using System.Collections.Generic;

namespace MovieBusinessLogic
{
    /// <summary>
    /// 场次
    /// </summary>
    public class Session
    {

        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static int Exists(string strWhere)
        {
            return MovieRepository.MySQL.Session.Exists(strWhere);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(MovieModel.Session model)
        {
            return MovieRepository.MySQL.Session.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(MovieModel.Session model)
        {
            return MovieRepository.MySQL.Session.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(string strWhere)
        {
            return MovieRepository.MySQL.Session.Delete(strWhere);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static MovieModel.Session GetModel(string strWhere)
        {
            return MovieRepository.MySQL.Session.GetModel(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<MovieModel.Session> GetList(string strWhere)
        {
            return MovieRepository.MySQL.Session.GetList(strWhere);
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static List<MovieModel.Session> GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return MovieRepository.MySQL.Session.GetList(strWhere, orderby, startIndex, endIndex);
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

