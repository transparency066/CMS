using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MovieModel;

namespace MovieRepository.MySQL
{
    public class AccountRp
    {
        private string connectionString = "server=localhost;database=moviedb;username=root;pwd=123";

        //检查账户
        public int CheckAccount(string UserName,string PassWord)
        {
            string sql = "select 权限 from 账号 where 账号ID = @username and 密码 = @password";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
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
                        return -1;
                    }
                    else while (reader.Read())
                        {
                            return reader.GetInt32("权限");
                        }
                }
            }
            return 1;
        }

        //创建账户
        public int CreateAccount(string UserName,string PassWord,string Name,string PhoneNumber,int Sex)
        {
            string sql = "select * from 账号 where 账号ID = @username";//判断是否已存在该用户名

            using (MySqlConnection conn = new MySqlConnection(connectionString))
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
                    sql = "insert into 账号(账号ID,密码,权限) values (@userid,@pwd,0)";//插入语句
                    cmd.CommandText = sql;//更新sql语句
                    cmd.Parameters.Add(new MySqlParameter("@userid", UserName));
                    cmd.Parameters.Add(new MySqlParameter("@pwd", PassWord));
                    cmd.ExecuteNonQuery();

                    sql = "insert into 用户 values (@uid,@Name,@PhoneNumber,@Sex)";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new MySqlParameter("@uid", UserName));
                    cmd.Parameters.Add(new MySqlParameter("@Name", Name));
                    cmd.Parameters.Add(new MySqlParameter("@PhoneNumber", PhoneNumber));
                    cmd.Parameters.Add(new MySqlParameter("@Sex", Sex));
                    cmd.ExecuteNonQuery();

                    return 1;
                }
            }
        }
    }
}
