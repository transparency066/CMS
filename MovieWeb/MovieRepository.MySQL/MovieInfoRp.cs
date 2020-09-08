using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    public class MovieInfoRp
    {
        private string connectionString = "server=localhost;database=moviedb;username=root;pwd=123";

        //插入影片信息(参数太多用模型传递)
        public int InsertMovieinfs(Movieinfo movieinf)
        {
            string id = movieinf.ID;
            string name = movieinf.Name;
            string type = movieinf.Type;
            string time = movieinf.Time;
            string ondate = movieinf.Ondate;
            string outdate = movieinf.Outdate;
            float price = movieinf.Price;
            string intro = movieinf.Intro;
            string url = movieinf.Url;

            string checkString = "SELECT 影片ID,名字 FROM 影片";
            string queryString = "INSERT INTO 影片 SET" + " 影片ID='" + id + "',名字='" + name + "'," +
                "类型='" + type + "',时长='" + time + "',上映日期='" + ondate + "',下线日期='" + outdate + "'," +
                "票价='" + price + "',简介='" + intro + "',图片url='" + url + "'";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command0 = new MySqlCommand())
                {
                    command0.Connection = connection;
                    command0.CommandText = checkString;
                    MySqlDataReader reader = command0.ExecuteReader();
                    while (reader.Read())
                    {

                        if (id == reader["影片ID"].ToString() || name == reader["名字"].ToString())
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
                    command1.CommandText = queryString;
                    command1.ExecuteNonQuery();
                }
                connection.Close();
            }
            return 1;

        }

        //查询影片信息
        public Movieinfo SearchMovieinfs(string id)
        {
            //string id = movieinf.ID;
            Movieinfo result = new Movieinfo();
            result.flag = 0;
            string queryString = "SELECT 影片ID,名字,类型,时长,上映日期,下线日期,票价,简介,图片url FROM 影片";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = queryString;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (id == reader["影片ID"].ToString())
                        {
                            result.flag = 1;
                            result.ID = reader["影片ID"].ToString();
                            result.Name = reader["名字"].ToString();
                            result.Type = reader["类型"].ToString();
                            result.Time = reader["时长"].ToString();
                            result.Ondate = reader["上映日期"].ToString();
                            result.Outdate = reader["下线日期"].ToString();
                            result.Price = (float)reader["票价"];
                            result.Intro = reader["简介"].ToString();
                            result.Url = reader["图片url"].ToString();
                            break;
                        }
                    }
                    reader.Close();

                }
                connection.Close();
            }
            return result;
        }

        //删除影片信息
        public int DeleteMovieinfs(string id)
        {
            //string id = movieinf.ID;
            int flag = 0;
            List<string> sessions = new List<string>();
            // List<int> number = new List<int>();
            List<string> tickets = new List<string>();
            string sessionid;
            string ticketid;
            string findSesString = "SELECT 场次ID FROM 场次 WHERE 影片ID='" + id + "'";
            //string deleteString = "START TRANSACTION;" +
            //    "SET foreign_key_checks = 0;" +
            //    "DELETE FROM 影片 WHERE 影片ID='" + id + "';" +
            //    "DELETE FROM 场次 WHERE 影片ID='" + id + "';" +
            //    "DELETE FROM 评论记录 WHERE 影片ID='" + id + "';" +
            //    "DELETE FROM 收藏记录 WHERE 影片ID='" + id + "';" +
            //    "SET foreign_key_checks = 1;COMMIT;";
            string deleteString = "DELETE FROM 影片 WHERE 影片ID='" + id + "'";
            //string queryString2 = "DELETE FROM 账号 WHERE 账号ID='" + account + "'";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand ReadSession = new MySqlCommand())
                {
                    ReadSession.Connection = connection;
                    ReadSession.CommandText = findSesString;
                    MySqlDataReader reader = ReadSession.ExecuteReader();
                    while (reader.Read())
                    {
                        sessionid = reader["场次ID"].ToString();
                        sessions.Add(sessionid);
                    }
                    reader.Close();
                }
                for (int i = 0; i < sessions.Count(); i++)
                {
                    using (MySqlCommand ReadTicket = new MySqlCommand())
                    {

                        string ReadTstring = "SELECT 影票ID FROM 影票 WHERE 场次ID='" + sessions[i] + "'";
                        ReadTicket.Connection = connection;
                        ReadTicket.CommandText = ReadTstring;
                        MySqlDataReader reader1 = ReadTicket.ExecuteReader();
                        while (reader1.Read())
                        {
                            ticketid = reader1["影票ID"].ToString();
                            tickets.Add(ticketid);
                        }
                        reader1.Close();
                    }
                }

                for (int i = 0; i < tickets.Count(); i++)
                {
                    using (MySqlCommand DeleteRecord = new MySqlCommand())
                    {
                        string DeleteRstring = "DELETE FROM 评分记录 WHERE 影票ID='" + tickets[i] + "'";
                        DeleteRecord.Connection = connection;
                        DeleteRecord.CommandText = DeleteRstring;
                        DeleteRecord.ExecuteNonQuery();
                    }
                }

                for (int i = 0; i < sessions.Count(); i++)
                {
                    using (MySqlCommand DeleteTicket = new MySqlCommand())
                    {
                        string DeleteTstring = "DELETE FROM 影票 WHERE 场次ID='" + sessions[i] + "'";
                        DeleteTicket.Connection = connection;
                        DeleteTicket.CommandText = DeleteTstring;
                        DeleteTicket.ExecuteNonQuery();
                    }
                }
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = deleteString;
                    command.ExecuteNonQuery();
                }
                flag = 1;
                connection.Close();
            }
            return flag;
        }

        //更新影片信息(参数太多用模型传递)
        public int UpdateMovieinfs(Movieinfo movieinf, string currentid)
        {
            int flag = 1;
            string id = movieinf.ID;
            string name = movieinf.Name;
            string type = movieinf.Type;
            string time = movieinf.Time;
            string ondate = movieinf.Ondate;
            string outdate = movieinf.Outdate;
            float price = movieinf.Price;
            string intro = movieinf.Intro;
            string url = movieinf.Url;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                if (id != currentid)
                {
                    string checkString1 = "SELECT 影片ID,名字 FROM 影片 WHERE 影片ID!='" + currentid + "'";
                    //string queryString1 = "START TRANSACTION;" +
                    //    "SET foreign_key_checks = 0;" +
                    //    "UPDATE 影片 SET 影片ID='" + id + "' WHERE 影片ID='" + currentid + "';" +
                    //    "UPDATE 场次 SET 影片ID='" + id + "' WHERE 影片ID='" + currentid + "';" +
                    //    "UPDATE 评论记录 SET 影片ID='" + id + "' WHERE 影片ID='" + currentid + "';" +
                    //    "UPDATE 收藏记录 SET 影片ID='" + id + "' WHERE 影片ID='" + currentid + "';" +
                    //    "SET foreign_key_checks = 1;COMMIT;";
                    string queryString1 = "UPDATE 影片 SET 影片ID='" + id + "' WHERE 影片ID='" + currentid + "'";
                    using (MySqlCommand check1 = new MySqlCommand())
                    {
                        check1.Connection = connection;
                        check1.CommandText = checkString1;
                        MySqlDataReader reader = check1.ExecuteReader();
                        while (reader.Read())
                        {

                            if (id == reader["影片ID"].ToString() || name == reader["名字"].ToString())
                            {
                                reader.Close();
                                connection.Close();
                                flag = 2; //flag=2 账号id或昵称重复
                                return flag;
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

                }
                else
                {
                    string checkString2 = "SELECT 名字 FROM 影片 WHERE 影片ID!='" + currentid + "'";
                    using (MySqlCommand check2 = new MySqlCommand())
                    {
                        check2.Connection = connection;
                        check2.CommandText = checkString2;
                        MySqlDataReader reader = check2.ExecuteReader();
                        while (reader.Read())
                        {

                            if (name == reader["名字"].ToString())
                            {
                                reader.Close();
                                connection.Close();
                                flag = 3; //昵称重复
                                return flag;
                            }

                        }
                        reader.Close();
                    }
                }
                string queryString2 = "UPDATE 影片 SET 名字='" + name + "',类型='" + type + "'," +
                    "时长='" + time + "',上映日期='" + ondate + "',下线日期='" + outdate + "',票价='" + price + "'," +
                    "简介='" + intro + "',图片url='" + url + "' WHERE 影片ID='" + id + "';";
                using (MySqlCommand command2 = new MySqlCommand())
                {
                    command2.Connection = connection;
                    command2.CommandText = queryString2;
                    command2.ExecuteNonQuery();
                }
                connection.Close();
            }
            return flag;
        }

        //获取所有影片信息
        public List<Movieinfo> GetallMovieinfs()
        {
            string queryString = "SELECT 影片ID,名字,类型,时长,上映日期,下线日期,票价,简介,图片url FROM 影片";
            var result = new List<Movieinfo>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Movieinfo()
                        {
                            ID = reader["影片ID"].ToString(),
                            Name = reader["名字"].ToString(),
                            Type = reader["类型"].ToString(),
                            Time = reader["时长"].ToString(),
                            Ondate = reader["上映日期"].ToString(),
                            Outdate = reader["下线日期"].ToString(),
                            Price = (float)reader["票价"],
                            Intro = reader["简介"].ToString(),
                            Url = reader["图片url"].ToString()

                        });
                    }
                    reader.Close();
                }
                catch (Exception ex) { }
                connection.Close();
            }
            return result;
        }

    }
}
