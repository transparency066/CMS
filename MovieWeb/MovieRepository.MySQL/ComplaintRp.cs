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
    public class ComplaintRp
    {
        private string connectionString = "server=localhost;database=moviedb;username=root;pwd=123";

        //添加评论
        public int AddComplaint(DateTime time, string uid, string content, int reply)
        {
            string sql = "insert into 反馈记录 set 反馈时间 = @time, 账号ID = @uid, 反馈内容=@content,是否回复=@reply";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
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

        //用户默认页面显示的反馈投诉情况
        public List<Complaint> GetComplaints(string uid)
        {
            string sql = "select * from 反馈记录 where 账号ID=" + uid;

            var result = new List<Complaint>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Complaint()
                        {
                            ComplaintTime = reader.GetDateTime(0),
                            UID = reader.GetString(1),
                            ComplaintText = reader.GetString(2),
                            ReplyFlag = reader.GetInt16(3),
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

        //管理员查看反馈投诉
        public List<Complaint> ViewComplaint()
        {
            string queryString = "SELECT  反馈时间, 账号ID, 反馈内容, 是否回复 FROM 反馈记录 WHERE(是否回复 = 0)";

            var result = new List<Complaint>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(queryString, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Complaint()
                    {
                        UID = reader["账号ID"].ToString(),
                        ComplaintTime = DateTime.Parse(reader["反馈时间"].ToString()),
                        ComplaintText = reader["账号ID"].ToString(),
                    });

                }
                reader.Close();
            }
            return result;
        }
    }
}
