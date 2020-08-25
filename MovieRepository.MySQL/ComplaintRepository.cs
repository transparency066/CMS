using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    public class ComplaintRepository
    {
        private string connectionString = "server=localhost;user=root;database=影院管理系统;port=3306;password=12345678";

        public List<UComplaint> UntreatedComplaint()
        {
            string queryString = "SELECT  反馈时间, 账号ID, 反馈内容, 是否回复 FROM 反馈记录 WHERE(是否回复 = 0)";

            var result = new List<UComplaint>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))   
            {
                MySqlCommand command = new MySqlCommand(queryString, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read()){
                    result.Add(new UComplaint()
                    {
                        UID = reader["账号ID"].ToString(),
                        ComplaintTime = DateTime.Parse(reader["反馈时间"].ToString()),
                        Complaint = reader["账号ID"].ToString(),
                    });

                }
                reader.Close();
            }
            return result;
        }
    }
}
