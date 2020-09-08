using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    public class M2U_Rp
    {
        private string connectionstring = "server=localhost;database=moviedb;username=root;pwd=123;";
        //Old Guids=true;可加可不加

        public List<M2UModel> GetM2UModels(string uid)
        {
            string sql = "select 回复管理员账号ID,回复时间,回复内容,反馈时间 from 回复内容 where 反馈账号ID=@uid";
            var result = new List<M2UModel>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@uid", uid));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new M2UModel()
                        {
                            mid=reader.GetString("回复管理员账号ID"),
                            M2Utime=reader.GetDateTime("回复时间"),
                            M2Utext=reader.GetString("回复内容"),
                            U2Mtime=reader.GetDateTime("反馈时间"),
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
