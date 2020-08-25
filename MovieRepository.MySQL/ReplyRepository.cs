using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    public class ReplyRepository
    {
        private string connectionString = "server=localhost;user=root;database=影院管理系统;port=3306;password=12345678";
        //连接数据库      
        public int SendMessage(MessageSend messages)
        {
            string uid = messages.UID;
            DateTime complainTime = messages.ComplaintTime;
            string aid = messages.AdminID;
            DateTime replyTime = messages.ReplyTime;
            string message = messages.Message;
            string signString = "SELECT  反馈时间, 账号ID, 是否回复 FROM 反馈记录";
            string queryString1 = "INSERT INTO 反馈ID SET" + " 账号ID='" + uid + "',反馈时间='" + complainTime + "'," +
                " 回复管理员账号ID='" + aid + "',回复时间='" + replyTime + "',回复内容='" + message + "'";
            string queryString2 = "START TRANSACTION;" +
                        "SET foreign_key_checks = 0;" +
                        "UPDATE 反馈记录 SET 是否回复='" + 1 +                  
                        "SET foreign_key_checks = 1;COMMIT;";
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
                        if (uid == reader["账号ID"].ToString() || replyTime == DateTime.Parse((reader["反馈时间"]).ToString()) || "1" == reader["是否回复"].ToString())
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
    }
}
