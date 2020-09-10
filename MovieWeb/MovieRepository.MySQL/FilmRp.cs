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
        public DataRowCollection queryFilms(string key)
        {
            if (key == null) key = "1";
            String[] keys = key.Split(' ');
            MySqlConnection conn = getConnect();
            try
            {
                MySqlCommand mycom = conn.CreateCommand();
                mycom.CommandText = $"SELECT * FROM 影片 where 名字 like '%{keys[0]}%'";
                MySqlDataAdapter adap = new MySqlDataAdapter(mycom);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                return ds.Tables[0].Rows;
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
        public Boolean rate(String id, String s,String userId)
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
    }
}
