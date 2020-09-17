using System;
using System.Data;
using System.Collections.Generic;

namespace MovieBusinessLogic
{
    /// <summary>
    /// 影片
    /// </summary>
    public class Movie
    {

        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static int Exists(string strWhere)
        {
            return MovieRepository.MySQL.Movie.Exists(strWhere);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(MovieModel.Movie model)
        {
            return MovieRepository.MySQL.Movie.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(MovieModel.Movie model)
        {
            return MovieRepository.MySQL.Movie.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(string strWhere)
        {
            return MovieRepository.MySQL.Movie.Delete(strWhere);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static MovieModel.Movie GetModel(string strWhere)
        {
            return MovieRepository.MySQL.Movie.GetModel(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<MovieModel.Movie> GetList(string strWhere)
        {
            return MovieRepository.MySQL.Movie.GetList(strWhere);
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static List<MovieModel.Movie> GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return MovieRepository.MySQL.Movie.GetList(strWhere, orderby, startIndex, endIndex);
        }

        #endregion  BasicMethod

        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

