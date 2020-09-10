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
    public class MarkRp
    {
        private string connectionString = "server=localhost;database=moviedb;username=root;pwd=123";

        private MySqlConnection getConnect()
        {
            String connetStr = "server=localhost;port=3306;user=root;password=123; database=moviedb;SslMode=none;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            conn.Open();
            return conn;
        }
        //获取指定影片的评论
        public DataRowCollection getComments()
        {
            MySqlConnection conn = getConnect();
            try
            {
                MySqlCommand mycom = conn.CreateCommand();
                mycom.CommandText = "SELECT * From 评论记录 ";
                MySqlDataAdapter adap = new MySqlDataAdapter(mycom);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                return  ds.Tables[0].Rows;
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

        public DataRowCollection queryTicket()
        {

            MySqlConnection conn = getConnect();
            try
            {
                MySqlCommand mycom = conn.CreateCommand();
                mycom.CommandText = "SELECT  影片.影片ID,影片.票价,影片.名字,影片.类型,Count(`影票`.`场次ID`) as 总人数 , Count(`影票`.`场次ID`) * `影片`.`票价` as 总票房  FROM  影片 LEFT JOIN `场次` on `场次`.`影片ID` = `影片`.`影片ID` LEFT JOIN `影票` on `影票`.`场次ID` = `场次`.`场次ID` group by `影片`.`影片ID`";
                MySqlDataAdapter adap = new MySqlDataAdapter(mycom);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                return  ds.Tables[0].Rows;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        //删除评论
        public Boolean DelComment(String pid, String id)
        {
            MySqlConnection conn = getConnect();
            try
            {
                MySqlCommand mycom = conn.CreateCommand();
                if (id == null || pid == null) return false;
                mycom.CommandText = $"delete from 评论记录 where 账号ID1='{id}' and 影票ID1 = '{pid}' ";
                mycom.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return true;
        }
    }
}
