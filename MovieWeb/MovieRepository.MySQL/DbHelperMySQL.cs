using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace MovieRepository.MySQL
{
    /// <summary>
    /// 数据库操作基类
    /// </summary>
    public class DbHelperMySQL
    {
        private static string _connectionString = string.Empty;
        /// <summary>
		/// 数据库连接字符串
		/// </summary>
		public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = ConfigurationManager.ConnectionStrings["Con_MySql"].ConnectionString;
                }
                return _connectionString;
            }
            set { _connectionString = value; }
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params MySqlParameter[] parameters)
        {
            int num = 0;
            try
            {
               
                MySqlCommand cmd = new MySqlCommand();
                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);
                    num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                num = -1;
                throw;
            }
            return num;
        }
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            int num = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, null);
                    num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                num = -1;
            }
            return num;
        }
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] parameters)
        {
            int num = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, parameters);
                num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                num = -1;
            }
            return num;
        }
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText)
        {
            int num = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, null);
                num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                num = -1;
            }
            return num;
        }
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="isOpenTrans">事务对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(MySqlTransaction isOpenTrans, CommandType cmdType, string cmdText, params MySqlParameter[] parameters)
        {
            int num = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                if (isOpenTrans == null || isOpenTrans.Connection == null)
                {
                    using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                    {
                        PrepareCommand(cmd, conn, isOpenTrans, cmdType, cmdText, parameters);
                        num = cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    PrepareCommand(cmd, isOpenTrans.Connection, isOpenTrans, cmdType, cmdText, parameters);
                    num = cmd.ExecuteNonQuery();
                }
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                num = -1;
            }
            return num;
        }
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="isOpenTrans">事务对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(MySqlTransaction isOpenTrans, CommandType cmdType, string cmdText)
        {
            int num = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, isOpenTrans.Connection, isOpenTrans, cmdType, cmdText, null);
                num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                num = -1;
            }
            return num;
        }
        /// <summary>
        /// 使用提供的参数，执行有结果集返回的数据库操作命令、并返回SqlDataReader对象
        /// </summary>
        /// <param name="isOpenTrans">事务对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行<</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns>返回SqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader(MySqlTransaction isOpenTrans, CommandType cmdType, string cmdText, params MySqlParameter[] parameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                PrepareCommand(cmd, conn, isOpenTrans, cmdType, cmdText, parameters);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();
                return null;
            }
        }
        /// <summary>
        /// 使用提供的参数，执行有结果集返回的数据库操作命令、并返回SqlDataReader对象
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns>返回SqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params MySqlParameter[] parameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();
                return null;
            }
        }
        /// <summary>
        ///使用提供的参数，执行有结果集返回的数据库操作命令、并返回SqlDataReader对象
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行<</param>
        /// <returns>返回SqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader(CommandType cmdType, string cmdText)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, null);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();
                return null;
            }
        }
        /// <summary>
        /// 查询数据填充到数据集DataSet中
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns>数据集DataSet对象</returns>
        public static DataSet GetDataSet(CommandType cmdType, string cmdText, params MySqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();
                return null;
            }
        }
        /// <summary>
        /// 查询数据填充到数据集DataSet中
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">命令文本</param>
        /// <returns>数据集DataSet对象</returns>
        public static DataSet GetDataSet(CommandType cmdType, string cmdText)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, null);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();
                return null;
            }
        }
        /// <summary>
        /// 依靠数据库连接字符串connectionString,
        /// 使用所提供参数，执行返回首行首列命令
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns>返回一个对象，使用Convert.To{Type}将该对象转换成想要的数据类型。</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params MySqlParameter[] parameters)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, parameters);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 依靠数据库连接字符串connectionString,
        /// 使用所提供参数，执行返回首行首列命令
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <returns>返回一个对象，使用Convert.To{Type}将该对象转换成想要的数据类型。</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, null);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        ///依靠数据库连接字符串connectionString,
        /// 使用所提供参数，执行返回首行首列命令
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns>返回一个对象，使用Convert.To{Type}将该对象转换成想要的数据类型。</returns>
        public static object ExecuteScalar(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] parameters)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, parameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        ///依靠数据库连接字符串connectionString,
        /// 使用所提供参数，执行返回首行首列命令
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <returns>返回一个对象，使用Convert.To{Type}将该对象转换成想要的数据类型。</returns>
        public static object ExecuteScalar(MySqlConnection connection, CommandType cmdType, string cmdText)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, null);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        ///依靠数据库连接字符串connectionString,
        /// 使用所提供参数，执行返回首行首列命令
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="isOpenTrans">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdType">存储过程名称或者T-SQL命令行</param>
        /// <param name="cmdText">执行命令所需的sql语句对应参数</param>
        /// <returns>返回一个对象，使用Convert.To{Type}将该对象转换成想要的数据类型。</returns>
        public static object ExecuteScalar(MySqlConnection conn, MySqlTransaction isOpenTrans, CommandType cmdType, string cmdText)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, conn, isOpenTrans, cmdType, cmdText, null);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        ///依靠数据库连接字符串connectionString,
        /// 使用所提供参数，执行返回首行首列命令
        /// </summary>
        /// <param name="isOpenTrans">事务</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns>返回一个对象，使用Convert.To{Type}将该对象转换成想要的数据类型。</returns>
        public static object ExecuteScalar(MySqlTransaction isOpenTrans, CommandType cmdType, string cmdText, params MySqlParameter[] parameters)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, isOpenTrans.Connection, isOpenTrans, cmdType, cmdText, parameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 为即将执行准备一个命令
        /// </summary>
        /// <param name="cmd">MySqlCommand对象</param>
        /// <param name="conn">MySqlConnection对象</param>
        /// <param name="isOpenTrans">MySqlTransaction对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行, e.g. Select * from Products</param>
        /// <param name="cmdParms">MySqlParameter to use in the command</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction isOpenTrans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (isOpenTrans != null)
            {
                cmd.Transaction = isOpenTrans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }

    }
}
