using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MovieModel;

namespace MovieRepository.MySQL
{
    public class UserCenterRp
    {
        private string connectionString = "server=localhost;database=moviedb;username=root;pwd=123";

        //插入用户信息
        public int UpdateUserInfo(string account, string name,string phone,int sex)
        {
            string queryString1 = "select count(0) from 用户 where 账号ID='"+account+"'";
            string insertString = "Insert Into `用户` (账号ID, 昵称,电话,性别) values ('"+ account + "','" + name+ "','" + phone + "','" + sex + "')";
            string updateString = "Update 用户 SET 昵称 = '" + name + "',电话 = '" + phone + "',性别 = '" + sex + "' WHERE 账号ID='" + account + "'";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var isUpdate = false;
                connection.Open();
                using (MySqlCommand command1 = new MySqlCommand())
                {
                    command1.Connection = connection;
                    command1.CommandText = queryString1;
                    int count = int.Parse(command1.ExecuteScalar().ToString());
                    if (count>0)
                    {
                        isUpdate = true;
                    }
                }
                using (MySqlCommand command1 = new MySqlCommand())
                {
                    command1.Connection = connection;
                    if (isUpdate)
                    {
                        command1.CommandText = updateString;
                    }
                    else
                    {
                        command1.CommandText = insertString;
                    }
                    command1.ExecuteNonQuery();
                }
                connection.Close();
            }
            return 1;
        }

        //获取所有用户信息
        public 用户 GetUserInfoByUID(string account)
        {
            string queryString = @"  select * from 用户 where 账号ID='"+account+"' ";
            var entity = new 用户();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        entity.账号ID = reader["账号ID"].ToString();
                        entity.昵称 = reader["昵称"].ToString();
                        entity.电话 = reader["电话"].ToString();
                        entity.性别 = (int)reader["性别"];
                    }
                    reader.Close();
                }
                catch (Exception ex) { }
                connection.Close();
            }
            return entity;
        }
        //获取所有用户信息
        public List<Ticket> GetTickList(string account)
        {
            string queryString = @"
                select 
	                a.`影票ID`
	                , a.`场次ID`
	                , a.`购票时间`
	                , e.`放映厅ID`
	                , e.`放映厅位置`
	                , d.`座位ID`
	                , d.`行位置`
	                , d.`列位置`
	                , a.`影票状态`
                from 
	                影票 a 
	                inner join 账号 b on a.`拥有者账号ID` = b.`账号ID` 
	                inner join 场次 c on a.`场次ID` = c.`场次ID` 
	                inner join 座位 d on a.`座位ID` = d.`座位ID`
	                inner join 放映厅 e on d.`放映厅ID` = e.`放映厅ID`
                where a.`拥有者账号ID` = '"+ account + "'order by a.`购票时间` desc";
            var result = new List<Ticket>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Ticket entity = new Ticket();
                        entity.影票ID = reader["影票ID"].ToString();
                        entity.场次ID = reader["场次ID"].ToString();
                        entity.购票时间 = DateTime.Parse(reader["购票时间"].ToString());
                        entity.放映厅ID = int.Parse(reader["放映厅ID"].ToString());
                        entity.放映厅位置 = reader["放映厅位置"].ToString();
                        entity.座位ID = int.Parse(reader["座位ID"].ToString());
                        entity.行位置 = int.Parse(reader["行位置"].ToString());
                        entity.列位置 = int.Parse(reader["列位置"].ToString());
                        entity.影票状态 = int.Parse(reader["影票状态"].ToString());

                        result.Add(entity);
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
