using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MovieModel;
namespace MovieRepository.MySQL
{

    public class Screening_Rp
    {
        private string connectionstring = "server=localhost;database=cms;username=root;pwd=123;Old Guids=true;";
        //Allow User Variables=True;
        public List<ScreeningModel> Manager_GetAllScreeningData()
        {
            string queryString = "select * from screening";
            var result = new List<ScreeningModel>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(queryString, conn))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ScreeningModel()
                        {
                            screening_id = reader.GetString("screening_id"),
                            film_id = reader.GetString("film_id"),
                            start_time = reader.GetDateTime("start_time"),
                            hall_id = reader.GetString("hall_id"),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return result;
        }

        public HallModel GetHallByScreeningId(string screening_id)
        {
            string queryString = "select * from hall where screening_id=@screening_id";
            var result = new HallModel();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(queryString, conn))
                {
                    cmd.Parameters.Add(new MySqlParameter("@screening_id", screening_id));
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result = new HallModel()
                        {
                            screening_id = reader.GetString("screening_id"),
                            total = reader.GetInt32("total"),
                            left = reader.GetInt32("left"),
                            hall_id = reader.GetString("hall_id"),
                        };
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return result;
        }

        public List<SeatsModel> GetSeatsByScreening(string screening_id)
        {
            string queryString = "select * from seats where screening_id=@screening_id";
            var result = new List<SeatsModel>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(queryString, conn))
                {
                    cmd.Parameters.Add(new MySqlParameter("@screening_id", screening_id));
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new SeatsModel()
                        {
                            screening_id = reader.GetString("screening_id"),
                            
                            x = reader.GetInt32("x"),
                            y = reader.GetInt32("y"),
                            available = reader.GetBoolean("available"),
                            used = reader.GetBoolean("used"),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return result;
        }

        public void DeleteScreeningById(string screening_id)
        {
            string sql = "delete from screening where screening_id=@screening_id";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@screening_id", screening_id));

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { }
                finally
                {
                    conn.Close();
                }
            }
        }


        public ScreeningModel GetScreeningById(string screening_id)
        {
            string queryString = "select * from screening where screening_id=@screening_id";
            var result = new ScreeningModel();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(queryString, conn))
                {
                    cmd.Parameters.Add(new MySqlParameter("@screening_id", screening_id));
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result = new ScreeningModel()
                        {
                            screening_id = reader.GetString("screening_id"),
                            film_id = reader.GetString("film_id"),
                            start_time = reader.GetDateTime("start_time"),
                            hall_id = reader.GetString("hall_id"),

                        };
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return result;
        }

        public void ModifyScreening(string screening_id, string film_id, string hall_id, DateTime start_time)
        {
            string queryString = "update screening set film_id=@film_id,hall_id=@hall_id,start_time=@start_time where screening_id=@screening_id ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@film_id", film_id));
                cmd.Parameters.Add(new MySqlParameter("@hall_id", hall_id));
                cmd.Parameters.Add(new MySqlParameter("@screening_id", screening_id));
                cmd.Parameters.Add(new MySqlParameter("@start_time", start_time));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        public void CreateScreening(string screening_id, string film_id, string hall_id, DateTime start_time)
        {
            string queryString = "insert into screening(screening_id,film_id,hall_id,start_time) values(@screening_id,@film_id,@hall_id,@start_time)";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@film_id", film_id));
                cmd.Parameters.Add(new MySqlParameter("@hall_id", hall_id));
                cmd.Parameters.Add(new MySqlParameter("@screening_id", screening_id));
                cmd.Parameters.Add(new MySqlParameter("@start_time", start_time));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }





        }
    }
}
