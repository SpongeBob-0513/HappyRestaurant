using System;
using MySql.Data.MySqlClient;

namespace GameServer.Tool
{
    public class ConnHelper
    {
        public const string CONNECTIONSTRING =
            "dataSource = 127.0.0.1; port = 3306; database = gamedb; user = root; pwd = root";

        public static MySqlConnection Connect()
        {
            MySqlConnection conn = new MySqlConnection(CONNECTIONSTRING);
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine("连接数据库的时候出现异常：" + e);
                return null;
            }
        }

        public static void CloseConnection(MySqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
            }
            else
            {
                Console.WriteLine("MySqlConnection 为空，无法正常关闭");
            }
        }
    }
}