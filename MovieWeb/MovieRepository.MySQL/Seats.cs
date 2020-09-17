using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    /// <summary>
    /// 数据访问类:座位
    /// </summary>
    public class Seats
    {
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static int Exists(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from 座位");
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
        public static int Add(MovieModel.Seats model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into 座位(");
            strSql.Append("座位ID,放映厅ID,行位置,列位置)");
            strSql.Append(" values (");
            strSql.Append("@座位ID,@放映厅ID,@行位置,@列位置)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@座位ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@放映厅ID", MySqlDbType.VarChar,5),
                    new MySqlParameter("@行位置", MySqlDbType.Int32,11),
                    new MySqlParameter("@列位置", MySqlDbType.Int32,11)};
            parameters[0].Value = model.座位ID;
            parameters[1].Value = model.放映厅ID;
            parameters[2].Value = model.行位置;
            parameters[3].Value = model.列位置;

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(MovieModel.Seats model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update 座位 set ");
            strSql.Append("行位置=@行位置,");
            strSql.Append("列位置=@列位置");
            strSql.Append(" where 座位ID=@座位ID and 放映厅ID=@放映厅ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@行位置", MySqlDbType.Int32,11),
                    new MySqlParameter("@列位置", MySqlDbType.Int32,11),
                    new MySqlParameter("@座位ID", MySqlDbType.Int32,11),
                    new MySqlParameter("@放映厅ID", MySqlDbType.VarChar,5)};
            parameters[0].Value = model.行位置;
            parameters[1].Value = model.列位置;
            parameters[2].Value = model.座位ID;
            parameters[3].Value = model.放映厅ID;

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from 座位 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static MovieModel.Seats GetModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 座位ID,放映厅ID,行位置,列位置 from 座位 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);

            MovieModel.Seats model = new MovieModel.Seats();
            if (row != null)
            {
                while (row.Read())
                {
                    if (row["座位ID"] != null && row["座位ID"].ToString() != "")
                    {
                        model.座位ID = int.Parse(row["座位ID"].ToString());
                    }
                    if (row["放映厅ID"] != null)
                    {
                        model.放映厅ID = row["放映厅ID"].ToString();
                    }
                    if (row["行位置"] != null && row["行位置"].ToString() != "")
                    {
                        model.行位置 = int.Parse(row["行位置"].ToString());
                    }
                    if (row["列位置"] != null && row["列位置"].ToString() != "")
                    {
                        model.列位置 = int.Parse(row["列位置"].ToString());
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
        public static List<MovieModel.Seats> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 座位ID,放映厅ID,行位置,列位置 ");
            strSql.Append(" FROM 座位 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);

            List<MovieModel.Seats> list = new List<MovieModel.Seats>();
            if (row != null)
            {
                while (row.Read())
                {
                    MovieModel.Seats model = new MovieModel.Seats();
                    if (row["座位ID"] != null && row["座位ID"].ToString() != "")
                    {
                        model.座位ID = int.Parse(row["座位ID"].ToString());
                    }
                    if (row["放映厅ID"] != null)
                    {
                        model.放映厅ID = row["放映厅ID"].ToString();
                    }
                    if (row["行位置"] != null && row["行位置"].ToString() != "")
                    {
                        model.行位置 = int.Parse(row["行位置"].ToString());
                    }
                    if (row["列位置"] != null && row["列位置"].ToString() != "")
                    {
                        model.列位置 = int.Parse(row["列位置"].ToString());
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
        public static List<MovieModel.Seats> GetList(string strWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from 座位 T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);

            List<MovieModel.Seats> list = new List<MovieModel.Seats>();
            if (row != null)
            {
                while (row.Read())
                {
                    MovieModel.Seats model = new MovieModel.Seats();
                    if (row["座位ID"] != null && row["座位ID"].ToString() != "")
                    {
                        model.座位ID = int.Parse(row["座位ID"].ToString());
                    }
                    if (row["放映厅ID"] != null)
                    {
                        model.放映厅ID = row["放映厅ID"].ToString();
                    }
                    if (row["行位置"] != null && row["行位置"].ToString() != "")
                    {
                        model.行位置 = int.Parse(row["行位置"].ToString());
                    }
                    if (row["列位置"] != null && row["列位置"].ToString() != "")
                    {
                        model.列位置 = int.Parse(row["列位置"].ToString());
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

