using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MovieModel;
using System.ComponentModel.Design;
using System.Data;

namespace MovieRepository.MySQL
{
    public class MovieRepository_nyx
    {
        private string connString = "server=localhost;database=moviedb;uid=root;pwd=123;Allow User Variables=True";
        public List<Comment> GetAll()
        {
            string sql = "select * from 评论记录";
            var result = new List<Comment>();
            using (MySqlConnection connection = new MySqlConnection(connString))
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
        public int AddComment(string mid, string uid, DateTime time, string content)
        {
            string movieID = mid;
            string userID = uid;
            DateTime commentTime = time;
            string commentContent = content;

            string sql = "insert into 评论记录 set 影片ID = @movieID, 账号ID = @userID, 评论时间=@commentTime,评论内容=@commentContent";
            using (MySqlConnection connection = new MySqlConnection(connString))
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
        public int AddComplaint(DateTime time, string uid, string content, int reply)
        {
            string sql = "insert into 反馈记录 set 反馈时间 = @time, 账号ID = @uid, 反馈内容=@content,是否回复=@reply";
            using (MySqlConnection connection = new MySqlConnection(connString))
            {
                int success = -1;
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    Console.WriteLine(cmd.CommandText);
                    cmd.CommandType = CommandType.Text;
                    MySqlParameter p1 = new MySqlParameter("@time", MySqlDbType.DateTime);
                    p1.Value = time;
                    MySqlParameter p2 = new MySqlParameter("@uid", MySqlDbType.VarChar);
                    p2.Value = uid;
                    MySqlParameter p3 = new MySqlParameter("@content", MySqlDbType.VarChar);
                    p3.Value = content;
                    MySqlParameter p4 = new MySqlParameter("@reply", MySqlDbType.Int16);
                    p4.Value = reply;
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
    }
}
