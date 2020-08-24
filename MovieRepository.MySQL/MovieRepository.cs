using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;
//using MovieModel;
//using Mysql.Data.MysqlClient;
using MySql.Data.MySqlClient;


namespace MovieRepository.MySQL
{
    public class MovieRepository
    {
        private string connectionString = "server=localhost;user=root;database=影院管理系统;port=3306;password=zc70082315";

        //连接数据库，进行数据库操作
        public void InsertUserinfs0()//这个函数是测试用函数请忽略
        {
            string queryString1 = "INSERT INTO 账号 SET 账号ID='testuser1',密码='123456',权限='0'";
            string queryString2 = "INSERT INTO 用户 SET 账号ID='testuser1',昵称='测试用户1',电话='13898400000',性别='1'";
            //var result = new List<Userinf>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
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
            //return result;
        }

        public int InsertUserinfs(Userinf userinf)
        {
            string account = userinf.Account;
            string password = userinf.Password;
            string name = userinf.Name;
            string phone = userinf.Phone;
            int sex = userinf.Sex;
            string checkString = "SELECT 账号ID,昵称 FROM 用户";
            string queryString1 = "INSERT INTO 账号 SET" + " 账号ID='" + account + "',密码='" + password + "',权限='0'";
            string queryString2 = "INSERT INTO 用户 SET" + " 账号ID='" + account + "',昵称='" + name + "',电话='" + phone + "',性别='" + sex + "'";
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

                        if (account == reader["账号ID"].ToString() || name == reader["昵称"].ToString())
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

        public Userinf SearchUserinfs(Userinf userinf)
        {
            Userinf result = new Userinf();
            result.flag = -1;
            string account = userinf.Account;
            string queryString1 = "SELECT 账号ID,密码 FROM 账号 WHERE 权限=0";
            string queryString2 = "SELECT 账号ID,昵称,电话,性别 FROM 用户";
            int flag = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command1 = new MySqlCommand())
                {
                    command1.Connection = connection;
                    command1.CommandText = queryString1;
                    MySqlDataReader reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        if (account == reader["账号ID"].ToString())
                        {
                            flag = 1;
                            result.flag = 1;
                            result.Account = reader["账号ID"].ToString();
                            result.Password = reader["密码"].ToString();
                            break;
                        }
                    }
                    reader.Close();
                    if (flag == 0)
                    {
                        connection.Close();
                        result.flag = 0;
                        return result;
                    }
                }
                using (MySqlCommand command2 = new MySqlCommand())
                {
                    command2.Connection = connection;
                    command2.CommandText = queryString2;
                    MySqlDataReader reader = command2.ExecuteReader();
                    while (reader.Read())
                    {
                        if (account == reader["账号ID"].ToString())
                        {
                            result.Name = reader["昵称"].ToString();
                            result.Phone = reader["电话"].ToString();
                            result.Sex = (int)reader["性别"];
                        }
                    }
                    reader.Close();

                }
                connection.Close();
            }
            return result;
        }

        public int DeleteUserinfs(Userinf userinf)
        {
            string account = userinf.Account;
            int flag = 0;
            string queryString1 = "START TRANSACTION;" +
                "SET foreign_key_checks = 0;" +
                "DELETE FROM 账号 WHERE 账号ID='" + account + "';" +
                "DELETE FROM 用户 WHERE 账号ID='" + account + "';" +
                "DELETE FROM 反馈记录 WHERE 账号ID='" + account + "';" +
                "DELETE FROM 回复内容 WHERE 反馈账号ID='" + account + "';" +
                "DELETE FROM 评分记录 WHERE 账号ID='" + account + "';" +
                "DELETE FROM 收藏记录 WHERE 账号ID='" + account + "';" +
                "DELETE FROM 影票 WHERE 拥有者账号ID='" + account + "';" +
                "SET foreign_key_checks = 1;COMMIT;";

            //string queryString2 = "DELETE FROM 账号 WHERE 账号ID='" + account + "'";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command1 = new MySqlCommand())
                {
                    command1.Connection = connection;
                    command1.CommandText = queryString1;
                    command1.ExecuteNonQuery();
                    flag += 2;
                }
                /*using(MySqlCommand command2=new MySqlCommand())
                {
                    command2.Connection = connection;
                    command2.CommandText = queryString2;
                    command2.ExecuteNonQuery();
                    flag += 1;
                }*/
                connection.Close();
            }
            return flag;
        }

        public int UpdateUserinfs(Userinf userinf, string currentacc)
        {
            int flag = 1;
            string account = userinf.Account;
            string password = userinf.Password;
            string name = userinf.Name;
            string phone = userinf.Phone;
            int sex = userinf.Sex;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                if (account != currentacc)
                {
                    string checkString1 = "SELECT 账号ID,昵称 FROM 用户 WHERE 账号ID!='" + currentacc + "'";
                    string queryString1 = "START TRANSACTION;" +
                        "SET foreign_key_checks = 0;" +
                        "UPDATE 用户 SET 账号ID='" + account + "' WHERE 账号ID='" + currentacc + "';" +
                        "UPDATE 账号 SET 账号ID='" + account + "' WHERE 账号ID='" + currentacc + "';" +
                        "UPDATE 反馈记录 SET 账号ID='" + account + "' WHERE 账号ID='" + currentacc + "';" +
                        "UPDATE 回复内容 SET 反馈账号ID='" + account + "' WHERE 反馈账号ID='" + currentacc + "';" +
                        "UPDATE 评分记录 SET 账号ID='" + account + "' WHERE 账号ID='" + currentacc + "';" +
                        "UPDATE 收藏记录 SET 账号ID='" + account + "' WHERE 账号ID='" + currentacc + "';" +
                        "UPDATE 影票 SET 拥有者账号ID='" + account + "' WHERE 拥有者账号ID='" + currentacc + "';" +
                        "SET foreign_key_checks = 1;COMMIT;";
                    using (MySqlCommand check1 = new MySqlCommand())
                    {
                        check1.Connection = connection;
                        check1.CommandText = checkString1;
                        MySqlDataReader reader = check1.ExecuteReader();
                        while (reader.Read())
                        {

                            if (account == reader["账号ID"].ToString() || name == reader["昵称"].ToString())
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
                    string checkString2 = "SELECT 昵称 FROM 用户 WHERE 账号ID!='" + currentacc + "'";
                    using (MySqlCommand check2 = new MySqlCommand())
                    {
                        check2.Connection = connection;
                        check2.CommandText = checkString2;
                        MySqlDataReader reader = check2.ExecuteReader();
                        while (reader.Read())
                        {

                            if (name == reader["昵称"].ToString())
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
                string queryString2 = "UPDATE 账号 SET 密码='" + password + "' WHERE 账号ID='" + account + "';" +
                    "UPDATE 用户 SET 昵称='" + name + "',电话='" + phone + "',性别='" + sex + "' WHERE 账号ID='" + account + "';";
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


        public int InsertMovieinfs(Movieinf movieinf)
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

        public Movieinf SearchMovieinfs(Movieinf movieinf)
        {
            string id = movieinf.ID;
            Movieinf result = new Movieinf();
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

        public int DeleteMovieinfs(Movieinf movieinf)
        {
            string id = movieinf.ID;
            int flag = 0;
            List<string> sessions = new List<string>();
            // List<int> number = new List<int>();
            List<string> tickets = new List<string>();
            string sessionid;
            string ticketid;
            string findSesString = "SELECT 场次ID FROM 场次 WHERE 影片ID='" + id + "'";
            string deleteString = "START TRANSACTION;" +
                "SET foreign_key_checks = 0;" +
                "DELETE FROM 影片 WHERE 影片ID='" + id + "';" +
                "DELETE FROM 场次 WHERE 影片ID='" + id + "';" +
                "DELETE FROM 评论记录 WHERE 影片ID='" + id + "';" +
                "DELETE FROM 收藏记录 WHERE 影片ID='" + id + "';" +
                "SET foreign_key_checks = 1;COMMIT;";
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

        public int UpdateMovieinfs(Movieinf movieinf, string currentid)
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
                    string queryString1 = "START TRANSACTION;" +
                        "SET foreign_key_checks = 0;" +
                        "UPDATE 影片 SET 影片ID='" + id + "' WHERE 影片ID='" + currentid + "';" +
                        "UPDATE 场次 SET 影片ID='" + id + "' WHERE 影片ID='" + currentid + "';" +
                        "UPDATE 评论记录 SET 影片ID='" + id + "' WHERE 影片ID='" + currentid + "';" +
                        "UPDATE 收藏记录 SET 影片ID='" + id + "' WHERE 影片ID='" + currentid + "';" +
                        "SET foreign_key_checks = 1;COMMIT;";
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

        public List<Movieinf> GetallMovieinfs()
        {
            string queryString = "SELECT 影片ID,名字,类型,时长,上映日期,下线日期,票价,简介,图片url FROM 影片";
            var result = new List<Movieinf>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Movieinf()
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

        public List<Userinf> GetallUserinfs()
        {
            string queryString = "SELECT 账号ID,昵称,电话,性别 FROM 用户";    
            var result = new List<Userinf>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Userinf()
                        {
                            Account = reader["账号ID"].ToString(),
                            Name = reader["昵称"].ToString(),
                            Phone = reader["电话"].ToString(),
                            Sex = (int)reader["性别"],
                           // Password=reader["密码"].ToString()

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
