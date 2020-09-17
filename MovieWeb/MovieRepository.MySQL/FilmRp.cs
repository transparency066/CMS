using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MovieModel;
using System.Data;

namespace MovieRepository.MySQL
{
    public class FilmRp
    {
        private MySqlConnection getConnect()
        {
            String connetStr = "server=localhost;port=3306;user=root;password=123; database=moviedb;SslMode=none;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            conn.Open();
            return conn;
        }


        //单个影片数据
        public DataRow queryFilmById(string id)
        {
            MySqlConnection conn = getConnect();
            try
            {
                MySqlCommand mycom = conn.CreateCommand();
                mycom.CommandText = $"SELECT * FROM 影片 where 影片ID = '{id}'";
                MySqlDataAdapter adap = new MySqlDataAdapter(mycom);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                return ds.Tables[0].Rows[0];
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return null;
        }

        // 获取评分数据
        public string queryScoreById(string key)
        {
            MySqlConnection conn = getConnect();
            try
            {
                MySqlCommand mycom1 = conn.CreateCommand();
                mycom1.CommandText = $"SELECT AVG(分数) as 分数 FROM 评分记录 where 影票ID in (select 影票ID from 影票 where 场次ID in (select 场次ID from 场次 where `影片ID`  = '{key}')) ";
                MySqlDataAdapter adap1 = new MySqlDataAdapter(mycom1);
                DataSet ds1 = new DataSet();
                adap1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                    return ds1.Tables[0].Rows[0]["分数"].ToString();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return null;
        }

        //搜索影片
        public List<SearchMovie> queryFilms(string key)
        {
            if (key == null) key = "1";
            String[] keys = key.Split(' ');
            MySqlConnection conn = getConnect();
            var searchMovies = new List<SearchMovie>();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.CommandText = "select 影片ID,名字,类型,图片url from 影片";
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString("名字").Contains(key))
                    {
                        var searchMovie = new SearchMovie()
                        {
                            ID = reader.GetString("影片ID"),
                            Name = reader.GetString("名字"),
                            Type = reader.GetString("类型"),
                            Url = reader.GetString("图片url")
                        };
                        using (MySqlCommand _cmd = new MySqlCommand())
                        {
                            MySqlConnection _conn = getConnect();
                            _cmd.Connection = _conn;
                            ///有评分为空的情况无法判断
                            _cmd.CommandText = "select 分数 from 影片 natural join 场次 natural join 影票 join 评分记录 using (影票ID) where 影片ID = @id";
                            _cmd.Parameters.Add(new MySqlParameter("@id", searchMovie.ID));
                            MySqlDataReader _reader = _cmd.ExecuteReader();
                            float cnt = 0, sum = 0;
                            if (!_reader.HasRows)
                            {
                                searchMovie.Score = -1;
                                searchMovies.Add(searchMovie);
                                continue;
                            }
                            else
                            {
                                while (_reader.Read())
                                {
                                    sum += _reader.GetFloat("分数");
                                    cnt++;
                                }
                            }
                            _reader.Close();
                            sum = sum / cnt;
                            searchMovie.Score = sum;
                        }
                        searchMovies.Add(searchMovie);
                    }
                }
                reader.Close();
            }
            return searchMovies;
        }

        public List<RankMovie> GetRankMovies()
        {
            MySqlConnection conn = getConnect();
            var rankMovies = new List<RankMovie>();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.CommandText = "select 影片ID,名字,图片url,count(影片ID) as 销售量 from 影片 natural join 场次 join 影票 using (场次ID) group by 影片ID,名字,图片url order by 销售量 desc";
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();
                int cnt = 0;
                while (reader.Read())
                {
                    rankMovies.Add(new RankMovie
                    {
                        ID = reader.GetString("影片ID"),
                        Name = reader.GetString("名字"),
                        Tickets = reader.GetInt32("销售量"),
                        Url = reader.GetString("图片url")
                    });
                    cnt++;
                    if (cnt == 10) break;
                }
                reader.Close();
                return rankMovies;
            }
        }

        public Boolean Rate(String id, String s, String userId)
        {
            int tmp = 0;

            if (id != null)
            {
                MySqlConnection conn = getConnect();
                try
                {
                    MySqlCommand mycom = conn.CreateCommand();

                    mycom.CommandText = $"Insert into 评分记录 value('{id}','{userId}','{s}') ";
                    mycom.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062)
                    {
                        return false;// "用户已评分";
                    }
                    if (ex.Number == 1452)
                    {
                        return false;//"没有购票记录";
                    }
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return tmp > 0;
        }

        //寻找具有同等类型的电影
        public List<RecommendMovie> GetRecommendMovies(string[] InitialType, string InitialId)
        {
            MySqlConnection conn = getConnect();
            var recommendMovies = new List<RecommendMovie>();
            string sql = "select * from 影片";
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.CommandText = sql;
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (InitialId == reader.GetString("影片ID")) continue;
                    if (recommendMovies.Count >= 4) break;
                    string type = reader.GetString("类型");
                    foreach (var i in InitialType)
                    {
                        if (type.Contains(i))
                        {
                            recommendMovies.Add(new RecommendMovie
                            {
                                ID = reader.GetString("影片ID"),
                                Name = reader.GetString("名字"),
                                Type = reader.GetString("类型"),
                                Url = reader.GetString("图片url")
                            });
                            break;
                        }
                    }
                }
                reader.Close();
                return recommendMovies;
            }
        }
    }
}
