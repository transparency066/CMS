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
    public class CommentRp
    {
        private string connectionString = "server=localhost;database=moviedb;username=root;pwd=123";

        private MySqlConnection getConnect()
        {
            String connetStr = "server=localhost;port=3306;user=root;password=123; database=moviedb;SslMode=none;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            conn.Open();
            return conn;
        }

        //获取所有评论
        public List<Comment> GetComments(string mid)
        {
            string sql = "select * from 评论记录 where 影片ID=" + mid;

            var result = new List<Comment>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Comment()
                        {
                            movieID = reader.GetString(0),
                            userID = reader.GetString(1),
                            commentTime = reader.GetDateTime(2),
                            commentContent = reader.GetString(3),
                        });
                    }

                    reader.Close();
                }
                catch (Exception ex) { }
                finally
                {
                    connection.Close();
                }
            }

            return result;
        }

        //添加评论
        public int AddComment(string mid, string uid, DateTime time, string content)
        {
            string movieID = mid;
            string userID = uid;
            DateTime commentTime = time;
            string commentContent = content;

            string sql = "insert into 评论记录 set 影片ID = @movieID, 账号ID = @userID, 评论时间=@commentTime,评论内容=@commentContent";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                int success = -1;
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    Console.WriteLine(cmd.CommandText);
                    cmd.CommandType = CommandType.Text;
                    MySqlParameter p1 = new MySqlParameter("@movieID", MySqlDbType.VarChar);
                    p1.Value = movieID;
                    MySqlParameter p2 = new MySqlParameter("@userID", MySqlDbType.VarChar);
                    p2.Value = userID;
                    MySqlParameter p3 = new MySqlParameter("@commentTime", MySqlDbType.DateTime);
                    p3.Value = commentTime;
                    MySqlParameter p4 = new MySqlParameter("@commentContent", MySqlDbType.VarChar);
                    p4.Value = commentContent;
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    success = cmd.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    if (success > 0)
                    {
                        Console.WriteLine("数据插入成功！");
                        return success;
                    }
                    else
                    {
                        return success;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
                return success;
            }
        }

        //删除评论
        public Boolean DelComment(String pid, String id)
        {
                MySqlConnection conn = getConnect();
                try
                {
                    MySqlCommand mycom = conn.CreateCommand();
                    if (id == null || pid == null) return false;
                    mycom.CommandText = $"delete from 评论记录 where 账号ID1='{id}' and 影票ID1 = '{pid}' ";
                    mycom.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return true;
        }
    }
}
