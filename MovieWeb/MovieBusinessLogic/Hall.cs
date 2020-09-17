using System;
using System.Data;
using System.Collections.Generic;

namespace MovieBusinessLogic
{
    /// <summary>
    /// 放映厅
    /// </summary>
    public class Hall
    {

        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static int Exists(string strWhere)
        {
            return MovieRepository.MySQL.Hall.Exists(strWhere);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(MovieModel.Hall model)
        {
            return MovieRepository.MySQL.Hall.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(MovieModel.Hall model)
        {
            return MovieRepository.MySQL.Hall.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(string strWhere)
        {
            return MovieRepository.MySQL.Hall.Delete(strWhere);
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static MovieModel.Hall GetModel(string strWhere)
        {
            return MovieRepository.MySQL.Hall.GetModel(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<MovieModel.Hall> GetList(string strWhere)
        {
            return MovieRepository.MySQL.Hall.GetList(strWhere);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static List<MovieModel.Hall> GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return MovieRepository.MySQL.Hall.GetList(strWhere, orderby, startIndex, endIndex);
        }
        #endregion  BasicMethod

        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

