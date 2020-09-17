using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    /// <summary>
    /// 数据访问类:影片
    /// </summary>
    public class Movie
    {
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static int Exists(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from 影片");
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
        public static int Add(MovieModel.Movie model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into 影片(");
            strSql.Append("影片ID,名字,类型,时长,上映日期,下线日期,票价,简介,图片url)");
            strSql.Append(" values (");
            strSql.Append("@影片ID,@名字,@类型,@时长,@上映日期,@下线日期,@票价,@简介,@图片url)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@影片ID", MySqlDbType.VarChar,10),
                    new MySqlParameter("@名字", MySqlDbType.VarChar,50),
                    new MySqlParameter("@类型", MySqlDbType.Int32,11),
                    new MySqlParameter("@时长", MySqlDbType.Time),
                    new MySqlParameter("@上映日期", MySqlDbType.Date),
                    new MySqlParameter("@下线日期", MySqlDbType.Date),
                    new MySqlParameter("@票价", MySqlDbType.Float),
                    new MySqlParameter("@简介", MySqlDbType.VarChar,300),
                    new MySqlParameter("@图片url", MySqlDbType.VarChar,100)};
            parameters[0].Value = model.影片ID;
            parameters[1].Value = model.名字;
            parameters[2].Value = model.类型;
            parameters[3].Value = model.时长;
            parameters[4].Value = model.上映日期;
            parameters[5].Value = model.下线日期;
            parameters[6].Value = model.票价;
            parameters[7].Value = model.简介;
            parameters[8].Value = model.图片url;

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(MovieModel.Movie model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update 影片 set ");
            strSql.Append("名字=@名字,");
            strSql.Append("类型=@类型,");
            strSql.Append("时长=@时长,");
            strSql.Append("上映日期=@上映日期,");
            strSql.Append("下线日期=@下线日期,");
            strSql.Append("票价=@票价,");
            strSql.Append("简介=@简介,");
            strSql.Append("图片url=@图片url");
            strSql.Append(" where 影片ID=@影片ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@名字", MySqlDbType.VarChar,50),
                    new MySqlParameter("@类型", MySqlDbType.Int32,11),
                    new MySqlParameter("@时长", MySqlDbType.Time),
                    new MySqlParameter("@上映日期", MySqlDbType.Date),
                    new MySqlParameter("@下线日期", MySqlDbType.Date),
                    new MySqlParameter("@票价", MySqlDbType.Float),
                    new MySqlParameter("@简介", MySqlDbType.VarChar,300),
                    new MySqlParameter("@图片url", MySqlDbType.VarChar,100),
                    new MySqlParameter("@影片ID", MySqlDbType.VarChar,10)};
            parameters[0].Value = model.名字;
            parameters[1].Value = model.类型;
            parameters[2].Value = model.时长;
            parameters[3].Value = model.上映日期;
            parameters[4].Value = model.下线日期;
            parameters[5].Value = model.票价;
            parameters[6].Value = model.简介;
            parameters[7].Value = model.图片url;
            parameters[8].Value = model.影片ID;

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static int Delete(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from 影片 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);

            return DbHelperMySQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static MovieModel.Movie GetModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 影片ID,名字,类型,时长,上映日期,下线日期,票价,简介,图片url from 影片 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);

            MovieModel.Movie model = new MovieModel.Movie();
            if (row != null)
            {
                while (row.Read())
                {
                    if (row["影片ID"] != null)
                    {
                        model.影片ID = row["影片ID"].ToString();
                    }
                    if (row["名字"] != null)
                    {
                        model.名字 = row["名字"].ToString();
                    }
                    if (row["类型"] != null && row["类型"].ToString() != "")
                    {
                        model.类型 = row["类型"].ToString();
                    }
                    if (row["时长"] != null && row["时长"].ToString() != "")
                    {
                        model.时长 = DateTime.Parse(row["时长"].ToString());
                    }
                    if (row["上映日期"] != null && row["上映日期"].ToString() != "")
                    {
                        model.上映日期 = DateTime.Parse(row["上映日期"].ToString());
                    }
                    if (row["下线日期"] != null && row["下线日期"].ToString() != "")
                    {
                        model.下线日期 = DateTime.Parse(row["下线日期"].ToString());
                    }
                    if (row["票价"] != null && row["票价"].ToString() != "")
                    {
                        model.票价 = float.Parse(row["票价"].ToString());
                    }
                    if (row["简介"] != null)
                    {
                        model.简介 = row["简介"].ToString();
                    }
                    if (row["图片url"] != null)
                    {
                        model.图片url = row["图片url"].ToString();
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
        public static List<MovieModel.Movie> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 影片ID,名字,类型,时长,上映日期,下线日期,票价,简介,图片url ");
            strSql.Append(" FROM 影片 ");
            strSql.Append(" where 1=1 ");
            strSql.Append(strWhere);
            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);

            List<MovieModel.Movie> list = new List<MovieModel.Movie>();
            if (row != null)
            {
                while (row.Read())
                {
                    MovieModel.Movie model = new MovieModel.Movie();
                    if (row["影片ID"] != null)
                    {
                        model.影片ID = row["影片ID"].ToString();
                    }
                    if (row["名字"] != null)
                    {
                        model.名字 = row["名字"].ToString();
                    }
                    if (row["类型"] != null && row["类型"].ToString() != "")
                    {
                        model.类型 = row["类型"].ToString();
                    }
                    if (row["时长"] != null && row["时长"].ToString() != "")
                    {
                        model.时长 = DateTime.Parse(row["时长"].ToString());
                    }
                    if (row["上映日期"] != null && row["上映日期"].ToString() != "")
                    {
                        model.上映日期 = DateTime.Parse(row["上映日期"].ToString());
                    }
                    if (row["下线日期"] != null && row["下线日期"].ToString() != "")
                    {
                        model.下线日期 = DateTime.Parse(row["下线日期"].ToString());
                    }
                    if (row["票价"] != null && row["票价"].ToString() != "")
                    {
                        model.票价 = float.Parse(row["票价"].ToString());
                    }
                    if (row["简介"] != null)
                    {
                        model.简介 = row["简介"].ToString();
                    }
                    if (row["图片url"] != null)
                    {
                        model.图片url = row["图片url"].ToString();
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
        public static List<MovieModel.Movie> GetList(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.影片ID desc");
            }
            strSql.Append(")AS Row, T.*  from 影片 T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);

            MySqlDataReader row = DbHelperMySQL.ExecuteReader(CommandType.Text, strSql.ToString(), null);
            List<MovieModel.Movie> list = new List<MovieModel.Movie>();
            if (row != null)
            {
                while (row.Read())
                {
                    MovieModel.Movie model = new MovieModel.Movie();
                    if (row["影片ID"] != null)
                    {
                        model.影片ID = row["影片ID"].ToString();
                    }
                    if (row["名字"] != null)
                    {
                        model.名字 = row["名字"].ToString();
                    }
                    if (row["类型"] != null && row["类型"].ToString() != "")
                    {
                        model.类型 = row["类型"].ToString();
                    }
                    if (row["时长"] != null && row["时长"].ToString() != "")
                    {
                        model.时长 = DateTime.Parse(row["时长"].ToString());
                    }
                    if (row["上映日期"] != null && row["上映日期"].ToString() != "")
                    {
                        model.上映日期 = DateTime.Parse(row["上映日期"].ToString());
                    }
                    if (row["下线日期"] != null && row["下线日期"].ToString() != "")
                    {
                        model.下线日期 = DateTime.Parse(row["下线日期"].ToString());
                    }
                    if (row["票价"] != null && row["票价"].ToString() != "")
                    {
                        model.票价 = float.Parse(row["票价"].ToString());
                    }
                    if (row["简介"] != null)
                    {
                        model.简介 = row["简介"].ToString();
                    }
                    if (row["图片url"] != null)
                    {
                        model.图片url = row["图片url"].ToString();
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

