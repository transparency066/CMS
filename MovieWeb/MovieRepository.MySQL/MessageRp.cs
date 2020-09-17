using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    public class MessageRp
    {
        private string connectionString = "server=localhost;database=moviedb;username=root;pwd=123";

        //发送消息给用户
        public int SendMessage(string uid, DateTime complainTime, string aid, DateTime replyTime, string message)
        {
            //string uid = messages.UID;
            //DateTime complainTime = messages.ComplaintTime;
            //string aid = messages.AdminID;
            //DateTime replyTime = messages.ReplyTime;
            //string message = messages.Text;
            string signString = "SELECT  反馈时间, 账号ID, 是否回复 FROM 反馈记录";
            string queryString1 = "INSERT INTO 回复内容 SET" + " 反馈账号ID='" + uid + "',反馈时间='" + complainTime + "'," +
                " 回复管理员账号ID='" + aid + "',回复时间='" + replyTime + "',回复内容='" + message + "'";
            //string queryString2 = "START TRANSACTION;" +
            //            "SET foreign_key_checks = 0;" +
            //            "UPDATE 反馈记录 SET 是否回复='" + 1 +                  
            //            "SET foreign_key_checks = 1;COMMIT;";
            string queryString2 = "UPDATE 反馈记录 SET 是否回复= 1";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command0 = new MySqlCommand())
                {
                    command0.Connection = connection;
                    command0.CommandText = signString;
                    MySqlDataReader reader = command0.ExecuteReader();
                    while (reader.Read())
                    {
                        if (uid == reader["账号ID"].ToString() && replyTime == DateTime.Parse((reader["反馈时间"]).ToString()) && "1" == reader["是否回复"].ToString())
                        {
                            reader.Close();
                            connection.Close();
                            return 0;
                        }

                    }
                    reader.Close();
                }
                using (MySqlCommand command1 = new MySqlCommand())
                {
                    command1.Connection = connection;
                    command1.CommandText = queryString1;
                    command1.ExecuteNonQuery();
                }
                using (MySqlCommand command2 = new MySqlCommand())
                {
                    command2.Connection = connection;
                    command2.CommandText = queryString2;
                    command2.ExecuteNonQuery();
                }
                connection.Close();
            }
            return 1;
        }

        //用户查看消息
        public List<Message> GetMessages(string uid)
        {
            string sql = "select * from 回复内容 join 反馈记录 where 反馈账号ID=@uid and 回复内容.反馈账号ID=反馈记录.账号ID and 回复内容.反馈时间=反馈记录.反馈时间";
            var result = new List<Message>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@uid", uid));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Message()
                        {
                            AdminID = reader.GetString("回复管理员账号ID"),
                            ReplyTime = reader.GetDateTime("回复时间"),
                            Text = reader.GetString("回复内容"),
                            ComplaintTime = reader.GetDateTime("反馈时间"),
                            FeedBackText=reader.GetString("反馈内容"),
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

    }
}
