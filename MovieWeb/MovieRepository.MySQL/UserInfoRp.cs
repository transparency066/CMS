using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MovieModel;

namespace MovieRepository.MySQL
{
    public class UserInfoRp
    {
        private string connectionString = "server=localhost;database=moviedb;username=root;pwd=123";

        //插入用户信息
        public int InsertUserinfs(string account, string password,string name,string phone,int sex)
        {
            //string account = userinf.UserName;
            //string password = userinf.PassWord;
            //string name = userinf.Name;
            //string phone = userinf.PhoneNumber;
            //int sex = userinf.Sex;
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

        //搜索用户信息
        public Account SearchUserinfs(string account)
        {
            Account result = new Account();
            result.flag = -1;
            //string account = userinf.UserName;
            string queryString = "SELECT 账号ID,密码,昵称,电话,性别 FROM 账号 natural join 用户";
            //string queryString2 = "SELECT 账号ID,昵称,电话,性别 FROM 用户";
            int flag = 0;
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
                        if (account == reader["账号ID"].ToString())
                        {
                            flag = 1;
                            result.flag = 1;
                            result.UserName = reader["账号ID"].ToString();
                            result.PassWord = reader["密码"].ToString();
                            result.Name = reader["昵称"].ToString();
                            result.PhoneNumber = reader["电话"].ToString();
                            result.Sex = (int)reader["性别"];
                            break;
                        }
                    }
                    reader.Close();
                    //if (flag == 0)
                    //{
                    //    connection.Close();
                    //    result.flag = 0;
                    //    return result;
                    //}
                }
                //using (MySqlCommand command2 = new MySqlCommand())
                //{
                //    command2.Connection = connection;
                //    command2.CommandText = queryString2;
                //    MySqlDataReader reader = command2.ExecuteReader();
                //    while (reader.Read())
                //    {
                //        if (account == reader["账号ID"].ToString())
                //        {
                //            result.Name = reader["昵称"].ToString();
                //            result.PhoneNumber = reader["电话"].ToString();
                //            result.Sex = (int)reader["性别"];
                //        }
                //    }
                //    reader.Close();

                //}
                connection.Close();
            }
            return result;
        }

        //删除用户信息
        public int DeleteUserinfs(string account)
        {
            //string account = userinf.UserName;
            int flag = 0;
            //string queryString1 = "START TRANSACTION;" +
            //    "SET foreign_key_checks = 0;" +
            //    "DELETE FROM 账号 WHERE 账号ID='" + account + "';" +
            //    "DELETE FROM 用户 WHERE 账号ID='" + account + "';" +
            //    "DELETE FROM 反馈记录 WHERE 账号ID='" + account + "';" +
            //    "DELETE FROM 回复内容 WHERE 反馈账号ID='" + account + "';" +
            //    "DELETE FROM 评分记录 WHERE 账号ID='" + account + "';" +
            //    "DELETE FROM 收藏记录 WHERE 账号ID='" + account + "';" +
            //    "DELETE FROM 影票 WHERE 拥有者账号ID='" + account + "';" +
            //    "SET foreign_key_checks = 1;COMMIT;";
            string queryString = "DELETE FROM 账号 WHERE 账号ID='" + account + "'";
            //string queryString2 = "DELETE FROM 账号 WHERE 账号ID='" + account + "'";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command1 = new MySqlCommand())
                {
                    command1.Connection = connection;
                    command1.CommandText = queryString;
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

        //更新用户信息
        public int UpdateUserinfs(string account,string password,string name,string phone, int sex,string currentacc)
        {
            int flag = 1;
            //string account = userinf.UserName;
            //string password = userinf.PassWord;
            //string name = userinf.Name;
            //string phone = userinf.PhoneNumber;
            //int sex = userinf.Sex;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                if (account != currentacc)
                {
                    string checkString1 = "SELECT 账号ID,昵称 FROM 用户 WHERE 账号ID!='" + currentacc + "'";
                    //string queryString1 = "START TRANSACTION;" +
                    //    "SET foreign_key_checks = 0;" +
                    //    "UPDATE 用户 SET 账号ID='" + account + "' WHERE 账号ID='" + currentacc + "';" +
                    //    "UPDATE 账号 SET 账号ID='" + account + "' WHERE 账号ID='" + currentacc + "';" +
                    //    "UPDATE 反馈记录 SET 账号ID='" + account + "' WHERE 账号ID='" + currentacc + "';" +
                    //    "UPDATE 回复内容 SET 反馈账号ID='" + account + "' WHERE 反馈账号ID='" + currentacc + "';" +
                    //    "UPDATE 评分记录 SET 账号ID='" + account + "' WHERE 账号ID='" + currentacc + "';" +
                    //    "UPDATE 收藏记录 SET 账号ID='" + account + "' WHERE 账号ID='" + currentacc + "';" +
                    //    "UPDATE 影票 SET 拥有者账号ID='" + account + "' WHERE 拥有者账号ID='" + currentacc + "';" +
                    //    "SET foreign_key_checks = 1;COMMIT;";
                    string queryString1 = "UPDATE 用户 SET 账号ID='" + account + "' WHERE 账号ID='" + currentacc + "'";
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

        //获取所有用户信息
        public List<Account> GetallUserinfs()
        {
            string queryString = "SELECT 账号ID,密码,昵称,电话,性别 FROM 账号 natural join 用户";
            var result = new List<Account>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Account()
                        {
                            UserName = reader["账号ID"].ToString(),
                            Name = reader["昵称"].ToString(),
                            PhoneNumber = reader["电话"].ToString(),
                            Sex = (int)reader["性别"],
                            PassWord = reader["密码"].ToString()
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
