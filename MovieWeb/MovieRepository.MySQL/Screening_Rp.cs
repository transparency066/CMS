using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieModel;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{


        public class Screening_Rp
        {
            private string connectionstring = "server=localhost;database=moviedb;username=root;pwd=123;Old Guids=true;";
            //Allow User Variables=True;
            public List<ScreeningModel> Manager_GetAllScreeningData()
            {
                string queryString = "select * from 场次";
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
                                screening_id = reader.GetString("场次ID"),
                                film_id = reader.GetString("影片ID"),
                                start_time = reader.GetDateTime("开映时间"),
                                hall_id = reader.GetString("放映厅ID"),
                            });
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
                return result;
            }

            public HallModel GetHallByHallId(string hall_id)
            {
                string queryString = "select * from 放映厅 where 放映厅ID=@hall_id";
                var result = new HallModel();
                using (MySqlConnection conn = new MySqlConnection(connectionstring))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(queryString, conn))
                    {
                        cmd.Parameters.Add(new MySqlParameter("@hall_id", hall_id));
                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            result = new HallModel()
                            {
                                hall_id = reader.GetString("放映厅ID"),
                                total = reader.GetInt32("容量"),
                                location = reader.GetString("放映厅位置"),
                                
                            };
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
                return result;
            }

        public List<SeatsModel> GetSeatsByHall(string screening_id)
        {
            string hall_id;
            string preQuery = "select 放映厅ID from 场次 where 场次ID = @screening_id";
            using (MySqlConnection preConn = new MySqlConnection(connectionstring))
            {
                preConn.Open();
                using (MySqlCommand preCmd = new MySqlCommand(preQuery, preConn))
                {
                    preCmd.Parameters.Add(new MySqlParameter("@screening_id", screening_id));
                    MySqlDataReader reader = preCmd.ExecuteReader();
                    reader.Read();
                    hall_id = reader.GetString("放映厅ID");
                }
            }

            string queryString = "select * from 座位 where 放映厅ID=@hall_id";
            var result = new List<SeatsModel>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(queryString, conn))
                {
                    cmd.Parameters.Add(new MySqlParameter("@hall_id", hall_id));

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var seat = new SeatsModel()
                        {
                            seats_id = reader.GetInt32("座位ID"),
                            hall_id = reader.GetString("放映厅ID"),
                            x = reader.GetInt32("行位置"),
                            y = reader.GetInt32("列位置")
                        };
                        using (MySqlCommand _cmd = new MySqlCommand())
                        {
                            _cmd.Parameters.Add(new MySqlParameter("@screening_id", screening_id));
                            using (MySqlConnection connection = new MySqlConnection(connectionstring))
                            {
                                connection.Open();
                                _cmd.Connection = connection;
                                _cmd.CommandText = "select * from 影票 where 场次ID=@screening_id and 座位ID='" + seat.seats_id + "'";
                                MySqlDataReader _reader = _cmd.ExecuteReader();
                                seat.flag = _reader.HasRows ? 1 : 0;
                                _reader.Close();
                                connection.Close();
                            }
                        }
                        result.Add(seat);
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return result;
        }

        public void DeleteScreeningById(string screening_id)
            {
                string sql = "delete from 场次 where 场次ID=@screening_id";
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
                string queryString = "select * from 场次 where 场次ID=@screening_id";
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
                                screening_id = reader.GetString("场次ID"),
                                film_id = reader.GetString("影片ID"),
                                start_time = reader.GetDateTime("开映时间"),
                                hall_id = reader.GetString("放映厅ID"),

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
                string queryString = "update 场次 set 影片ID=@film_id,放映厅ID=@hall_id,开映时间=@start_time where 场次ID=@screening_id ";
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
                string queryString = "insert into 场次(场次ID,影片ID,放映厅ID,开映时间) values(@screening_id,@film_id,@hall_id,@start_time)";
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
        public bool CheckHallTime(string hall_id, DateTime start_time)
        {
            string queryString = "select * from 场次 where 放映厅ID=@hall_id and ((TIMESTAMPDIFF(hour,开映时间,@start_time1)<2 and TIMESTAMPDIFF(hour,开映时间,@start_time2)>=0) or (TIMESTAMPDIFF(hour,开映时间,@start_time3)>-2 and (TIMESTAMPDIFF(hour,开映时间,@start_time4)<=0)))";
            var result = new List<ScreeningModel>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(queryString, conn))
                {

                    cmd.Parameters.Add(new MySqlParameter("@hall_id", hall_id));
                    cmd.Parameters.Add(new MySqlParameter("@start_time1", start_time));
                    cmd.Parameters.Add(new MySqlParameter("@start_time2", start_time));
                    cmd.Parameters.Add(new MySqlParameter("@start_time3", start_time));
                    cmd.Parameters.Add(new MySqlParameter("@start_time4", start_time));

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ScreeningModel()
                        {
                            screening_id = reader.GetString("场次ID"),
                            film_id = reader.GetString("影片ID"),
                            start_time = reader.GetDateTime("开映时间"),
                            hall_id = reader.GetString("放映厅ID"),

                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            if (result.Count==0)
            {
                return true;
            }
            else return false;
        }

        public bool CheckScreeningId(string screening_id)
        {
            string queryString = "select * from 场次 where 场次ID=@screening_id ;";
            var result = new List<ScreeningModel>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(queryString, conn))
                {


                    cmd.Parameters.Add(new MySqlParameter("@screening_id", screening_id));

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ScreeningModel()
                        {
                            screening_id = reader.GetString("场次ID"),
                            film_id = reader.GetString("影片ID"),
                            start_time = reader.GetDateTime("开映时间"),
                            hall_id = reader.GetString("放映厅ID"),

                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            if (result.Count==0)
            {
                return true;
            }
            else return false;
        }
    }

}
