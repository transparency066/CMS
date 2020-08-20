using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    public class Account
    {
        private string connString = "server=localhost;database=moviedb;username=root;pwd=123";

        public int CheckAccount(string UserName,string PassWord)//登录系统
        {
            string sql = "select 权限 from 账号 where 账号ID = @username and 密码 = @password";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new MySqlParameter("@username", UserName));
                    cmd.Parameters.Add(new MySqlParameter("@password", PassWord));
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        return 0;
                    }
                    else while (reader.Read())
                        {
                            return reader.GetInt32("权限");
                        }
                }
            }
            return 1;
        }

        public int CreateAccount(string UserName,string PassWord)
        {
            string sql = "select * from 账号 where 账号ID = @username and 密码 = @password";//判断是否已存在该用户名

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new MySqlParameter("@username", UserName));
                    cmd.Parameters.Add(new MySqlParameter("@password", PassWord));
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return 0;
                    }
                    reader.Close();
                    sql = "insert into 账号(账号ID,密码,权限) values (@userid,@pwd,1)";//插入语句
                    cmd.CommandText = sql;//更新sql语句
                    cmd.Parameters.Add(new MySqlParameter("@userid", UserName));
                    cmd.Parameters.Add(new MySqlParameter("@pwd", PassWord));
                    cmd.ExecuteNonQuery();
                    return 1;
                }
            }
        }
    }
}
