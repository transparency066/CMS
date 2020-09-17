using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    /// <summary>
    /// 数据访问类:影票
    /// </summary>
    public class Tickets
    { 
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static int Exists(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from 影票");
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
        public static int Add(MovieModel.Tickets model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into 影票(");
            strSql.Append("影票ID,拥有者账号ID,场次ID,座位ID,购票时间,影票状态)");
            strSql.Append(" values (");
            strSql.Append("@影票ID,@拥有者账号ID,@场次ID,@座位ID,@购票时间,@影票状态)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@影票ID", MySqlDbType.VarChar,15),
                    new MySqlParameter("@拥有者账号ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@场次ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@座位ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@购票时间", MySqlDbType.DateTime),
                    new MySqlParameter("@影票状态", MySqlDbType.Int32,11)};
            parameters[0].Value = model.影票ID;
            parameters[1].Value = model.拥有者账号ID;
            parameters[2].Value = model.场次ID;
            parameters[3].Value = model.座位ID;
            parameters[4].Value = model.购票时间;
            parameters[5].Value = model.影票状态;

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(MovieModel.Tickets model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update 影票 set ");
            strSql.Append("拥有者账号ID=@拥有者账号ID,");
            strSql.Append("场次ID=@场次ID,");
            strSql.Append("座位ID=@座位ID,");
            strSql.Append("购票时间=@购票时间,");
            strSql.Append("影票状态=@影票状态");
            strSql.Append(" where 影票ID=@影票ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@拥有者账号ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@场次ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@座位ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@购票时间", MySqlDbType.DateTime),
                    new MySqlParameter("@影票状态", MySqlDbType.Int32,11),
                    new MySqlParameter("@影票ID", MySqlDbType.VarChar,15)};
            parameters[0].Value = model.拥有者账号ID;
            parameters[1].Value = model.场次ID;
            parameters[2].Value = model.座位ID;
            parameters[3].Value = model.购票时间;
            parameters[4].Value = model.影票状态;
            parameters[5].Value = model.影票ID;

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from 影票 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }
        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static MovieModel.Tickets GetModel(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 影票ID,拥有者账号ID,场次ID,座位ID,购票时间,影票状态 from 影票 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);
            MovieModel.Tickets model = new MovieModel.Tickets();
            if (row != null)
            {
                while (row.Read())
                {
                    if (row["影票ID"] != null)
                    {
                        model.影票ID = row["影票ID"].ToString();
                    }
                    if (row["拥有者账号ID"] != null)
                    {
                        model.拥有者账号ID = row["拥有者账号ID"].ToString();
                    }
                    if (row["场次ID"] != null)
                    {
                        model.场次ID = row["场次ID"].ToString();
                    }
                    if (row["座位ID"] != null && row["座位ID"].ToString() != "")
                    {
                        model.座位ID = int.Parse(row["座位ID"].ToString());
                    }
                    if (row["购票时间"] != null && row["购票时间"].ToString() != "")
                    {
                        model.购票时间 = DateTime.Parse(row["购票时间"].ToString());
                    }
                    if (row["影票状态"] != null && row["影票状态"].ToString() != "")
                    {
                        model.影票状态 = int.Parse(row["影票状态"].ToString());
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
        public static List<MovieModel.Tickets> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 影票ID,拥有者账号ID,场次ID,座位ID,购票时间,影票状态 ");
            strSql.Append(" FROM 影票 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);
            List<MovieModel.Tickets> list = new List<MovieModel.Tickets>();
            if (row != null)
            {
                while (row.Read())
                {
                    MovieModel.Tickets model = new MovieModel.Tickets();
                    if (row["影票ID"] != null)
                    {
                        model.影票ID = row["影票ID"].ToString();
                    }
                    if (row["拥有者账号ID"] != null)
                    {
                        model.拥有者账号ID = row["拥有者账号ID"].ToString();
                    }
                    if (row["场次ID"] != null)
                    {
                        model.场次ID = row["场次ID"].ToString();
                    }
                    if (row["座位ID"] != null && row["座位ID"].ToString() != "")
                    {
                        model.座位ID = int.Parse(row["座位ID"].ToString());
                    }
                    if (row["购票时间"] != null && row["购票时间"].ToString() != "")
                    {
                        model.购票时间 = DateTime.Parse(row["购票时间"].ToString());
                    }
                    if (row["影票状态"] != null && row["影票状态"].ToString() != "")
                    {
                        model.影票状态 = int.Parse(row["影票状态"].ToString());
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
        public static List<MovieModel.Tickets> GetList(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.影票ID desc");
            }
            strSql.Append(")AS Row, T.*  from 影票 T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);
            List<MovieModel.Tickets> list = new List<MovieModel.Tickets>();
            if (row != null)
            {
                while (row.Read())
                {
                    MovieModel.Tickets model = new MovieModel.Tickets();
                    if (row["影票ID"] != null)
                    {
                        model.影票ID = row["影票ID"].ToString();
                    }
                    if (row["拥有者账号ID"] != null)
                    {
                        model.拥有者账号ID = row["拥有者账号ID"].ToString();
                    }
                    if (row["场次ID"] != null)
                    {
                        model.场次ID = row["场次ID"].ToString();
                    }
                    if (row["座位ID"] != null && row["座位ID"].ToString() != "")
                    {
                        model.座位ID = int.Parse(row["座位ID"].ToString());
                    }
                    if (row["购票时间"] != null && row["购票时间"].ToString() != "")
                    {
                        model.购票时间 = DateTime.Parse(row["购票时间"].ToString());
                    }
                    if (row["影票状态"] != null && row["影票状态"].ToString() != "")
                    {
                        model.影票状态 = int.Parse(row["影票状态"].ToString());
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

