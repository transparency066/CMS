using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    /// <summary>
    /// 数据访问类:放映厅
    /// </summary>
    public class Hall
    {
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static int Exists(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from 放映厅");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);

            object obj = DbHelperMySQL.ExecuteScalar(CommandType.Text, strSql.ToString(), null);
            int result = 0;
            var success = int.TryParse(obj.ToString(), out result);

            return success ? result : 0;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(MovieModel.Hall model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into 放映厅(");
            strSql.Append("放映厅ID,容量,放映厅位置)");
            strSql.Append(" values (");
            strSql.Append("@放映厅ID,@容量,@放映厅位置)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@放映厅ID", MySqlDbType.VarChar,5),
                    new MySqlParameter("@容量", MySqlDbType.Int32,10),
                    new MySqlParameter("@放映厅位置", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.放映厅ID;
            parameters[1].Value = model.容量;
            parameters[2].Value = model.放映厅位置;

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(MovieModel.Hall model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update 放映厅 set ");
            strSql.Append("容量=@容量,");
            strSql.Append("放映厅位置=@放映厅位置");
            strSql.Append(" where 放映厅ID=@放映厅ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@容量", MySqlDbType.Int32,10),
                    new MySqlParameter("@放映厅位置", MySqlDbType.VarChar,50),
                    new MySqlParameter("@放映厅ID", MySqlDbType.VarChar,5)};
            parameters[0].Value = model.容量;
            parameters[1].Value = model.放映厅位置;
            parameters[2].Value = model.放映厅ID;

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from 放映厅 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static MovieModel.Hall GetModel(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 放映厅ID,容量,放映厅位置 from 放映厅 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);

            MovieModel.Hall model = new MovieModel.Hall();
            if (row != null)
            {
                while (row.Read())
                {
                    if (row["放映厅ID"] != null)
                    {
                        model.放映厅ID = row["放映厅ID"].ToString();
                    }
                    if (row["容量"] != null && row["容量"].ToString() != "")
                    {
                        model.容量 = int.Parse(row["容量"].ToString());
                    }
                    if (row["放映厅位置"] != null)
                    {
                        model.放映厅位置 = row["放映厅位置"].ToString();
                    }
                }
                row.Close();
                row.Dispose();
            }
            return model;

        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<MovieModel.Hall> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 放映厅ID,容量,放映厅位置 ");
            strSql.Append(" FROM 放映厅 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);

            List<MovieModel.Hall> list = new List<MovieModel.Hall>();
            if (row != null)
            {
                while (row.Read())
                {
                    MovieModel.Hall model = new MovieModel.Hall();
                    if (row["放映厅ID"] != null)
                    {
                        model.放映厅ID = row["放映厅ID"].ToString();
                    }
                    if (row["容量"] != null && row["容量"].ToString() != "")
                    {
                        model.容量 = int.Parse(row["容量"].ToString());
                    }
                    if (row["放映厅位置"] != null)
                    {
                        model.放映厅位置 = row["放映厅位置"].ToString();
                    }
                    list.Add(model);
                }
                row.Close();
                row.Dispose();
            }

            return list;
        }
         
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static List<MovieModel.Hall> GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.放映厅ID desc");
            }
            strSql.Append(")AS Row, T.*  from 放映厅 T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);

            List<MovieModel.Hall> list = new List<MovieModel.Hall>();
            if (row != null)
            {
                while (row.Read())
                {
                    MovieModel.Hall model = new MovieModel.Hall();
                    if (row["放映厅ID"] != null)
                    {
                        model.放映厅ID = row["放映厅ID"].ToString();
                    }
                    if (row["容量"] != null && row["容量"].ToString() != "")
                    {
                        model.容量 = int.Parse(row["容量"].ToString());
                    }
                    if (row["放映厅位置"] != null)
                    {
                        model.放映厅位置 = row["放映厅位置"].ToString();
                    }
                    list.Add(model);
                }
                row.Close();
                row.Dispose();
            }

            return list;
        }
        #endregion  BasicMethod
    }
}

