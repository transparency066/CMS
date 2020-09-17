using System;
using System.Data;
using System.Collections.Generic;

namespace MovieBusinessLogic
{
    /// <summary>
    /// 座位
    /// </summary>
    public class Seats
    {

        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static int Exists(string strWhere)
        {
            return MovieRepository.MySQL.Seats.Exists(strWhere);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(MovieModel.Seats model)
        {
            return MovieRepository.MySQL.Seats.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(MovieModel.Seats model)
        {
            return MovieRepository.MySQL.Seats.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(string strWhere)
        {
            return MovieRepository.MySQL.Seats.Delete(strWhere);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static MovieModel.Seats GetModel(string strWhere)
        {
            return MovieRepository.MySQL.Seats.GetModel(strWhere);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<MovieModel.Seats> GetList(string strWhere)
        {
            return MovieRepository.MySQL.Seats.GetList(strWhere);
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static List<MovieModel.Seats> GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return MovieRepository.MySQL.Seats.GetList(strWhere, orderby, startIndex, endIndex);
        }

        #endregion  BasicMethod

        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

