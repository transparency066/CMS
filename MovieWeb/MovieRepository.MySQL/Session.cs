using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    /// <summary>
    /// 数据访问类:场次
    /// </summary>
    public class Session
    {
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static int Exists(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from 场次");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);

            object obj = DbHelperMySQL.ExecuteScalar(CommandType.Text, strSql.ToString(), null);
            int result=0;
            var success = int.TryParse(obj.ToString(), out result);
           
            return success ? result : 0;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(MovieModel.Session model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into 场次(");
            strSql.Append("场次ID,影片ID,开映时间,放映厅ID)");
            strSql.Append(" values (");
            strSql.Append("@场次ID,@影片ID,@开映时间,@放映厅ID)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@场次ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@影片ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@开映时间", MySqlDbType.DateTime),
                    new MySqlParameter("@放映厅ID", MySqlDbType.VarChar,5)};
            parameters[0].Value = model.场次ID;
            parameters[1].Value = model.影片ID;
            parameters[2].Value = model.开映时间;
            parameters[3].Value = model.放映厅ID;

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(MovieModel.Session model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update 场次 set ");
            strSql.Append("影片ID=@影片ID,");
            strSql.Append("开映时间=@开映时间,");
            strSql.Append("放映厅ID=@放映厅ID");
            strSql.Append(" where 场次ID=@场次ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@影片ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@开映时间", MySqlDbType.DateTime),
                    new MySqlParameter("@放映厅ID", MySqlDbType.VarChar,5),
                    new MySqlParameter("@场次ID", MySqlDbType.VarChar,10)};
            parameters[0].Value = model.影片ID;
            parameters[1].Value = model.开映时间;
            parameters[2].Value = model.放映厅ID;
            parameters[3].Value = model.场次ID;

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from 场次 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static MovieModel.Session GetModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 场次ID,影片ID,开映时间,放映厅ID from 场次 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);
            MovieModel.Session model = new MovieModel.Session();
            if (row != null)
            {
                while (row.Read())
                {
                    if (row["场次ID"] != null)
                    {
                        model.场次ID = row["场次ID"].ToString();
                    }
                    if (row["影片ID"] != null)
                    {
                        model.影片ID = row["影片ID"].ToString();
                    }
                    if (row["开映时间"] != null && row["开映时间"].ToString() != "")
                    {
                        model.开映时间 = DateTime.Parse(row["开映时间"].ToString());
                    }
                    if (row["放映厅ID"] != null)
                    {
                        model.放映厅ID = row["放映厅ID"].ToString();
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
        public static List<MovieModel.Session> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 场次ID,影片ID,开映时间,放映厅ID ");
            strSql.Append(" FROM 场次 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);

            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);
            List<MovieModel.Session> list = new List<MovieModel.Session>();
            if (row != null)
            {
                while (row.Read())
                {
                    MovieModel.Session model = new MovieModel.Session();
                    if (row["场次ID"] != null)
                    {
                        model.场次ID = row["场次ID"].ToString();
                    }
                    if (row["影片ID"] != null)
                    {
                        model.影片ID = row["影片ID"].ToString();
                    }
                    if (row["开映时间"] != null && row["开映时间"].ToString() != "")
                    {
                        model.开映时间 = DateTime.Parse(row["开映时间"].ToString());
                    }
                    if (row["放映厅ID"] != null)
                    {
                        model.放映厅ID = row["放映厅ID"].ToString();
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
        public static List<MovieModel.Session> GetList(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.场次ID desc");
            }
            strSql.Append(")AS Row, T.*  from 场次 T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);

            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);
            List<MovieModel.Session> list = new List<MovieModel.Session>();
            if (row != null)
            {
                while (row.Read())
                {
                    MovieModel.Session model = new MovieModel.Session();
                    if (row["场次ID"] != null)
                    {
                        model.场次ID = row["场次ID"].ToString();
                    }
                    if (row["影片ID"] != null)
                    {
                        model.影片ID = row["影片ID"].ToString();
                    }
                    if (row["开映时间"] != null && row["开映时间"].ToString() != "")
                    {
                        model.开映时间 = DateTime.Parse(row["开映时间"].ToString());
                    }
                    if (row["放映厅ID"] != null)
                    {
                        model.放映厅ID = row["放映厅ID"].ToString();
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

