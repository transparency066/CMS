using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MovieModel;
using System.Data;

namespace MovieRepository.MySQL
{
    public class WishRp
    {
        private string connectionstring = "server=localhost;database=moviedb;username=root;pwd=123;";
        //Old Guids=true;可加可不加

        public List<wishModel> GetWishModels(string uid)//获取收藏影片信息
        {
            string sql = "select 影片ID,收藏时间,名字,图片url,上映日期,下线日期,简介 from 收藏记录 natural join 影片 where 账号ID=@uid order by 收藏时间 desc";
            var result = new List<wishModel>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@uid", uid));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new wishModel()
                        {
                            movieID=reader.GetString("影片ID"),
                            movieName=reader.GetString("名字"),
                            wishTime=reader.GetDateTime("收藏时间"),
                            movieImgPath=reader.GetString("图片url"),
                            upTime=reader.GetDateTime("上映日期"),
                            downTime=reader.GetDateTime("下线日期"),
                            movieDetail=reader.GetString("简介"),
                        });
                    }
                    reader.Close();
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public bool IsInWishList(string uid, string movieID)
        {
            string sql = "select * from 收藏记录 where 账号ID=@uid and 影片ID=@movieID";
            bool flag = true;
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@uid", uid));
                cmd.Parameters.Add(new MySqlParameter("@movieID", movieID));
                MySqlDataAdapter reader = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                reader.Fill(ds, "收藏记录");
                DataTable dt = ds.Tables["收藏记录"];
                if (dt.Rows.Count == 0) flag = false;
            }
            return flag;
        }

        public bool AddWishModels(string uid,string movieID,DateTime wishtime)//添加收藏影片
        {
            bool flag = true;
            string sql = "insert into 收藏记录 set 账号ID=@uid , 影片ID=@movieID , 收藏时间=@wishtime";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@uid",uid));
                cmd.Parameters.Add(new MySqlParameter("@movieID", movieID));
                cmd.Parameters.Add(new MySqlParameter("@wishtime", wishtime));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    flag = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return flag;
        }

        public void DeleteWishModels(string uid,string movieID)//删除收藏影片
        {
            string sql = "delete from 收藏记录 where 影片ID=@movieID and 账号ID=@uid";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@movieID", movieID));
                cmd.Parameters.Add(new MySqlParameter("@uid", uid));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
